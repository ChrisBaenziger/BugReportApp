using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    internal interface IBugReportManager
    {
        // Get one bug ticket
        BugTicketVM GetBugTicket(int bugTicketID);

        // Get bug tickets by 
        List<BugTicket> GetAllBugTickets();
        List<BugTicket> GetBugTicketsByVersion(string version);
        List<BugTicket> GetBugTicketsByArea(string area);
        List<BugTicket> GetBugTicketsByStatus(string status);
        List<BugTicket> GetBugTicketsByFeature(string feature);
        List<BugTicket> GetBugTicketsByAssignedTo(int assignedTo);
        
        // Get list labels
        List<string> GetAllVersions();
        List<string> GetAllAreas();
        List<string> GetAllStatus();
        List<string> GetAllFreatures();
        

        // 
        bool UpdateBugReport (BugTicket oldBugTicket, BugTicket newBugTicket);
    }
}
