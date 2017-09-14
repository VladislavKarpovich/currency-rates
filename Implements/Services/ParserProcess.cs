using Abstracts;
using Abstracts.Repositories;
using Abstracts.Services;
using DataStructs;
using Implements.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implements.Services
{
    public class ParserProcess : IParserProcess
    {
        private readonly IRepository<Bank> _bankRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Currency> _currencRepository;
        private readonly IRepository<Office> _officeRepository;
        private readonly ICrossRateRepository<CrossRate> _crossRateRepository;
        private readonly ICrossRateRepository<ActualCrossRate> _actualCrossRateRepository;
        private readonly IParserPresenter _parser;

        public ParserProcess(
            IRepository<Bank> bankRepository,
            IRepository<City> cityRepository,
            IRepository<Currency> currencRepository,
            IRepository<Office> officeRepository,
            ICrossRateRepository<CrossRate> crossRateRepository,
            ICrossRateRepository<ActualCrossRate> actualCrossRateRepository,
        IParserPresenter parser)
        {
            _bankRepository = bankRepository;
            _cityRepository = cityRepository;
            _currencRepository = currencRepository;
            _officeRepository = officeRepository;
            _crossRateRepository = crossRateRepository;
            _actualCrossRateRepository = actualCrossRateRepository;
            _parser = parser;

        }

        public async Task RunAsync()
        {
            var parsResult = await _parser.ParseAsync();

            var nodes = parsResult as List<CurrencyRate> ?? parsResult.ToList();
            await _actualCrossRateRepository.CleanTable();
            foreach (var node in nodes)
            {
                await RecordAsync(node);
            }
        }

        private async Task RecordAsync(CurrencyRate node)
        {
            var bank = await _bankRepository.FindOrCreateAsync(new Bank { Name = node.Bank });
            var city = await _cityRepository.FindOrCreateAsync(new City { Name = node.City });
            var office = await GetOffice(node, bank, city);

            var crossRates = new List<CrossRate>
            {
                await GetCrossRateAsync("BYR", "USD", node.BYR_USD_Bid, node.BYR_USD_Ask, node.DateTime, office.OfficeId),
                await GetCrossRateAsync("BYR", "EUR", node.BYR_EUR_Bid, node.BYR_EUR_Ask, node.DateTime, office.OfficeId),
                await GetCrossRateAsync("BYR", "RUB", node.BYR_RUB_Bid, node.BYR_RUB_Ask, node.DateTime, office.OfficeId),
                await GetCrossRateAsync("EUR", "USD", node.BYR_EUR_Bid, node.BYR_USD_Ask, node.DateTime, office.OfficeId)
            };
            foreach (var rate in crossRates)
            {
                await _crossRateRepository.AddCrossRateAsync(rate);
                await _actualCrossRateRepository.AddCrossRateAsync(new ActualCrossRate
                {
                    OfficeId = rate.OfficeId,
                    Bid = rate.Bid,
                    Ask = rate.Ask,
                    DateTime = rate.DateTime,
                    CurrencyIdFrom = rate.CurrencyIdFrom,
                    CurrencyIdTo = rate.CurrencyIdTo
                });
            }
        }

        private async Task<CrossRate> GetCrossRateAsync(
            string from,
            string to, 
            double bid, 
            double ask, 
            DateTime? dateTime,
            Guid officeId)
        {
            var idFrom = await _currencRepository.FindOrCreateAsync(new Currency { Name = from });
            var idTo = await _currencRepository.FindOrCreateAsync(new Currency { Name = to });
            return new CrossRate
            {
                DateTime = dateTime,
                Bid = bid,
                Ask = ask,
                CurrencyIdFrom = idFrom.CurrencyId,
                CurrencyIdTo = idTo.CurrencyId,
                OfficeId = officeId
            };
        }

        private Task<Office> GetOffice(CurrencyRate node, Bank bank, City city)
        {
            var office = new Office
            {
                Tittle = node.Office,
                BankId = bank.BankId,
                CityId = city.CityId,
                Address = node.Adress,
                Contacts = node.Phone
            };
            return _officeRepository.FindOrCreateAsync(office);
        }
    }
}
