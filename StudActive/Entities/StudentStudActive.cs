using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudActive.Entities
{
    public partial class StudentStudActive
    {
        [Key]
        public Guid StudActiveId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid? RoleActive { get; set; }
        public string VkLink { get; set; }
        public Guid StudentId { get; set; }
        public Guid StudentCouncilId { get; set; }
    }
}