using Abstracts;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Implements
{
    public class Loader: ILoader
    {
        public HtmlDocument Load(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
