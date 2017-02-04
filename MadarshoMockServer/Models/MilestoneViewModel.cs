using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadarshoMockServer.Models
{
    public class MilestoneViewModel
    {
        public ICollection<MilestoneView> pregnant { get; set; }
        public ICollection<TtcMilestoneViewModel> ttc { get; set; }
    }

    public class TtcMilestoneViewModel
    {
        public long id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public int day { get; set; }
        public string phase { get; set; }
    }

    public class MilestoneView
    {
        public string title { get; set; }
        public string des { get; set; }
        public int week { get; set; }
        public bool featured { get; set; }
    }
}