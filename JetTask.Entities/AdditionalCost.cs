using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetTask.Entities
{
    public class AdditionalCost
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("CostCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("CostUpdatedBy")]
        public User UpdatedBy { get; set; }
    }
}