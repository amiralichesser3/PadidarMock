using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls;

namespace MadarshoMockServer.Models
{
    public class PurpleUser
    { 
        public long timestamp { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
        public string pass { get; set; }
        public int? state { get; set; }//0 for nothing, 1 for ttc, 2 for pregz
        public long? lastPeriod { get; set; }
        public long? birthday { get; set; }
        public string fcmToken { get; set; }
        public WeekView pregnancy { get; set; }
        public TtcView ttc { get; set; }
        public int? height { get; set; }
        public int? currentWeight { get; set; }
        public int? newsletter { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public List<PurpleUserWeight> weights { get; set; }
        public List<PurpleUserSymptom> symptoms { get; set; }
        public List<PurpleUserPrenatalCare> prenatalCares { get; set; }
        public List<PurpleDate> periods { get; set; }
    }

    public class PurpleDate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof (PurpleDate))
            {
                PurpleDate pd = (PurpleDate) obj;

                if (day != pd.day)
                {
                    return false;
                }

                if (month != pd.month)
                {
                    return false;
                }

                if (year != pd.year)
                {
                    return false;
                }

                return true;
            }

            return base.Equals(obj);
        }
    }

    public class PurpleUserPrenatalCare
    {
        public PurpleDate date { get; set; }
        public long prenatalCareId { get; set; }
    }

    public class PurpleUserSymptom
    {
        public PurpleDate date { get; set; }
        public long symptomId { get; set; }
    }

    public class PurpleUserWeight
    {
        public PurpleDate date { get; set; }
        public int weight { get; set; }
    }

    public class TtcView
    {
        public int cycle { get; set; }
        public int day { get; set; }
        public string phase { get; set; }
        public string enPhase { get; set; }
    }

    public class WeekView 
    {
        public int week { get; set; }
        public int day { get; set; }
    }
}