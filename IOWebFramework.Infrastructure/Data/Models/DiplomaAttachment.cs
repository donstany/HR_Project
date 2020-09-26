using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("diploma_attachments")]
    public class DiplomaAttachment
    {
        [Column("diploma_id")]
        public int DiplomaId { get; set; }

        [ForeignKey(nameof(DiplomaId))]
        public Diploma Diploma { get; set; }

        [Column("attached_document_id")]
        public long AttachedDocumentId { get; set; }

        [ForeignKey(nameof(AttachedDocumentId))]
        public AttachedDocument AttachedDocument { get; set; }
    }
}
