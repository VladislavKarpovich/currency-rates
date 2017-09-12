using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Abstracts
{
    public interface ILoader
    {
         Task<HtmlDocument> LoadAsync(string url);
    }
}
