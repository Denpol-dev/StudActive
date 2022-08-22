using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudActive.Models
{
    public class DutyListModel
    {
        public Guid Id { get; set; }
        public string Fio { get; set; }
        public DateTime DateDuty { get; set; }
        public string IsDone { get; set; } //есть три состояния - "+" (продежурил), " " (не продежурил), "-" (пропустил)
        public bool IsVerification { get; set; } //подтверждение того, что участник сс продежурил от председа
    }
}
