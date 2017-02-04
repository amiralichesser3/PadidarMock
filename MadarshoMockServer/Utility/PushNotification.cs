using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using MadarshoMockServer.Models;

namespace MadarshoMockServer.Utility
{
    public class PushNotification
    {
        public PushNotification(SyncObject obj, string deviceId)
        {
            try
            {
                //var applicationID = "AIzaSyAH4P1A34CxZL43G1LZxAG11_gjjwYiolY";
                //var applicationID = "AIzaSyDUO4n5L5SDEEW5P1Rr9xGO4gu56yxHfKw";
                var applicationID =
                    "AAAAuzo0NTQ:APA91bHk703uTRhhVC_i3eb5EiVDYqGP-4nW1T20GJ8IyO1mec1hq9kiGSOvhSOie5plZakTl_1-6IFycBzL52-9FILVTqTHr3AFCP4_qKe5RGKsaF6T3cj8FiJeJzP0xRLJFMg3Ypvn";

                var senderId = "804135384372"; 

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";

                tRequest.ContentType = "application/json";

                var data = new

                {

                    to = deviceId,

                    data = new

                    {
                        syncType = obj.syncType,

                        userChange = obj.userChange
                    }
                };
                 

                var json = Json.Encode(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));

                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {

                    dataStream.Write(byteArray, 0, byteArray.Length);


                    using (WebResponse tResponse = tRequest.GetResponse())
                    {

                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {

                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {

                                String sResponseFromServer = tReader.ReadToEnd();

                                string str = sResponseFromServer;

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                string str = ex.Message;

            }

        }

    }
}