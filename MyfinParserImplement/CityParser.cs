using Abstracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace MyfinParserImplement
{
    public class CityParser : ICityParser
    {
        public IEnumerable<string> Parse(HtmlDocument htmlDoc)
        {
            const string xpath = "//*[@id=\"modal-list-menu\"]/div/div/div[2]/div[2]/div/ul/li/a";
            var colomsContainers = htmlDoc.DocumentNode.SelectNodes(xpath);
            foreach (var item in colomsContainers)
            {
                var link = item.GetAttributeValue("data-slug", "none");
                if (link != "none")
                {
                    yield return $"https://myfin.by/currency/{link}";
                }
            }
        }
    }
}
