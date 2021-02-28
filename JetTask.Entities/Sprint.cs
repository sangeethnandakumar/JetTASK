using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Entities
{
    public class Sprint
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set; }
        public Project Project { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public SprintStatus SprintStatus { get; set; }

        [ForeignKey("ReleaseId")]
        public Release Release { get; set; }

        public bool IsActive { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("SprintCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("SprintUpdatedBy")]
        public User UpdatedBy { get; set; }
    }

    public enum SprintStatus
    {
        SprintInactive,
        SprintActive,
        SprintClosed
    }
}