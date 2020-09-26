using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Сертификати")]
    [Table("d_certificates")]
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Дата на издаване")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Валиден до")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Регистрационен номер")]
        public string RegNumber { get; set; }

        [Display(Name = "Оценка")]
        [StringLength(50)]
        public string Grade { get; set; }

        public List<CertificateAttachment> CertificateAttachments { get; set; } = new List<CertificateAttachment>();

        [Display(Name = "Ниво")]
        public string Level { get; set; }

        public string FileContentId { get; set; }

        [DisplayName("Име на сертификат")]
        public int CertificateNameIssuerId { get; set; }

        [ForeignKey(nameof(CertificateNameIssuerId))]
        public CertificateNameIssuer CertificateNameIssuer { get; set; }

       
        [DisplayName("Тип на сертификат")]
        public int CertificateTypeId { get; set; }

        [ForeignKey(nameof(CertificateTypeId))]
        public CertificateType CertificateType { get; set; }

        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }

        public bool IsDeleted { get; set; }
    }
}
