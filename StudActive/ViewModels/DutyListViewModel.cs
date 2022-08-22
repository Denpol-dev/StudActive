using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Models;
using StudActive.Entities;

namespace StudActive.ViewModels
{
    public class DutyListViewModel
    {
        /// <summary>
        /// Получение актуального графика дежурств
        /// </summary>
        public List<DutyListModel> GetDutyList()
        {
            using (var context = new Context())
            {
                DateTime today = DateTime.Now;

                List<DutyListModel> result = new List<DutyListModel>();
                var dutyListCalendars = context.DutyListCalendars.ToList();
                var createDatesDutyList = dutyListCalendars.Select(x => x.CreateDate).ToList();
                createDatesDutyList.Sort();
                var actualDutyListCalendar = dutyListCalendars.FirstOrDefault(x => x.CreateDate == createDatesDutyList.Last());

                var actualDutyLists = context.DutyLists.Where(x => x.DutyListCalendarId == actualDutyListCalendar.DutyListCalendarId).ToList();

                foreach(var dutyList in actualDutyLists)
                {
                    result.Add(new DutyListModel
                    {
                        Id = dutyList.DutyListId,
                        Fio = dutyList.Fio,
                        DateDuty = dutyList.DateDuty,
                        IsDone = dutyList.IsDone,
                        IsVerification = dutyList.IsVerification
                    });
                }
                return result;
            }
        }

        public bool SaveDuty(Guid Id)
        {
            using (var context = new Context())
            {
                var dutyListSave = context.DutyLists.Where(x => x.DutyListId == Id).FirstOrDefault();

                dutyListSave.IsDone = "Продежурил";

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
