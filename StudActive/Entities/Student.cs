using System;

#nullable disable

namespace StudActive.Entities
{
    public partial class Student
    {
        public Guid StudentId { get; set; }
        public Guid? GroupId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MobilePhoneNumber { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
