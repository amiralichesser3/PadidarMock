using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MadarshoMockServer.Models
{
    public class SyncObject
    {
        public string syncType { get; set; }
        public string userChange { get; set; }

        public long timestamp { get; set; }

        public SyncObject()
        {
            
        }

        public SyncObject(string syncType, UserChange userChange, long timestamp)
        {
            this.syncType = syncType;
            this.userChange = Json.Encode(userChange);
            this.timestamp = timestamp;
        }
    }
}