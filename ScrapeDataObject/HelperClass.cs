using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    internal static class HelperClass
    {
        public static List<string> contentAndDocument = new List<string>()
        {
            "Attachment","Document","ContentVersion"
        };
        public static string BatchFileName = "PTIBatchFile.bat";
        public static string DataLoaderPath = @"C:\\Users\\Usama Malik\\dataloader\\v57.0.0\\bin\";
        public static string cmdBase = $"process.bat F:\\Projects\\PTI\\{CurrentDateWithDash()} ";
        public static bool HaveData<TSource>(this TSource source)
        {
            return source != null;
        }
        public static bool ListHaveData<TSource>(this IEnumerable<TSource> source)
        {
            return source != null && source.Any() && source.FirstOrDefault().HaveData();
        }
        public static List<TSource> StringToCls<TSource>(this string source)
        {
            try
            {
                return source.ListHaveData() ? JsonConvert.DeserializeObject<List<TSource>>(source) : new List<TSource>();
            }
            catch (Exception ee)
            { }
            return new List<TSource>();
        }
        public static TSource StringToSingleCls<TSource>(this string source)
        {
            try
            {
                return source.HaveData() ? JsonConvert.DeserializeObject<TSource>(source) : new List<TSource>().FirstOrDefault();
            }
            catch (Exception ee)
            { }
            return new List<TSource>().FirstOrDefault();
        }
        public static string CurrentDateWithDash()
        {
            var date = DateTime.Now.Date.ToShortDateString();
            date = date.Replace('/', '-');
            return date;
        }
    }
}
