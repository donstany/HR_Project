using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("training_attachments")]
    public class TrainingAttachment
    {
        [Column("training_id")]
        public int TrainingId { get; set; }

        [ForeignKey(nameof(TrainingId))]
        public Training Training { get; set; }

        [Column("attached_document_id")]
        public long AttachedDocumentId { get; set; }

        [ForeignKey(nameof(AttachedDocumentId))]
        public AttachedDocument AttachedDocument { get; set; }
    }
}
