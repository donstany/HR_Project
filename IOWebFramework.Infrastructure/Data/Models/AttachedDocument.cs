using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("attached_documents")]
    public class AttachedDocument
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        //[Column("attachment_type_id")]
        //public int AttachmentTypeId { get; set; }

        //[ForeignKey(nameof(AttachmentTypeId))]
        //public AttachmentType AttachmentType { get; set; }

        //[Column("activity_id")]
        //public int? activityId { get; set; }

        //[ForeignKey(nameof(activityId))]
        //public activity activity { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        //[Column("has_file")]
        //public bool HasFile { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("file_content_id")]
        public string FileContentId { get; set; }

        [Column("date_uploaded")]
        public DateTime? DateUploaded { get; set; }
    }
}
