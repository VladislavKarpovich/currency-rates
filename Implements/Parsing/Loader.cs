using Abstracts;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace Implements
{
    public class Loader: ILoader
    {
        public async Task<HtmlDocument> LoadAsync(string url)
        {
            var http = new HttpClient();
            var html = await http.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
    }
}
