using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MadarshoMockServer.Models
{
    public class UserSnapshot
    {
        [Key]
        public int Id { get; set; } 
        public string serializedText { get; set; }

        public UserSnapshot()
        {
            
        }

        public UserSnapshot(PurpleUser purpleUser)
        { 
            serializedText = Json.Encode(purpleUser);
        }

        public PurpleUser GetPurpleUser()
        {
            return serializedText != null ? Json.Decode<PurpleUser>(serializedText) : null;
        }
    }
}