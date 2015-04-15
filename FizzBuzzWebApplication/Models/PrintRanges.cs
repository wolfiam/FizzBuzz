using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FizzBuzzWebApplication.Models
{
    public class PrintRanges
    {
        public static bool fizz { get; set; }
        public static bool buzz { get; set; }

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
                        if (db.FizzBuzzDatabaseTables.Any(u => u.Number == i))
                        {
                            Repository.activateNumbers(i);
                        }
                        else
                        {
                            FizzBuzzDatabaseTable fb = new FizzBuzzDatabaseTable();
                            fb.Number = i;


                             fizz = i % 3 == 0;
                             buzz = i % 5 == 0;
                            if (fizz && buzz)
                            {
                                fb.Message = ("FizzBuzz");
                            }
                            else if (fizz)
                            {
                                fb.Message = ("Fizz");
                            }
                            else if (buzz)
                            {
                                fb.Message = ("Buzz");
                            }
                            else { fb.Message = string.Empty; }

                            fb.DateTimeEntered = DateTime.Now;
                            fb.Active = 1;
                            FizzBuzzContext.dbconn.FizzBuzzDatabaseTables.InsertOnSubmit(fb);
                            FizzBuzzContext.dbconn.SubmitChanges();
                        }
                    }
                }
                else
                {
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
                            bool fizz = i % 3 == 0;
                            bool buzz = i % 5 == 0;
                            if (fizz && buzz)
                            {
                                fb.Message = ("FizzBuzz");
                            }
                            else if (fizz)
                            {
                                fb.Message = ("Fizz");
                            }
                            else if (buzz)
                            {
                                fb.Message = ("Buzz");
                            }
                            else { fb.Message = string.Empty; }
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