using System;

namespace StudActive.Models
{
    public class StudentsModel
    {
        public Guid Id { get; set; }
        public string Fio { get; set; }
        public Guid GroupId { get; set; }
        public string GroupNumber { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MobilePhone { get; set; }
    }

    public class GroupsModel
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
    }

    public class RolesStudActiveModel
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string NameRU { get; set; }
    }
}
