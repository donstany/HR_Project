using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.Dossier
{
    public class CertificateViewModel
    {
        public int Id { get; set; }

        [DisplayName("Име на сертификат")]
        [Range(1, int.MaxValue, ErrorMessage = FieldIsMandatory)]
        public int CertificateNameIssuerId { get; set; }

        [DisplayName("Тип на сертификат")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int CertificateTypeId { get; set; }

        [DisplayName("Регистрационен номер")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string RegNumber { get; set; }

        [DisplayName("Оценка")]
        [StringLength(GradeDiplomaMaxLength, ErrorMessage = GradeCertificateMsg)]
        public string Grade { get; set; }

        [DisplayName("Ниво")]
        public string Level { get; set; }

        [DisplayName("Прикачи документи")]
        public string FileContentId { get; set; }

        public int PersonId { get; set; }

        [DisplayName("Дата на издаване")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата на изтичане")]
        public DateTime? DateEnd { get; set; }

        public bool IsDeleted { get; set; }

        public BreadcrumbInfoModel BreadcrumbInfo { get; set; } = new BreadcrumbInfoModel();
    }
}

