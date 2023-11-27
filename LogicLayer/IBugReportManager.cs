using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IBugReportManager
    {
        // Get one bug ticket
        BugTicketVM GetBugTicket(int bugTicketID);

        // Get bug tickets by 
        List<BugTicketVM> GetAllBugTickets();
        List<BugTicketVM> GetBugTicketsByVersion(string version);
        List<BugTicketVM> GetBugTicketsByArea(string area);
        List<BugTicketVM> GetBugTicketsByStatus(string status);
        List<BugTicketVM> GetBugTicketsByFeature(string feature);
        List<BugTicketVM> GetBugTicketsByAssignedTo(int assignedTo);
        
        // Get list labels
        List<string> GetAllVersions();
        List<string> GetAllAreas();
        List<string> GetAllStatus();
        List<string> GetAllFeatures();
        
        // Modify Bug Tickets
        bool UpdateBugReport (BugTicket oldBugTicket, BugTicket newBugTicket);
        int AddBugReport(BugTicket bugTicket);

        List<KeyValuePair<string, string>> GetStatistics();
    }
}
