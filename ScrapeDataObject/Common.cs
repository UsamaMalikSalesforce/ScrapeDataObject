using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    public class Common
    {
        public class Result
        {
            public int? Id { get; set; }
            public string Message { get; set; }
            public string Name { get; set; }
            public bool? Status { get; set; }
            public string data { get; set; }
            public bool? dataIsList { get; set; }
        }
    }
}
