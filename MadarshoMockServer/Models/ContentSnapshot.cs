using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MadarshoMockServer.Models
{
    public class ContentSnapshot
    {

        [Key]
        public int Id { get; set; }
        public long date { get; set; }
        public string serializedText { get; set; }

        public ContentSnapshot()
        {
            
        }

        public ContentSnapshot(PurpleContent purpleContent)
        {
            date = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            serializedText = Json.Encode(purpleContent);
        }

        public PurpleContent GetPurpleContent()
        {
            return serializedText != null ? Json.Decode<PurpleContent>(serializedText) : null;
        }
    }
}