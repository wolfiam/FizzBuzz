using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FizzBuzzWebApplication.Models
{
    public class Repository
    {
        public static void deactivateNumbers()
        {
            using (FizzBuzzDataContext db = new FizzBuzzDataContext())
            {
                db.FizzBuzzDatabaseTables.Where(x => x.Active == 1).ToList().ForEach(a => a.Active = 0);
                db.SubmitChanges();
            }
        }

        public static void activateNumbers(int number)
        {
            using (FizzBuzzDataContext db = new FizzBuzzDataContext())
            {
                var fizzBuzzNumber = db.FizzBuzzDatabaseTables.Single(x => x.Number == number);
                fizzBuzzNumber.Active = 1;
                db.SubmitChanges();
            }
        }
    }
}