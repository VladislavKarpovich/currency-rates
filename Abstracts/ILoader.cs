using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Abstracts
{
    public interface ILoader
    {
         HtmlDocument Load(string url);
    }
}
