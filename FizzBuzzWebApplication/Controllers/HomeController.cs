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

            return Json(Repository.results(sidx, sord, page, rows), JsonRequestBehavior.AllowGet);
        }

        public void printRange(int from, int to)
        {
            PrintRanges.print(from, to);
        }

      
    }
}
