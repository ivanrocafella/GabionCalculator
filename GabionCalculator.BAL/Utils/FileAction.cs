using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Utils
{
    public static class FileAction<T> where T : class 
    {
        public static string Serialize(T t) => JsonConvert.SerializeObject(t);
        public static T Deserialize(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
