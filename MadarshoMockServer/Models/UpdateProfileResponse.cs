using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadarshoMockServer.Models
{
    public class UpdateProfileResponse
    {
        public long timestamp { get; set; }

        public UpdateProfileResponse(long timestamp)
        {
            this.timestamp = timestamp;
        }
    }
}