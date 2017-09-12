using System;

namespace DataStructs
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Bank { get; set; }
        public string Office { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public DateTime? DateTime { get; set; }
        public double BYR_USD_Ask { get; set; }
        public double BYR_USD_Bid { get; set; }
        public double BYR_EUR_Ask { get; set; }
        public double BYR_EUR_Bid { get; set; }
        public double BYR_RUB_Ask { get; set; }
        public double BYR_RUB_Bid { get; set; }
        public double EUR_USD_Ask { get; set; }
        public double EUR_USD_Bid { get; set; }

        public override string ToString()
        {
            return $"{City} {Office}";
        }
    }
}
