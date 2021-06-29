using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildingCompany.Models;
using BuildingCompany.Models.Entityes;
using BuildingCompany.Models.HelperEntityes;

namespace BuildingCompany.Controllers
{
    public class HomeController : Controller
    {
        public DB_Context dB_Context = DbConnection.GetInstance();

        public IActionResult Index()
        {
            //IEnumerable<Members> members = dB_Context.Members.ToList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Facility()
        {
            DB_Context ctx = DbConnection.GetInstance();
            List<Facility> fac;
            if (DbConnection.UserRole == Role.Foreman)
            {
                try
                {
                    fac = (from f in ctx.Facility
                           join w in ctx.Works on f.id equals w.facility_id
                           where w.member_id == DbConnection.CurrentMember.id
                           select new Facility
                           {
                               id = f.id,
                               deal_id = f.deal_id,
                               date_of_start = f.date_of_start,
                               time = f.time,
                               number_of_floors = f.number_of_floors,
                               area = f.area,
                               adress = f.adress
                           }).ToList();
                }
                catch (Exception e)
                {
                    fac = new List<Facility>();
                }
            } else
            {
                fac = ctx.Facility.ToList();
            }
            return View(fac);
        }

        [HttpGet]
        public IActionResult CreateWork(int id, bool f_id)
        {
            
            if(DbConnection.createWork == null)
            {
                DbConnection.createWork = new Works();
            }
            if (f_id)
            {
                DbConnection.createWork.facility_id = id;
            } else
            {
                DbConnection.createWork.member_id = id;
            }

            DbConnection.createWork.start_date = DateTime.Today;
            DbConnection.createWork.end_date = DateTime.Today;
            return View(DbConnection.createWork);
        }

        [HttpPost]
        public IActionResult CreateWork(Works work)
        {
            DB_Context context = DbConnection.GetInstance();
            try
            {
                //mReview.Review.Member_id = DbConnection.currentMember.Member_id;
                //mReview.Review.Lecture_id = mReview.Lecture.Lecture_id;
                context.Works.Add(work);

                //context.Add(mReview.Review);
                context.SaveChanges();
                DbConnection.createWork = null;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Invalid input data: " + e.ToString());
                return View(work);
            }
            return RedirectToAction("ForemanWorks", "Account");
        }

        [HttpGet]
        public IActionResult Workers()
        {
            DB_Context context = DbConnection.GetInstance();
            List<MemberV2> memberV2s;
            try
            {
                memberV2s = (from memb in context.Members
                          where memb.role_id == 2
                          select new MemberV2
                          {
                              member = new Members
                              {
                                  id = memb.id,
                                  name = memb.name,
                                  surname = memb.surname,
                                  salary = memb.salary,
                                  patronymic = memb.patronymic,
                                  phone_number = memb.phone_number,
                                  email = memb.email
                              }
                          }).ToList();

                foreach(var mv2 in memberV2s)
                {
                    mv2.WorkAmount = (from w in context.Works where w.member_id == mv2.member.id
                                      where w.end_date >= DateTime.Today
                                      select w.id).Count();
                }
            }
            catch (Exception e)
            {
                memberV2s = new List<MemberV2>();
            }
            return View(memberV2s);
        }

        [HttpGet]
        public IActionResult DealsList()
        {
            DB_Context context = DbConnection.GetInstance();
            List<DealsV2> dealsV2s;
            try
            {
                dealsV2s = (from d in context.Deals
                             join comp in context.Companies on d.company_id equals comp.id
                             select new DealsV2
                             {
                                 Deal = d,
                                 Companie = comp
                                 
                             }).ToList();
            }
            catch (Exception e)
            {
                dealsV2s = new List<DealsV2>();
                dealsV2s.Add(new DealsV2 { Companie = new Companies { comp_name = e.ToString() } });
            }
            return View(dealsV2s);
        }

        [HttpGet]
        public IActionResult Members()
        {
            DB_Context ctx = DbConnection.GetInstance();
            IEnumerable<Members> workers = ctx.Members.ToList();
            return View(workers);
        }

        [HttpGet]
        public IActionResult EditSalary(int id)
        {
            DB_Context ctx = DbConnection.GetInstance();
            MemberV2 mem = new MemberV2();
            mem.member = ctx.Members.Find(id);
            return View(mem);
        }

        [HttpPost]
        public IActionResult EditSalary(MemberV2 mem)
        {
            DB_Context context = DbConnection.GetInstance();
            try
            {
                context.Members.Find(mem.member.id).salary = mem.member.salary;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Invalid input data: " + e.ToString());
                return View(mem);
            }
            return View(mem); //RedirectToAction("ForemanWorks", "Account");
        }

        [HttpGet]
        public IActionResult Companies()
        {
            DB_Context ctx = DbConnection.GetInstance();
            IEnumerable<Companies> comp = ctx.Companies.ToList();
            return View(comp);
        }

        [HttpGet]
        public IActionResult CreateMember()
        {
            MemberV2 member = new MemberV2();
            member.member = new Members();
            member.member.foreman_id = -1;
            member.member.date_of_employment = DateTime.Today;
            return View(member);
        }

        [HttpPost]
        public IActionResult CreateMember(MemberV2 a)
        {
            DB_Context context = DbConnection.GetInstance();
            try
            {
                context.Members.Add(a.member);
                context.SaveChanges();
                return RedirectToAction("Members", "Home");
            }
            catch (Exception ex)
            {
                DbConnection.GetInstance();
                ModelState.AddModelError("Error", "Invalid input data: " + ex.ToString());
                return View(a);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
