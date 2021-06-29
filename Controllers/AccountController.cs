using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BuildingCompany.Models;
using BuildingCompany.Models.Entityes;
using BuildingCompany.Models.HelperEntityes;
using Microsoft.AspNetCore.Mvc;

namespace BuildingCompany.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Logout()
        {
            DbConnection.SetDefaultInstanse();
            DbConnection.Args = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(DB_User user)
        {
            //Получение значения логина и пароля с Login View
            string[] args = new string[2] { user.Login, user.Password };

            try
            {
                //Получение текущего контекста БД
                DB_Context context = DbConnection.GetInstance();
                //Получение пользователя с заданым логином и паролем если таков существует
                IEnumerable<Members> member = context.Members.Where(e => e.email == args[0]).Where(e => e.password_hash == args[1]).ToList();
                //TODO change to hash at finish
                //IEnumerable<Members> member = context.Members.Where(e => e.Email == args[0]).Where(e => e.Password_hash == GetHash(args[1])).ToList();
                DbConnection.CurrentMember = member.First();

                //Определение роли пользователя для переподключения к БД с нужными правами
                switch (DbConnection.CurrentMember.role_id)
                {
                    case 1:
                        //Установление роли Бухгалетра
                        DbConnection.UserRole = Role.Accountant;
                        //Переподключение к БД с нужной ролью
                        DbConnection.ResetUser(new string[] { "Accountant", "11111" });
                        break;
                    case 2:
                        //Установление роли Рабочего
                        DbConnection.UserRole = Role.Workers;
                        DbConnection.ResetUser(new string[] { "Worker", "worker" });
                        break;
                    case 3:
                        //Установление роли Директора
                        DbConnection.UserRole = Role.Director;
                        DbConnection.ResetUser( new string[] { "Director", "12345678" });
                        break;
                    case 4:
                        //Установление роли Мастера
                        DbConnection.UserRole = Role.Foreman;
                        DbConnection.ResetUser(new string[] { "Foreman", "foreman" });
                        break;
                }

            }
            //В случае, если такого пользователя не нашло просиходит возврат к Пользователю по умолчанию
            catch (Exception e)
            {
                //Установление подключения к БД под дефолтным пользователем(права только на логин) 
                DbConnection.ResetUser(DbConnection.defaultUser);
                //Возврат ошибки обратно в Login View для того что бы пользователь увидел что пользователя с такими(введенными) данными не существует
                ModelState.AddModelError("Password_hash", "Invalid username or password. Exeption: " + e.ToString());
                return View(user);
            }

            //установление флага успешного прохождения процедуры логина
            bool authenticated = (DbConnection.UserRole != Role.Default_User);

            if (!authenticated)
            {
                ModelState.AddModelError("Password_hash", "Invalid username or password");
                return View(user);
            }
            //По окончанию всех проверок установка флага и преход на Home Views
            DbConnection.login = authenticated;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            DB_Context ctx = DbConnection.GetInstance();
            MemberV2 mv2 = new MemberV2();
            mv2.member = DbConnection.CurrentMember;
            if(DbConnection.CurrentMember.foreman_id > 0)
            {
                //.Where(m => m.id == DbConnection.CurrentMember.foreman_id).ToList().First();
                mv2.foreman = ctx.Members.Find(DbConnection.CurrentMember.foreman_id);
            }
            return View(mv2);
        }

        [HttpPost]
        public IActionResult Profile(MemberV2 a)
        {
            DB_Context context = DbConnection.GetInstance();
            Members e = context.Members.Find(a.member.id);
            e.name = a.member.name;
            e.surname = a.member.surname;
            e.patronymic = a.member.patronymic;
            e.date_of_employment = a.member.date_of_employment;
            e.role_id = a.member.role_id;
            e.phone_number = a.member.phone_number;
            e.email = a.member.email;
            e.salary = a.member.salary;
            e.password_hash = a.member.password_hash;
            try
            {
                context.SaveChanges();
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                DbConnection.GetInstance();
                ModelState.AddModelError("Error", "Invalid input data: " + ex.ToString());
                return View(a);
            }
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordChanger passwordChanger)
        {
            DB_Context context = DbConnection.GetInstance();
            context.Members.Find(passwordChanger.memberId).password_hash = passwordChanger.newPassword1;
            try
            {
                context.SaveChanges();
                DbConnection.CurrentMember = context.Members.Find(passwordChanger.memberId);
            }
            catch (Exception e)
            {
                DbConnection.GetInstance();
                ModelState.AddModelError("Error", "Invalid input data: " + e.ToString());
            }
            return RedirectToAction("Profile", DbConnection.CurrentMember);
        }

        [HttpGet]
        public IActionResult MyWorks(int id)
        {
            DB_Context ctx = DbConnection.GetInstance();
            IEnumerable<Works> works = ctx.Works.Where(w => w.member_id == id).ToList();
            return View(works);
        }

        [HttpGet]
        public IActionResult MyWorkers()
        {
            DB_Context ctx = DbConnection.GetInstance();
            IEnumerable<Members> workers = ctx.Members.Where(w => w.foreman_id == DbConnection.CurrentMember.id && w.role_id == 2).ToList();
            return View(workers);
        }

        [HttpGet]
        public IActionResult ForemanWorks()
        {
            DB_Context context = DbConnection.GetInstance();
            List<WorksV2> mWorks;
            try
            {
                mWorks = (from memb in context.Members
                            where memb.foreman_id == DbConnection.CurrentMember.id
                            join w in context.Works on memb.id equals w.member_id
                            select new WorksV2
                            {
                                worker = new Members
                                {
                                    id = memb.id,
                                    name = memb.name,
                                    surname = memb.surname,
                                    patronymic = memb.patronymic,
                                    phone_number = memb.phone_number,
                                    email = memb.email
                                },
                                work = w
                            }).ToList();
            }
            catch (Exception e)
            {
                mWorks = new List<WorksV2>();
            }
            return View(mWorks);
        }

        [HttpGet]
        public IActionResult CreateWorker()
        {
            MemberV2 member = new MemberV2();
            member.member = new Members();
            member.member.foreman_id = DbConnection.CurrentMember.id;
            member.member.salary = 1;
            member.member.date_of_employment =  DateTime.Today;
            
            return View(member);
        }

        [HttpPost]
        public IActionResult CreateWorker(MemberV2 a)
        {
            DB_Context context = DbConnection.GetInstance();
            try
            {
                context.Members.Add(a.member);
                context.SaveChanges();
                return RedirectToAction("MyWorkers", "Account");
            }
            catch (Exception ex)
            {
                DbConnection.GetInstance();
                ModelState.AddModelError("Error", "Invalid input data: " + ex.ToString());
                return View(a);
            }
        }

        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}