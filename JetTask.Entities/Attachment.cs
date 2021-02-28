using System.ComponentModel.DataAnnotations;

namespace JetTask.Entities
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public int Location { get; set; }
        public AttachmentType Type { get; set; }
    }

    public enum AttachmentType
    {
        Document,
        Text,
        Image
    }
}