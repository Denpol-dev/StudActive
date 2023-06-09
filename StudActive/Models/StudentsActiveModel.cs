using Azure;
using Newtonsoft.Json;
using StudActive.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudActive.Models
{
    public class StudentsActiveModel
    {
        public Guid Id { get; set; }
        public string CouncilName { get; set; }
        public string Fio { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public string Sex { get; set; }
        public string MobilePhone { get; set; }
        public string VkLink { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid StudentId { get; set; }
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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid GroupId { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MobilePhoneNumber { get; set; }

        public async Task<bool> ChangeStudentActive()
        {
            var response = "";
            var url = Connection.url + "studactive/changestudentactive";
            var res = false;

            using (var httpClient = new HttpClient())
            {
                var uri = new UriBuilder(url);
                httpClient.DefaultRequestHeaders.Add("userid", App.userId.ToString());
                var st = JsonConvert.SerializeObject(this);
                var re = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");

                try
                {
                    var result = await httpClient.PutAsync(uri.Uri, re);
                    result.EnsureSuccessStatusCode();
                    response = await result.Content.ReadAsStringAsync();
                    res = true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return res;
        }
    }
}
