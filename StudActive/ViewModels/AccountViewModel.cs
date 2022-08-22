using StudActive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Entities;
using System.Security.Cryptography;

namespace StudActive.ViewModels
{
    public class AccountViewModel
    {
        public AccountModel Login(LoginModel model)
        {
            using Context context = new();
            AccountModel account = new AccountModel();
            var user = context?.Users.FirstOrDefault(x => x.UserName == model.UserName);

            if (user.Password == model.Password)
            {
                var role = context.UserRoles.FirstOrDefault(x => x.UserRoleId == user.Role);
                string roleName = role.Name;
                account = new AccountModel
                {
                    UserName = user.UserName,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    Role = roleName,
                    Id = user.Id
                };
                return account;
            }
            return null;
        }

        public AccountModel LoginHash(LoginModel model)
        {
            using Context context = new();
            AccountModel account = new AccountModel();

            var user = context?.Users.FirstOrDefault(x => x.UserName == model.UserName);

            string pass = HashPass(model.Password, user.Salt);

            if (user.Hash == pass)
            {
                var role = context.UserRoles.FirstOrDefault(x => x.UserRoleId == user.Role);
                string roleName = role.Name;
                account = new AccountModel
                {
                    UserName = user.UserName,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    Role = roleName,
                    Id = user.Id
                };
                return account;
            }
            return null;
        }

        public bool CheckStudActive(LoginModel model)
        {
            using Context context = new();
            AccountModel account = new AccountModel();
            var user = context?.Users.FirstOrDefault(x => x.UserName == model.UserName);

            StudentsActiveModel studActives = new StudentsActiveModel();
            StudentsModel students = new StudentsModel();

            var student = context.Students.FirstOrDefault(x => x.UserId == user.Id);
            var studActive = context.StudentStudActives.FirstOrDefault(x => x.StudentId == student.StudentId);

            if (studActive != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string HashPass(string text, byte[] salt)
        {
            HashAlgorithm hash = new SHA256Managed();

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
    }
}
