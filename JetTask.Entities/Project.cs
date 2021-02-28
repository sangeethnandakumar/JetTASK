using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JetTask.Entities
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectLogo { get; set; }
        public string ProjectPrimaryColor { get; set; }
        public string ProjectSecondaryColor { get; set; }
        public string ProjectTertiaryColor { get; set; }
        public Client Client { get; set; }

        public ICollection<Sprint> Sprints { get; set; }
        public ICollection<AdditionalCost> AdditionalCosts { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }

        [ForeignKey("UserId")]
        public int? CreatedById { get; set; }

        [ForeignKey("UserId")]
        public int? UpdatedById { get; set; }
    }
}