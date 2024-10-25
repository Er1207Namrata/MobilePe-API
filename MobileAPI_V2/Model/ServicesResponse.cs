using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class ServicesResponse<T>
    {
        public T Data { get; set; }
        public bool response { get; set; } = true;
        public string message { get; set; } = null;
    }
}
