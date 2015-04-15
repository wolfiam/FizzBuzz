using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FizzBuzzWebApplication.Models
{
    public class FizzBuzzContext : DbContext
    {
        public static FizzBuzzDataContext dbconn = new FizzBuzzDataContext();
    }
}