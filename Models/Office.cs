namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Office")]
    public partial class Office
    {
        public Guid OfficeId { get; set; }

        public Guid BankId { get; set; }

        public Guid CityId { get; set; }

        public string Address { get; set; }

        public string Contacts { get; set; }

        public string Tittle { get; set; }
    }
}
