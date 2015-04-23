using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PrintRanges
    {


        public static void print(int from, int to)
        {
            using (FizzBuzzContext connection = new FizzBuzzContext())
            {
                FizzBuzzDataContext db = new FizzBuzzDataContext();
                Repository.deactivateNumbers();
                if (from < to)
                {
                    for (int i = from; i < to + 1; i++)
                    {
                        rangeBuilder(i, db);
                    }
                }
                else
                {
                    for (int i = to; i < from + 1; i++)
                    {
                        rangeBuilder(i, db);
                    }
                }
            }
        }

        public static void rangeBuilder(int i, FizzBuzzDataContext db)
        {

            //CHECK IF i EXISTS
            if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
            {
                //IF WE ALREADY HAVE THE VALUES SAVED, JUST MAKE THEM ACTIVE.
                Repository.activateNumbers(i);
            }
            else
            {
                FizzBuzzDatabaseTable fb = new FizzBuzzDatabaseTable();
                //GET LIST OF ALL DIVISORS
                var divisorList = Divisor.getDivisors();

                //CHECK IF EACH NUMBER, IN THE LIST TO BE PRINTED, CAN BE DIVIDED BY ANY NUMBER IN THE DIVISOR LIST
                foreach (var numberToBeDividedby in divisorList)
                {
                    bool divisor = i % Convert.ToInt32(numberToBeDividedby.Divisor) == 0;
                    if (divisor)
                    {
                        //IF TRUE, PRINT THE FOLLOWING.
                        fb.Number = i;
                        fb.Message = numberToBeDividedby.Message.ToString();
                        fb.DateTimeEntered = DateTime.Now;
                        fb.Active = 1;
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {//IF WE ADD A NEW RANGE, NO NEED TO PRTIN THE VALUES AGAIN FOR THE VALUES ALREADY ACTIVE.
                        }
                        //FOR THE VALUES THAT DIDNT EXIST, ADD THEM.
                        else { db.FizzBuzzDatabaseTables.InsertOnSubmit(fb); }
                        db.SubmitChanges();
                    }
                    else
                    {
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {
                            //FOR VALUES NOT DIVISIBLE BY THE DIVISOR.
                            //IF WE ADD A NEW RANGE, NO NEED TO PRTIN THE VALUES AGAIN FOR THE VALUES ALREADY ACTIVE.
                        }
                        else
                        {
                            //FOR THE VALUES THAT DIDNT EXIST AND WERE NOT DIVISIBLE BY THE DIVISOR, ADD THEM.
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