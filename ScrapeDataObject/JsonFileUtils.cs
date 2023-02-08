using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScrapeDataObject
{
    internal class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        public static void SimpleWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, _options);
            File.WriteAllText(fileName, jsonString);
        }

    }
}
