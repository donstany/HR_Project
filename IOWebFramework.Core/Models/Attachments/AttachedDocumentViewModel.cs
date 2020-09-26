using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace IOWebFramework.Core.Models.Attachments
{
    public class AttachedDocumentViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// ID на документа, за който се отнася този прикачен документ
        /// </summary>
        [JsonProperty("DocumentId")]
        public int DocumentId { get; set; }

        /// <summary>
        /// Тип на документа, за който се отнася този прикачен документ(Diploma, Certificate....)
        /// </summary>
        [JsonProperty("DocumentType")]
        public int DocumentType { get; set; }

        //[JsonProperty("attachmentTypeId")]
        //[Display(Name = "Тип на прикачения документ")]
        //public int AttachmentTypeId { get; set; }

        //[JsonProperty("attachmentTypeName")]
        //public string AttachmentTypeName { get; set; }

        [JsonProperty("number")]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        [JsonProperty("date")]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        //[JsonProperty("hasFile")]
        //[Display(Name = "Съществува файл")]
        //public bool HasFile { get; set; }

        [JsonProperty("description")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [JsonProperty("isActive")]
        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

        [JsonProperty("fileContentId")]
        public string FileContentId { get; set; }

        //public DateTime? DateToCompare { get; set; }

        //public int MonthsToSubtract { get; set; }

        public bool IsEditMode { get; set; }

        [JsonProperty("dateUploaded")]
        public DateTime? DateUploaded { get; set; }
    }
}