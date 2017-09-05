using Abstracts;
using DataStructs;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace MyfinParserImplement
{
    public class MyfinParser: ICurrencyRateParser
    {
        public IEnumerable<CurrencyRate> Parse(HtmlDocument htmlDoc)
        {
            const string xpath = "//*[@id=\"workarea\"]/div[2]/div[2]/div/table/tbody/tr";
            var rows = htmlDoc.DocumentNode.SelectNodes(xpath);

            var cities = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"workarea\"]/div[2]/div[1]/ul/li");
            var city = cities.FirstOrDefault(c => c.GetAttributeValue("class", "none") == "active").InnerText;

            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i += 2)
                {
                    var rates = ParseRateFromBank(bank: rows[i], table: rows[i + 1]);
                    foreach (var rate in rates)
                    {
                        rate.City = city;
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

        private CurrencyRate ParseRate(HtmlNode html)
        {
            return new CurrencyRate
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
}
