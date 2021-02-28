using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Entities
{
    public class HourleyRate
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("RateCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("RateUpdatedBy")]
        public User UpdatedBy { get; set; }
    }
}