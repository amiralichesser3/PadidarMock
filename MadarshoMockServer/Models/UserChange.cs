using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadarshoMockServer.Models
{
    public class UserChange
    {
        public long timeStamp { get; set; }
        public PurpleUser user { get; set; }

        public PurpleUser removedUser { get; set; }
    }
}