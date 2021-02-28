using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetTask.Entities
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("LabelCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("LabelUpdatedBy")]
        public User UpdatedBy { get; set; }
    }
}