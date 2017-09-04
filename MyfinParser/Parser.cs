using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Abstracts;
using CurrencyWebApi.Models;

namespace MyfinParser
{
    public class Parser : IParser
    {
        public IEnumerable<CurrencyRate> Parse()
        {
            var cities = CityParser.ParseCities();
            foreach (var city in cities)
            {
                var rates = ParseRateFromCity(city);
                foreach (var rate in rates)
                {
                    yield return rate;
                }
            }
        }

        private IEnumerable<CurrencyRate> ParseRateFromCity(City city)
        {
            var html = $"https://myfin.by/currency/${city.Link}";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            const string xpath = "//*[@id=\"workarea\"]/div[2]/div[2]/div/table/tbody/tr";
            var rows = htmlDoc.DocumentNode.SelectNodes(xpath);
            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i += 2)
                {
                    var rates = ParseRateFromBank(bank: rows[i], table: rows[i + 1]);
                    foreach (var rate in rates)
                    {
                        rate.City = city.Name;
                        yield return rate;
                    }
                }
            }
        }

        private IEnumerable<CurrencyRate> ParseRateFromBank(HtmlNode bank, HtmlNode table)
        {
            var bankName = bank.SelectSingleNode("td/span/a").InnerText;
            var offices = table
                .SelectNodes("td/div/div/div")
                .Last()
                .SelectNodes("table/tbody/tr");

            foreach (var office in offices)
            {
                var rate = ParseRate(office);
                rate.Bank = bankName;
                yield return rate;
            }
        }

        private CurrencyRate ParseRate(HtmlNode html) => new CurrencyRate
        {
            Office = html.SelectSingleNode("td/div/a").InnerText.Trim(),
            Phone = html.SelectSingleNode("td/div[2]").InnerText.Trim(),
            Adress = html.SelectSingleNode("td/div[3]").InnerText.Trim(),
            DateTime = ParserUtils.ParseDateTime(html.SelectSingleNode("td/div[4]").InnerText.Trim()),
            BYR_USD_Ask = ParserUtils.ParseRate(html.SelectSingleNode("td[2]").InnerText),
            BYR_USD_Bid = ParserUtils.ParseRate(html.SelectSingleNode("td[3]").InnerText),
            BYR_EUR_Ask = ParserUtils.ParseRate(html.SelectSingleNode("td[4]").InnerText),
            BYR_EUR_Bid = ParserUtils.ParseRate(html.SelectSingleNode("td[5]").InnerText),
            BYR_RUB_Ask = ParserUtils.ParseRate(html.SelectSingleNode("td[6]").InnerText),
            BYR_RUB_Bid = ParserUtils.ParseRate(html.SelectSingleNode("td[7]").InnerText),
            EUR_USD_Ask = ParserUtils.ParseRate(html.SelectSingleNode("td[8]").InnerText),
            EUR_USD_Bid = ParserUtils.ParseRate(html.SelectSingleNode("td[9]").InnerText)
        };
    }
}
