using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("ek_streets")]
    public class EkStreet
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("ekatte")]
        public string Ekatte { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("date_from")]
        public DateTime DateFrom { get; set; }

        [Column("date_to")]
        public DateTime? DateTo { get; set; }
    }
}
