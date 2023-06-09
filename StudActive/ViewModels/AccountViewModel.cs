using StudActive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Entities;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using StudActive.Services;

namespace StudActive.ViewModels
{
    public class AccountViewModel
    {
        public async Task<AccountModel> LoginHash(LoginModel model)
        {
            var response = "";
            var url = Connection.url + "user/login";
            var res = false;

            using (var httpClient = new HttpClient())
            {
                var uri = new UriBuilder(url);
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
                requestMessage.Headers.Add("username", model.UserName);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model), null, "application/json");

                try
                {
                    var result = await httpClient.SendAsync(requestMessage);
                    result.EnsureSuccessStatusCode();
                    response = await result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return new AccountModel();
                }
            }

            var account = JsonConvert.DeserializeObject<AccountModel>(response);
            return account;

        }

        public bool CheckStudActive(LoginModel model)
        {
            try
            {
                using Context context = new();
                var account = new AccountModel();
                var user = context?.Users.FirstOrDefault(x => x.UserName == model.UserName);

                var studActives = new StudentsActiveModel();
                var students = new StudentsModel();

                var student = context.Students.FirstOrDefault(x => x.UserId == user.Id);
                var studActive = context.StudentStudActives.FirstOrDefault(x => x.StudentId == student.StudentId);

                bool result = studActive != null;
                return result;
            }
            catch
            {
                return false;
            }
        }

        public string HashPass(string text, byte[] salt)
        {
            var hash = new SHA256Managed();

            byte[] saltBytes = salt;
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            //string textSalt = text + salt;
            //byte[] textBytesSalt = Encoding.UTF8.GetBytes(textSalt);
            byte[] textBytesSalt = new byte[textBytes.Length + saltBytes.Length];

            for (int i = 0; i < textBytes.Length; i++)
                textBytesSalt[i] = textBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                textBytesSalt[textBytes.Length + i] = saltBytes[i];

            byte[] hashSaltBytes = hash.ComputeHash(textBytesSalt);

            string hastValue = Convert.ToBase64String(hashSaltBytes);
            return hastValue;
        }

        public static string GetPass(int x)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < x)
            {
                char c = (char)r.Next(33, 125);
                if (char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }
    }
}
