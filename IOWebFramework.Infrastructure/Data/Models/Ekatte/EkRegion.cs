using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("ek_regions")]
    public class EkRegion
    {
        [Column("raion")]
        public string Raion { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("document")]
        public string Document { get; set; }

        [Column("settlement_id")]
        public int? SettlementId { get; set; }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(SettlementId))]
        public EkEkatte Settlement { get; set; }
    }
}
