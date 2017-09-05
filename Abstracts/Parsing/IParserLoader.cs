namespace Abstracts
{
    public interface IParserLoader
    {
        ICurrencyRateParser GetParser(string url);
    }
}
