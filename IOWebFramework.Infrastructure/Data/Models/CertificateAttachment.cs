using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Table("certificate_attachments")]
    public class CertificateAttachment
    {
        [Column("certificate_id")]
        public int CertificateId { get; set; }

        [ForeignKey(nameof(CertificateId))]
        public Certificate Certificate { get; set; }

        [Column("attached_document_id")]
        public long AttachedDocumentId { get; set; }

        [ForeignKey(nameof(AttachedDocumentId))]
        public AttachedDocument AttachedDocument { get; set; }
    }
}
