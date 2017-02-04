using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadarshoMockServer.Models
{
    public class PurpleContent
    {
        public string contentVersion { get; set; }
        public string applicationVersion { get; set; }
        public ICollection<DrupalContent> articles { get; set; }
        public ICollection<DrupalContent> tips { get; set; }
        public DrupalContent bodyChanges { get; set; }
        public DrupalContent fetus { get; set; }
        public DrupalContent uterus { get; set; }
        public ICollection<DrupalContent2> prenatalCares { get; set; }
        public ICollection<DrupalContent2> symptoms { get; set; }
        public ICollection<MilestoneViewModel> milestones { get; set; }
    }
}