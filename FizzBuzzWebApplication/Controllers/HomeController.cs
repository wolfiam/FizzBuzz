using FizzBuzzWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FizzBuzzWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFizzBuzzLists(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var FizzBuzzResults = FizzBuzzContext.dbconn.FizzBuzzDatabaseTables.Where(x => x.Active == 1).Select(
                    a => new
                    {
                        a.Id,
                        a.Number,
                        a.Message,
                        a.DateTimeEntered,
                        a.Active
                    });
            int totalRecords = FizzBuzzResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                FizzBuzzResults = FizzBuzzResults.OrderByDescending(s => s.Number);
                FizzBuzzResults = FizzBuzzResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                FizzBuzzResults = FizzBuzzResults.OrderBy(s => s.Number);
                FizzBuzzResults = FizzBuzzResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = FizzBuzzResults
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public void printRange(int from, int to)
        {
            using (FizzBuzzContext connection = new FizzBuzzContext())
            {
                FizzBuzzDataContext db = new FizzBuzzDataContext();
                Repository.deactivateNumbers();
                if (from < to)
                {
                    for (int i = from; i < to + 1; i++)
                    {
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {
                            Repository.activateNumbers(i);
                        }
                        else
                        {
                            FizzBuzzDatabaseTable fb = new FizzBuzzDatabaseTable();
                            fb.Number = i;
                            fb.Message = "";
                            fb.DateTimeEntered = DateTime.Now;
                            fb.Active = 1;
                            FizzBuzzContext.dbconn.FizzBuzzDatabaseTables.InsertOnSubmit(fb);
                            FizzBuzzContext.dbconn.SubmitChanges();
                        }
                    }
                }
                else{
                    for (int i = to; i < from + 1; i++)
                    {
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {
                            Repository.activateNumbers(i);
                        }
                        else
                        {
                            FizzBuzzDatabaseTable fb = new FizzBuzzDatabaseTable();
                            fb.Number = i;
                            fb.Message = "";
                            fb.DateTimeEntered = DateTime.Now;
                            fb.Active = 1;
                            FizzBuzzContext.dbconn.FizzBuzzDatabaseTables.InsertOnSubmit(fb);
                            FizzBuzzContext.dbconn.SubmitChanges();
                        }
                    }
                }
            }
        }
    }
}
