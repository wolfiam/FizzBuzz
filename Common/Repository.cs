using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
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
        public static void activateNumbers(int i)
        {
            using (FizzBuzzDataContext db = new FizzBuzzDataContext())
            {
                var fizzBuzzNumber = db.FizzBuzzDatabaseTables.Single(x => x.Number == i);
                FizzBuzzDatabaseTable fb = new FizzBuzzDatabaseTable();
                //GET LIST OF ALL DIVISORS
                var divisorList = Divisor.getDivisors();
                foreach (var numberToBeDividedby in divisorList)
                {
                    bool divisor = i % Convert.ToInt32(numberToBeDividedby.Divisor) == 0;
                    if (divisor)
                    {
                        //CHECK IF EACH NUMBER, IN THE LIST TO BE PRINTED, CAN BE DIVIDED BY ANY NUMBER IN THE DIVISOR LIST
                        fizzBuzzNumber.Number = i;
                        fizzBuzzNumber.Message = numberToBeDividedby.Message.ToString();
                        fizzBuzzNumber.DateTimeEntered = DateTime.Now;
                        fizzBuzzNumber.Active = 1;
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {//NO NEED TO PRTIN THE VALUE AGAIN
                        }
                        else { db.FizzBuzzDatabaseTables.InsertOnSubmit(fb); }
                        db.SubmitChanges();
                    }
                    else
                    {
                        //FOR VALUES NOT DIVISIBLE BY THE DIVISOR BUT STILL EXIST
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {
                            fizzBuzzNumber.Active = 1;
                            db.SubmitChanges();
                        }
                        else
                        {
                            //JUST FOR INSURANCE, IF THE NUMBER DID NOT EXIST, ADD IT NOW.
                            fizzBuzzNumber.Active = 1;
                            FizzBuzzContext.dbconn.FizzBuzzDatabaseTables.InsertOnSubmit(fb);
                            FizzBuzzContext.dbconn.SubmitChanges();
                        }

                    }


                }
            }
        }

        public static jqGridFizzBuzzTable results(string sidx, string sord, int page, int rows)
        {
            var jqGrid = new jqGridFizzBuzzTable();

            jqGrid.pageIndex = Convert.ToInt32(page) - 1;
            jqGrid.pageSize = rows;
            jqGrid.rows = FizzBuzzContext.dbconn.FizzBuzzDatabaseTables
                .OrderBy(x => x.Number)
                 .Where(x => x.Active == 1)
                 .Select(
                     a => new FizzBuzz
                     {
                         Id = a.Id,
                         Number = a.Number,
                         Message = a.Message,
                         DateTimeEntered = a.DateTimeEntered,
                         Active = a.Active
                     }).ToList();
            jqGrid.totalRecords = jqGrid.rows.Count();
            jqGrid.totalPages = (int)Math.Ceiling((float)jqGrid.totalRecords / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                jqGrid.rows.OrderByDescending(s => s.Number);
                jqGrid.rows.Skip(jqGrid.pageIndex * jqGrid.pageSize).Take(jqGrid.pageSize);
            }
            else
            {
                jqGrid.rows.OrderBy(s => s.Number);
                jqGrid.rows.Skip(jqGrid.pageIndex * jqGrid.pageSize).Take(jqGrid.pageSize);
            }

            return jqGrid;
        }

        public static jqGridSeUpTable SetUpresults(string sidx, string sord, int page, int rows)
        {
            var jqGrid = new jqGridSeUpTable();

            jqGrid.pageIndex = Convert.ToInt32(page) - 1;
            jqGrid.pageSize = rows;
            jqGrid.rows = FizzBuzzContext.dbconn.SetUpDatabaseTables
                 .Select(
                     a => new SetUp
                     {
                         Id = a.Id,
                         Divisor = a.Divisor,
                         Message = a.Message,
                         DateTimeEntered = a.DateTimeEntered,
                         Active = a.Active
                     }).ToList();
            jqGrid.totalRecords = jqGrid.rows.Count();
            jqGrid.totalPages = (int)Math.Ceiling((float)jqGrid.totalRecords / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                jqGrid.rows.OrderByDescending(s => Convert.ToInt32(s.Divisor));
                jqGrid.rows.Skip(jqGrid.pageIndex * jqGrid.pageSize).Take(jqGrid.pageSize);
            }
            else
            {
                jqGrid.rows.OrderBy(s => Convert.ToInt32(s.Divisor));
                jqGrid.rows.Skip(jqGrid.pageIndex * jqGrid.pageSize).Take(jqGrid.pageSize);
            }

            return jqGrid;
        }

        public static void createSetUp([Bind(Exclude = "Id")] SetUp objTodo)
        {
            try
            {
                SetUpDatabaseTable fb = new SetUpDatabaseTable();
                fb.Active = 1;
                fb.DateTimeEntered = DateTime.Now;
                double num;
                if (double.TryParse(objTodo.Divisor, out num))
                {
                    fb.Divisor = objTodo.Divisor;
                }
                else
                {
                    return;
                }

                fb.Message = objTodo.Message;

                FizzBuzzContext.dbconn.SetUpDatabaseTables.InsertOnSubmit(fb);
                FizzBuzzContext.dbconn.SubmitChanges();
            }
            catch { }
        }

        public static void editSetUp(SetUp objTodo)
        {
            using (FizzBuzzDataContext db = new FizzBuzzDataContext())
            {
                var setUp = db.SetUpDatabaseTables.Single(x => x.Id == objTodo.Id);
                setUp.Active = objTodo.Active;
                setUp.DateTimeEntered = DateTime.Now;
                setUp.Message = objTodo.Message;
                setUp.Divisor = objTodo.Divisor;
                db.SubmitChanges();
            }
        }

    }
}
