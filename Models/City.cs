namespace Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("City")]
    public partial class City
    {
        public Guid CityId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
