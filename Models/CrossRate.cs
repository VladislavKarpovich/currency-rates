namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CrossRate")]
    public partial class CrossRate
    {
        public Guid CrossRateId { get; set; }

        public Guid CurrencyIdFrom { get; set; }

        public Guid CurrencyIdTo { get; set; }

        public DateTime? DateTime { get; set; }

        public double Bid { get; set; }

        public double Ask { get; set; }

        public Guid OfficeId { get; set; }
    }
}
