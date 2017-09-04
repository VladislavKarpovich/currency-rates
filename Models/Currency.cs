namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Currency")]
    public partial class Currency
    {
        public Guid CurrencyId { get; set; }

        [StringLength(5)]
        public string Name { get; set; }
    }
}
