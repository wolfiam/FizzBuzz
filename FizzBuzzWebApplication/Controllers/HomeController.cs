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

        FizzBuzzContext db = new FizzBuzzContext();
        public JsonResult GetFizzBuzzLists(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var todoListsResults = db.FizzBuzzLists.Select(
                    a => new
                    {
                        a.Id,
                        a.Number,
                        a.Message,
                        a.DateTimeEntered,
                        a.Active
                    });
            int totalRecords = todoListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                todoListsResults = todoListsResults.OrderByDescending(s => s.Number);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                todoListsResults = todoListsResults.OrderBy(s => s.Number);
                todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = todoListsResults
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create([Bind(Exclude = "Id")] FizzBuzz objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.FizzBuzzLists.Add(objTodo);
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Edit(FizzBuzz objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(objTodo).State = EntityState.Modified;
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Delete(int Id)
        {
            FizzBuzz FizzBuzzList = db.FizzBuzzLists.Find(Id);
            db.FizzBuzzLists.Remove(FizzBuzzList);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }
}
