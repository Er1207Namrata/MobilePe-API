using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileAPI_V2.Model
{
    public class Notification
    {
        public static string SendNotification(long memberId, string DeviceId, string NotificationMessage, string NotificationTitle)
        {
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var objNotification = new
                {
                    to = DeviceId,
                    notification = new
                    {
                        body = NotificationMessage,
                        title = NotificationTitle,
                        image = "https://mobilepe.co.in/Franchiseassets/img/M-01.png"
                    },
                    data = new
                    {
                        title = NotificationTitle,
                        body = NotificationMessage,
                        image = "https://mobilepe.co.in/Franchiseassets/img/M-01.png"
                    },
                    priority = "high"
                };
                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotification);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAkKEO-hk:APA91bEu-xWmdhrtseZsQa5MkYfati0RCU7StkYrFEcANS--Eg9y4Wh1K0C31L5NgEJqnh-6tdw2WUNS9OBRpGnTqOzu1mP7mX1sNNzfV9YC8Ki5PWLHzB9gTauLNe95wmgblYrMFojC"));
                tRequest.Headers.Add(string.Format("Sender: id={0}", "621177403929"));
                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                               
                            }
                        }

                    }
                }

            }
            catch
            {
                throw;
            }
            return "";
        }
    }
  

    public class FCM
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string IsRead { get; set; }
        public string Notificationtype { get; set; }
        public bool Status { get; set; }
        public long Pk_NotificationId { get; set; }
    }
    public class FCMResponse 
    {
        public List<FCM> result { get; set; }
    }
    public class NotificationCount 
    {
        public int Notificationcount { get; set; }
    }
    public class ReadNotification
    {
        public long memberId { get; set; }
        public long pk_NotificationId { get; set; }
        public string type { get; set; }
    }


}
