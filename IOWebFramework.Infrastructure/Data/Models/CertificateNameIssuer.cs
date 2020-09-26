using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Сертификати")]
    [Table("d_certificate_name_issuer")]
    public class CertificateNameIssuer
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
