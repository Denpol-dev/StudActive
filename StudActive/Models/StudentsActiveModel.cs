using System;

namespace StudActive.Models
{
    public class StudentsActiveModel
    {
        public Guid Id { get; set; }
        public string CouncilName { get; set; }
        public string Fio { get; set; }
        public string GroupName { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public string Role { get; set; }
        public string Sex { get; set; }
        public string MobilePhone { get; set; }
        public string VkLink { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class RegistrationStudActiveModel
    {
        public Guid StudActiveId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid? RoleActive { get; set; }
        public string VkLink { get; set; }
        public Guid StudentId { get; set; }
        public Guid StudentCouncilId { get; set; }
        public Guid GroupId { get; set; }
    }
}
