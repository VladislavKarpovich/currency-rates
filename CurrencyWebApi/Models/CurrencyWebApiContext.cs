using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CurrencyWebApi.Models
{
    public class CurrencyWebApiContext : DbContext
    {
    
        public CurrencyWebApiContext() : base("name=CurrencyWebApi")
        {

        }

        public DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}
