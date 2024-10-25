using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class CommonResponseDTO
    {
        public bool Status { get; set; }
        
        public int flag { get; set; }
        public string message { get; set; }  
        public string response { get; set; }
    }
    public class CommonResponseDTO<T>
    {
        public bool Status { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
    public class CommonListResponseDTO<T>
    {
        public bool Status { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
        public List<T> result { get; set; }
    }
}
   
