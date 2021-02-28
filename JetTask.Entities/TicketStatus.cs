using System.ComponentModel.DataAnnotations;

namespace JetTask.Entities
{
    public class TicketStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Resolution Resolution { get; set; }
    }

    public enum Resolution
    {
        Open,
        Dev,
        Review,
        Done,
        Hold,
        Closed
    }
}