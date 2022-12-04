using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Models;
using StudActive.Entities;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;

namespace StudActive.ViewModels
{
    public class StudentsViewModel
    {
        /// <summary>
        /// Получить студентов из студсовета авторизованного пользователя, которые не лежат в архиве
        /// </summary>
        public static async Task<List<StudentsActiveModel>> GetStudentsActiveIsNotArchive(Guid userId)
        {
            using var context = new Context();
            List<StudentsActiveModel> result = new List<StudentsActiveModel>();
            var userStudent = context.Students.FirstOrDefault(x => x.UserId == userId);
            Guid studentId = userStudent.StudentId;

            List<StudentStudActive> allStudents = new();
            List<StudentStudActive> studentActives = new();
            List<Student> students = new();
            List<Group> groups = new();
            List<RoleStudActive> roles = new();

            await context.StudentStudActives.ForEachAsync(context => { allStudents.Add(context); });
            var studentLogin = allStudents.FirstOrDefault(x => x.StudentId == studentId);
            await context.StudentStudActives.Where(x => x.StudentCouncilId == studentLogin.StudentCouncilId && x.IsArchive != true).ForEachAsync(context => { studentActives.Add(context); });
            await context.Students.ForEachAsync(context=> { students.Add(context); });
            await context.Groups.ForEachAsync(context => { groups.Add(context); });
            await context.RoleStudActives.ForEachAsync(context => { roles.Add(context); });

            if (studentActives != null)
            {
                foreach (var studActiv in studentActives)
                {
                    var student = students.FirstOrDefault(x => x.StudentId == studActiv.StudentId);
                    var groupId = student.GroupId;
                    var group = groups.FirstOrDefault(x => x.GroupId == groupId);

                    string sex = student.Sex.ToString();

                    if (sex == "0")
                        sex = "Ж";
                    else
                        sex = "М";

                    Guid roleId = studActiv.RoleActive.GetValueOrDefault();
                    var role = roles.FirstOrDefault(x => x.Id == roleId);
                    string roleName = role.Name;

                    if (roleName == "Member")
                        roleName = "Участник";
                    else if (roleName == "Chairman")
                        roleName = "Председатель";
                    else if (roleName == "ViceChairman")
                        roleName = "Зам. председателя";

                    var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                    long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                    result.Add(new StudentsActiveModel
                    {
                        Id = studActiv.StudActiveId,
                        Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                        EntryDate = studActiv.EntryDate,
                        LeavingDate = studActiv.LeavingDate,
                        ReEntryDate = studActiv.ReEntryDate,
                        IsArchive = studActiv.IsArchive,
                        RoleId = role.Id,
                        Role = roleName,
                        Sex = sex,
                        MobilePhone = string.Format("{0:+7 (###) ###-##-##}", phoneString),
                        BirthDate = student.BirthDate,
                        VkLink = "https://vk.com/" + studActiv.VkLink,
                        GroupId = group.GroupId,
                        GroupName = group.Name + "-" + group.CourseNumber + group.Number,
                        CouncilName = council.Name
                    });
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Получить всех студентов из студсовета авторизованного пользователя
        /// </summary>
        public static async Task<List<StudentsActiveModel>> GetStudentsActiveIsArchiveToo(Guid userId)
        {
            using var context = new Context();
            List<StudentsActiveModel> result = new List<StudentsActiveModel>();
            var userStudent = context.Students.FirstOrDefault(x => x.UserId == userId);
            Guid studentId = userStudent.StudentId;

            List<StudentStudActive> allStudents = new();
            List<StudentStudActive> studentActives = new();
            List<Student> students = new();
            List<Group> groups = new();
            List<RoleStudActive> roles = new();

            await context.StudentStudActives.ForEachAsync(context => { allStudents.Add(context); });
            var studentLogin = allStudents.FirstOrDefault(x => x.StudentId == studentId);
            await context.StudentStudActives.Where(x => x.StudentCouncilId == studentLogin.StudentCouncilId).ForEachAsync(context => { studentActives.Add(context); });
            await context.Students.ForEachAsync(context => { students.Add(context); });
            await context.Groups.ForEachAsync(context => { groups.Add(context); });
            await context.RoleStudActives.ForEachAsync(context => { roles.Add(context); });

            if (studentActives != null)
            {
                foreach (var studActiv in studentActives)
                {
                    var student = students.FirstOrDefault(x => x.StudentId == studActiv.StudentId);
                    var groupId = student.GroupId;
                    var group = groups.FirstOrDefault(x => x.GroupId == groupId);

                    string sex = student.Sex.ToString();

                    if (sex == "0")
                        sex = "Ж";
                    else
                        sex = "М";

                    Guid roleId = studActiv.RoleActive.GetValueOrDefault();
                    var role = roles.FirstOrDefault(x => x.Id == roleId);
                    string roleName = role.Name;

                    if (roleName == "Member")
                        roleName = "Участник";
                    else if (roleName == "Chairman")
                        roleName = "Председатель";
                    else if (roleName == "ViceChairman")
                        roleName = "Зам. председателя";

                    var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                    long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                    result.Add(new StudentsActiveModel
                    {
                        Id = studActiv.StudActiveId,
                        Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                        EntryDate = studActiv.EntryDate,
                        LeavingDate = studActiv.LeavingDate,
                        ReEntryDate = studActiv.ReEntryDate,
                        IsArchive = studActiv.IsArchive,
                        RoleId = role.Id,
                        Role = roleName,
                        Sex = sex,
                        MobilePhone = string.Format("{0:+7 (###) ###-##-##}", phoneString),
                        BirthDate = student.BirthDate,
                        VkLink = "https://vk.com/" + studActiv.VkLink,
                        GroupId = group.GroupId,
                        GroupName = group.Name + "-" + group.CourseNumber + group.Number,
                        CouncilName = council.Name
                    });
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public StudentsActiveModel GetStudentActive(Guid userId)
        {
            using var context = new Context();
            StudentsActiveModel result = new StudentsActiveModel();
            var userStudents = context.Students.FirstOrDefault(x => x.UserId == userId);
            Guid studentId = userStudents.StudentId;

            var groups = context.Groups.ToList();
            var studActiv = context.StudentStudActives.FirstOrDefault(x => x.StudentId == studentId);
            var students = context.Students.ToList();

            if (studActiv != null)
            {
                var student = students.FirstOrDefault(x => x.StudentId == studActiv.StudentId);
                var groupId = student.GroupId;
                var group = groups.FirstOrDefault(x => x.GroupId == groupId);

                string sex = student.Sex.ToString();

                if (sex == "0")
                    sex = "Ж";
                else
                    sex = "М";

                Guid roleId = studActiv.RoleActive.GetValueOrDefault();
                var role = context.RoleStudActives.FirstOrDefault(x => x.Id == roleId);
                string roleName = role.Name;

                if (roleName == "Member")
                    roleName = "Рядовой";
                else if (roleName == "Chairman")
                    roleName = "Председатель";
                else if (roleName == "ViceChairman")
                    roleName = "Зам. председателя";

                var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                result = new StudentsActiveModel
                {
                    Id = studActiv.StudentId,
                    Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                    EntryDate = studActiv.EntryDate,
                    LeavingDate = studActiv.LeavingDate,
                    ReEntryDate = studActiv.ReEntryDate,
                    IsArchive = studActiv.IsArchive,
                    Role = roleName,
                    Sex = sex,
                    MobilePhone = string.Format("{0:+7 (###) ###-##-##}", phoneString),
                    BirthDate = student.BirthDate,
                    VkLink = "https://vk.com/" + studActiv.VkLink,
                    GroupName = group.Name + "-" + group.CourseNumber + group.Number,
                    CouncilName = council.Name
                };
                return result;
            }
            return null;
        }

        public string GetPass()
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < 6)
            {
                char c = (char)r.Next(33, 125);
                if (char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }

        public Tuple<string, byte[]> GetHashPass(string plainText)
        {
            byte[] salt;
            Random random = new Random();
            int saltSize = random.Next(4, 8);
            salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string text = plainText;
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] textBytesSalt = new byte[textBytes.Length + salt.Length];

            for (int i = 0; i < textBytes.Length; i++)
                textBytesSalt[i] = textBytes[i];

            for (int i = 0; i < salt.Length; i++)
                textBytesSalt[textBytes.Length + i] = salt[i];

            HashAlgorithm hash = new SHA256Managed();

            byte[] hashSaltBytes = hash.ComputeHash(textBytesSalt);
            string hastValue = Convert.ToBase64String(hashSaltBytes);

            return Tuple.Create(hastValue, salt);
        }

        public bool ChangePass(string plainText, Guid id)
        {
            Tuple<string, byte[]> saltHash = GetHashPass(plainText);

            using var context = new Context();
            var authorizedUser = context.Users.Where(x => x.Id == id).FirstOrDefault();

            string hashSalt = saltHash.Item1;
            byte[] salt = saltHash.Item2;

            authorizedUser.Hash = hashSalt;
            authorizedUser.Salt = salt;

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

        /// <summary>
        /// Генерация хэшей для всех пользователей (СТАРОЕ!)
        /// </summary>
        public bool CreateHashPass()
        {
            using var context = new Context();
            var users = context.Users.Where(u => u.Hash == null);

            foreach(var user in users)
            {
                Tuple<string, byte[]> saltHash = GetHashPass(user.Password);
                string hashSalt = saltHash.Item1;
                byte[] salt = saltHash.Item2;
                user.Hash= hashSalt;
                user.Salt = salt;
            }

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

        public List<StudentsModel> GetNonRegistratedStrudents()
        {
            using var context = new Context();
            List<StudentsModel> result = new List<StudentsModel>();
            var students = context.Students.ToList();
            var studentsActive = context.StudentStudActives.ToList();
            List<Guid> studentsActiveIds = new();
            studentsActive.ForEach(s => studentsActiveIds.Add(s.StudentId));
            var groups = context.Groups.ToList();

            result.Add(new StudentsModel
            {
                Fio = "Пусто",
                BirthDate = null
            });

            foreach (var student in students)
            {
                if (!studentsActiveIds.Contains(student.StudentId))
                {
                    var group = groups.FirstOrDefault(x => x.GroupId == student.GroupId);
                    result.Add(new StudentsModel
                    {
                        Id = student.StudentId,
                        Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                        GroupId = group.GroupId,
                        GroupNumber = group.Name + "-" + group.CourseNumber + group.Number,
                        Sex = student.Sex,
                        BirthDate = student.BirthDate.Value,
                        MobilePhone = student.MobilePhoneNumber
                    });
                }                
            }
            return result;
        }
        public List<StudentsModel> GetAllStudents()
        {
            using var context = new Context();
            List<StudentsModel> result = new List<StudentsModel>();
            var students = context.Students.ToList();
            var groups = context.Groups.ToList();

            result.Add(new StudentsModel
            {
                Fio = "Пусто",
                BirthDate = null
            });

            foreach (var student in students)
            {
                var group = groups.FirstOrDefault(x => x.GroupId == student.GroupId);
                result.Add(new StudentsModel
                {
                    Id = student.StudentId,
                    Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                    GroupId = group.GroupId,
                    GroupNumber = group.Name + "-" + group.CourseNumber + group.Number,
                    Sex = student.Sex,
                    BirthDate = student.BirthDate.Value,
                    MobilePhone = student.MobilePhoneNumber
                });
            }
            return result;
        }

        public List<GroupsModel> GetAllGroups()
        {
            using var context = new Context();
            List<GroupsModel> result = new List<GroupsModel>();
            var groups = context.Groups.ToList();

            foreach (var group in groups)
            {
                var groupName = group.Name + "-" + group.CourseNumber + group.Number;
                result.Add(new GroupsModel
                {
                    GroupId = group.GroupId,
                    GroupName = groupName
                });
            }
            return result;
        }

        public List<RolesStudActiveModel> GetAllRolesStudActiveModel()
        {
            using var context = new Context();
            List<RolesStudActiveModel> result = new List<RolesStudActiveModel>();
            var roles = context.RoleStudActives.ToList();

            foreach (var role in roles)
            {
                result.Add(new RolesStudActiveModel
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    NameRU = role.NameRU
                });
            }
            return result;
        }

        /// <summary>
        /// Создание студента СС из уже зарегистрированного студента
        /// </summary>
        /// <param name="regModel"></param>
        /// <returns></returns>
        public bool CreateAgainStudentActive(RegistrationStudActiveModel regModel)
        {
            using Context context = new Context();
            bool result = true;
            StudentStudActive studAct = new();
            Student stud = new();
            try
            {
                //Заполняем добавление в таблицу StudentStudActives
                studAct.StudActiveId = Guid.NewGuid();
                studAct.EntryDate = regModel.EntryDate;
                studAct.IsArchive = regModel.IsArchive;
                studAct.RoleActive = regModel.RoleActive;
                studAct.VkLink = regModel.VkLink;
                studAct.StudentId = regModel.StudentId;
                studAct.StudentCouncilId = regModel.StudentCouncilId;
                context.StudentStudActives.Add(studAct);
                context.SaveChanges();

                //Изменяем таблицу Students при условии, что изменилась группа студента или имя
                var student = context.Students
                    .Where(x => x.StudentId == regModel.StudentId)
                    .FirstOrDefault();
                if (student.GroupId != regModel.GroupId)
                {
                    student.GroupId = regModel.GroupId;
                    context.SaveChanges();
                }
                if (student.FirstName != regModel.FirstName)
                {

                }
            }
            catch
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// Полное создание нового студента в систему
        /// </summary>
        /// <param name="regModel"></param>
        /// <returns></returns>
        public bool CreateFullNewStudentActive(RegistrationStudActiveModel regModel)
        {
            using Context context = new Context();
            bool result = true;
            StudentStudActive studAct = new();
            Student stud = new();
            User user = new();
            try
            {
                string fio = regModel.LastName + regModel.FirstName[0] + regModel.MiddleName[0];
                Random r = new();
                int rand = r.Next(10, 99);
                int randPass = r.Next(5, 7);
                string username = Transliteration.RuEnTransliteration(fio.ToUpper()) + rand.ToString();
                string pass = AccountViewModel.GetPass(randPass);
                Tuple<string, byte[]> HashSalt = GetHashPass(pass);
                //Заполняем таблицу User
                user.Id = Guid.NewGuid();
                user.UserName = username.ToLower();
                user.FirstName = regModel.FirstName;
                user.MiddleName = regModel.MiddleName;
                user.LastName = regModel.LastName;
                user.Hash = HashSalt.Item1;
                user.Salt = HashSalt.Item2;
                user.Role = Guid.Parse("c4220640-6a53-4a3c-b681-e48e845d658f");
                try
                {
                    FileStream fs = new FileStream(@"Content\NewLoginPassword.txt", FileMode.CreateNew);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(username + " " + pass);
                }
                catch { }
                MessageBox.Show(username + " " + pass);

                //Заполняем добавление в таблицу Students
                stud.StudentId = Guid.NewGuid();
                stud.GroupId = regModel.GroupId;
                stud.FirstName = regModel.FirstName;
                stud.MiddleName = regModel.MiddleName;
                stud.LastName = regModel.LastName;
                stud.Sex= regModel.Sex;
                stud.BirthDate= regModel.BirthDate;
                stud.MobilePhoneNumber = regModel.MobilePhoneNumber;
                stud.UserId = user.Id;

                //Заполняем добавление в таблицу StudentStudActives
                studAct.StudActiveId = Guid.NewGuid();
                studAct.EntryDate = regModel.EntryDate;
                studAct.IsArchive = false;
                studAct.StudentId = stud.StudentId;
                studAct.StudentCouncilId = regModel.StudentCouncilId;
                studAct.VkLink = regModel.VkLink;
                studAct.RoleActive = regModel.RoleActive;

                //Сохранение
                context.Users.Add(user);
                context.Students.Add(stud);
                context.StudentStudActives.Add(studAct);
                context.SaveChanges();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public Guid? GetStudentCouncilId(Guid studentId)
        {
            using var context = new Context();
            var studActiv = context.StudentStudActives.FirstOrDefault(x => x.StudentId == studentId);
            var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);
            return council.StudentCouncilId;
        }

        /// <summary>
        /// Изменение архивности студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public static async Task<bool> ChangeIsArchive(Guid studentId)
        {
            bool res = false;
            using var context = new Context();

            var student = await context.StudentStudActives.Where(x => x.StudActiveId == studentId).FirstOrDefaultAsync();

            student.IsArchive = student.IsArchive != true;
            try
            {
                context.SaveChanges();
                res = true;
            }
            catch { }
            return res;
        }
    }
}
