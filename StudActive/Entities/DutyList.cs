using System;

namespace StudActive.Entities
{
    public class DutyList
    {
        public Guid DutyListId { get; set; }
        public Guid DutyListCalendarId { get; set; }
        public string Fio { get; set; }
        public DateTime DateDuty { get; set; }
        public string IsDone { get; set; } //есть три состояния - "+" (продежурил), " " (не продежурил), "-" (пропустил)
        public bool IsVerification { get; set; } //подтверждение того, что участник сс продежурил от председа
    }
}
