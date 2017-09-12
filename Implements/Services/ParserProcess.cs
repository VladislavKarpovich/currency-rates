using Abstracts;
using Abstracts.Repositories;
using Abstracts.Services;
using DataStructs;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Implements.Services
{
    public class ParserProcess : IParserProcess
    {
        private readonly IDbRepository<Bank> _bankRepository;
        private readonly IDbRepository<City> _cityRepository;
        private readonly IDbRepository<Currency> _currencRepository;
        private readonly IDbRepository<Office> _officeRepository;
        private readonly ICrossRateRepository _crossRateRepository;
        private readonly IParserPresenter _parser;

        public ParserProcess(
            IDbRepository<Bank> bankRepository,
            IDbRepository<City> cityRepository,
            IDbRepository<Currency> currencRepository,
            IDbRepository<Office> officeRepository,
            ICrossRateRepository crossRateRepository,
            IParserPresenter parser)
        {
            _bankRepository = bankRepository;
            _cityRepository = cityRepository;
            _currencRepository = currencRepository;
            _officeRepository = officeRepository;
            _crossRateRepository = crossRateRepository;
            _parser = parser;

        }

        public async Task RunAsync()
        {
            var parsResult = await _parser.ParseAsync();
            var nodes = parsResult as List<CurrencyRate> ?? parsResult.ToList();
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
                Debug.WriteLine("Recoding");
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
