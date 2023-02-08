using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    public class salesforceJsonConfig
    {
        public List<sfObjects> sfObjectList { get; set; }
        public string action { get; set; }
        public class sfObjects
        {
            public string objectName { get; set; }
            public string query { get; set; }
        }
    }
}
