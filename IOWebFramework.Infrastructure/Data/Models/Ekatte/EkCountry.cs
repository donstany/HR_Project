using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("ek_countries")]
    public class EkCountry
    {
        [Key]
        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
