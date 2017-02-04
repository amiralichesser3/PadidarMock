using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadarshoMockServer.Models
{
    public class DrupalContent2
    {
        public bool isChecked { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public string url { get; set; }
        public ICollection<string> tips { get; set; }
    }
}