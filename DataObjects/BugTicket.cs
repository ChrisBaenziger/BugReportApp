using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class BugTicket
    {
        public int BugTicketID { get; set; }
        public DateTime BugDate { get; set; }
        public int SubtmitID { get; set; }
        public string VersionNumber { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Feature { get; set; }
        public int AssignedTo { get; set; }
        public DateTime LastWorkedDate { get; set; }
        public int LastWorkedEmployee { get; set; }
        public bool Active { get; set; }
    }

    public class BugTicketVM
    {
        public string AssignedToName { get; set; }

        public string LastWorkedName { get; set; }
    }
        
}
