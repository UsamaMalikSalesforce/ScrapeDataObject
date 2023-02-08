using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = new CommonClass.Result();
            var Obj = new Scrape();
            result = Obj.loadWeb();
            if (result.Status)
            {
                var cred = CommonClass.GetCustomConfig();
                result = cred.Status ? Obj.login(cred.data) : cred;
                if(result.Status)
                {
                    result = Obj.ScrapeData();
                    Console.WriteLine(result.Status ? "Success": result.Message);
                }
                else
                {
                    Console.WriteLine("ERROR: " + result.Message);
                }
            }
            else
            {
                Console.WriteLine("ERROR: " + result.Message);
            }
           
        }
    }
}
