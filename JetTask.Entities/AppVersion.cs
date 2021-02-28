using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetTask.Entities
{
    public class AppVersion
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Build { get; set; }
        public DateTime? UserCreatedOn { get; set; }
        public DateTime? UserUpdatedOn { get; set; }

        [ForeignKey("VersionCreatedBy")]
        public User CreatedBy { get; set; }

        [ForeignKey("VersionUpdatedBy")]
        public User UpdatedBy { get; set; }
    }
}