using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void activateNumbers(int number)
        {
            using (FizzBuzzDataContext db = new FizzBuzzDataContext())
            {
                var fizzBuzzNumber = db.FizzBuzzDatabaseTables.Single(x => x.Number == number);
                fizzBuzzNumber.Active = 1;
                db.SubmitChanges();
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
    }
}