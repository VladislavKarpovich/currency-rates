namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CrossRateView")]
    public partial class CrossRateView
    {
        [Key]
        [Column(Order = 0)]
        public Guid CrossRateId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid CurrencyIdFrom { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CurrencyIdTo { get; set; }

        public DateTime? DateTime { get; set; }

        [Key]
        [Column(Order = 3)]
        public double Bid { get; set; }

        [Key]
        [Column(Order = 4)]
        public double Ask { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid OfficeId { get; set; }

        [StringLength(5)]
        public string CurrencyFrom { get; set; }

        [StringLength(5)]
        public string CurrencyTo { get; set; }

        public string Address { get; set; }

        public string Contacts { get; set; }

        public string Tittle { get; set; }
    }
}
