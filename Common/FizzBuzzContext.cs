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
        public static FizzBuzzDataContext dbconn = new FizzBuzzDataContext();
    }
}