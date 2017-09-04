using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Bank")]
    public partial class Bank
    {
        public Guid BankId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
