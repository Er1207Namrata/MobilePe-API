using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.DataLayer
{
    public class ConnectionString
    {

        public ConnectionString(string value) => Value = value;
        public string Value { get; }
    }
    public class MallConnectionString
    {

        public MallConnectionString(string value) => Value = value;
        public string Value { get; }
    }
    
}
