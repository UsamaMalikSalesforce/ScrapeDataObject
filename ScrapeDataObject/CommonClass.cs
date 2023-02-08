using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    public class CommonClass
    {
        public class Result
        {
            public int? Id { get; set; }
            public string Message { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
            public string data { get; set; }
            public bool? dataIsList { get; set; }
        }
        public class CustomConfig
        {
            public string userName { get; set; }
            public string password { get; set; }

        }
        public static Result GetCustomConfig()
        {
            var result = new Result();
            try
            {
                using (StreamReader r = new StreamReader(@"customConfig.json"))
                {
                    string json = r.ReadToEnd();
                    var data = HelperClass.StringToSingleCls<CustomConfig>(json);
                    if (data.HaveData())
                    {
                        result.Status = true;
                        result.Message = "Success";
                        result.data = json;
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "No Data found";
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
