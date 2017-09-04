using HtmlAgilityPack;
using System.Collections.Generic;

namespace MyfinParser
{
    class CityParser
    {
        public static IEnumerable<City> ParseCities()
        {
            var html = @"https://myfin.by/currency/minsk";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            const string xpath = "//*[@id=\"modal-list-menu\"]/div/div/div[2]/div[2]/div/ul/li/a";
            var colomsContainers = htmlDoc.DocumentNode.SelectNodes(xpath);
            foreach (var item in colomsContainers)
            {
                var link = item.GetAttributeValue("data-slug", "none");
                var name = item.InnerText;
                if (link != "none")
                {
                    yield return new City { Name = name, Link = link };
                }
            }
        }
    }
}
