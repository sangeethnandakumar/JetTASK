using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetTask.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public Comment ParentComment { get; set; }
        public string Content { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("CommentCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("CommentUpdatedBy")]
        public User UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}