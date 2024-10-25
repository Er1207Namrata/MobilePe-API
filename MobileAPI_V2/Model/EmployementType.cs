using MobileAPI_V2.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MobileAPI_V2.Model
{
    public class EmployementType
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class EmplTypelist
    {
        public List<EmployementType> Emplist { get; set; }
    }
    public class EmployementTypeResponse
    {
        
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
