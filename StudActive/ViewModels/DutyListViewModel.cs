using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Models;
using StudActive.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudActive.ViewModels
{
    public class DutyListViewModel
    {
        /// <summary>
        /// Получение актуального графика дежурств
        /// </summary>
        public static async Task<List<DutyListModel>> GetDutyList()
        {
            using (var context = new Context())
            {
                DateTime today = DateTime.Now;
                List<DutyListCalendar> dutyListCalendars = new();
                List<DutyList> actualDutyLists = new();
                List<DutyListModel> result = new List<DutyListModel>();
                await context.DutyListCalendars.ForEachAsync(context => { dutyListCalendars.Add(context); });
                var createDatesDutyList = dutyListCalendars.Select(x => x.CreateDate).ToList();
                createDatesDutyList.Sort();
                var actualDutyListCalendar = dutyListCalendars.FirstOrDefault(x => x.CreateDate == createDatesDutyList.Last());

                await context.DutyLists.Where(x => x.DutyListCalendarId == actualDutyListCalendar.DutyListCalendarId).ForEachAsync(context => { actualDutyLists.Add(context); });

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

        public bool MissedDuty(Guid Id)
        {
            using (var context = new Context())
            {
                var dutyListSave = context.DutyLists.Where(x => x.DutyListId == Id).FirstOrDefault();

                dutyListSave.IsDone = "Пропустил";

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
