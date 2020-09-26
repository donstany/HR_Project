using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Специалности")]
    [Table("d_classifiers")]
    public class Classifier
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    
        [Required]
        public int ParentId { get; set; }
      
        [StringLength(200)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
