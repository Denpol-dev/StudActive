using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudActive.Models;
using StudActive.Entities;
using System.Security.Cryptography;

namespace StudActive.ViewModels
{
    public class StudentsViewModel
    {
        /// <summary>
        /// Получить всех студентов из студсовета авторизованного пользователя
        /// </summary>
        public List<StudentsActiveModel> GetStudentsActiveIsNotArchive(Guid userId)
        {
            using var context = new Context();
            List<StudentsActiveModel> result = new List<StudentsActiveModel>();
            var userStudent = context.Students.FirstOrDefault(x => x.UserId == userId);
            Guid studentId = userStudent.StudentId;

            var allStudents = context.StudentStudActives.ToList();
            var studentLogin = allStudents.FirstOrDefault(x => x.StudentId == studentId);
            var studentActives = context.StudentStudActives.Where(x => x.StudentCouncilId == studentLogin.StudentCouncilId && x.IsArchive != true).ToList();
            var students = context.Students.ToList();
            var groups = context.Groups.ToList();
            var roles = context.RoleStudActives.ToList();

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
                        roleName = "Рядовой";
                    else if (roleName == "Chairman")
                        roleName = "Председатель";
                    else if (roleName == "ViceChairman")
                        roleName = "Зам. председателя";

                    var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                    long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                    result.Add(new StudentsActiveModel
                    {
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
                    });
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<StudentsActiveModel> GetStudentsActiveIsArchiveToo(Guid userId)
        {
            using var context = new Context();
            List<StudentsActiveModel> result = new List<StudentsActiveModel>();
            var userStudent = context.Students.FirstOrDefault(x => x.UserId == userId);
            Guid studentId = userStudent.StudentId;

            var allStudents = context.StudentStudActives.ToList();
            var studentLogin = allStudents.FirstOrDefault(x => x.StudentId == studentId);
            var studentActives = context.StudentStudActives.Where(x => x.StudentCouncilId == studentLogin.StudentCouncilId).ToList();
            var students = context.Students.ToList();
            var groups = context.Groups.ToList();
            var roles = context.RoleStudActives.ToList();

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
                        roleName = "Рядовой";
                    else if (roleName == "Chairman")
                        roleName = "Председатель";
                    else if (roleName == "ViceChairman")
                        roleName = "Зам. председателя";

                    var council = context.StudentCouncils.FirstOrDefault(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                    long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                    result.Add(new StudentsActiveModel
                    {
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
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);

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

        public bool CreateFullNewStudentActive(RegistrationStudActiveModel regModel)
        {
            using Context context = new Context();
            bool result = true;
            StudentStudActive studAct = new();
            Student stud = new();
            try
            {
                //Заполняем добавление в таблицу Students
                stud.FirstName = regModel.FirstName;
                stud.MiddleName = regModel.MiddleName;
                stud.LastName = regModel.LastName;
                stud.GroupId = regModel.GroupId;

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
    }
}
