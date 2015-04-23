using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class jqGrid
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
    }

    public class jqGridFizzBuzzTable : jqGrid
    {
        public List<FizzBuzz> rows = new List<FizzBuzz>();
    }

    public class jqGridSeUpTable : jqGrid
    {
        public List<SetUp> rows = new List<SetUp>();
    }
}