using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FizzBuzzContext : DbContext
    {
        public static DbSet<FizzBuzz> FizzBuzzLists { get; set; }
        public static  DbSet<SetUp> SetUpLists { get; set; }
        public static FizzBuzzDataContext dbconn = new FizzBuzzDataContext();
    }
}