using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetTask.Entities
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int Assignee { get; set; }
        public int Reporter { get; set; }
        public ICollection<Label> Labels { get; set; }
        public Project Project { get; set; }
        public TimeSpan Estimate { get; set; }
        public Sprint Sprint { get; set; }
        public TaskPriority Priority { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public AppVersion FixVersion { get; set; }
        public TicketType Type { get; set; }
        public ICollection<Ticket> AttachedTickets { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<User> Watchers { get; set; }
        public HourleyRate HourleyRate { get; set; }

        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("TicketCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("TicketUpdatedBy")]
        public User UpdatedBy { get; set; }
    }

    public enum TaskPriority
    {
        Lowest,
        Low,
        Medium,
        High,
        Highest
    }

    public enum TicketType
    {
        Bug,
        Task,
        Epic
    }
}