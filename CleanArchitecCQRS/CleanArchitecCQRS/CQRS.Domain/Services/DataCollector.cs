using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Domain.Services
{
    public class DataCollector
    {
        public DataCollector(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }
        public static DataCollector SetDefault(string name)
        {
            const string L = "«";
            const string R = "»";
            return new DataCollector(string.Format("{0}{1}{2}", L, name, R), "");
        }
    }
}
