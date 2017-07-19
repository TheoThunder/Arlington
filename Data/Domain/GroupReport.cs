using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Domain
{
    public class GroupReport
    {
        public GroupReport()
        {


        }

        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int TGAPM { get; set; }
        public double GAPW { get; set; }
        public double AppointmentPercentage { get; set; }
        public int TCPM { get; set; }
        public double SACR { get; set; }


    }

}
