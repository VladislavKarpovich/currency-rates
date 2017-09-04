namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OfficeView")]
    public partial class OfficeView
    {
        [Key]
        [Column(Order = 0)]
        public Guid OfficeId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid BankId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CityId { get; set; }

        public string Address { get; set; }

        public string Contacts { get; set; }

        public string Tittle { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string Bank { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string City { get; set; }
    }
}
