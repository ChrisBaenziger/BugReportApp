using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IBugReportAccessor
    {
        // Get one bug ticket
        BugTicket SelectBugTicketByBugTicketID(int bugTicketID);

        // Get bug tickets by
        List<BugTicket> SelectAllBugTickets();
        List<BugTicket> SelectBugTicketsByVersion(string version);
        List<BugTicket> SelectBugTicketsByArea(string area);
        List<BugTicket> SelectBugTicketsByStatus(string status);
        List<BugTicket> SelectBugTicketByFeature(string feature);
        List<BugTicket> SelectBugTicketByAssignedTo(int AssignedTo);

        // Get list lables
        List<string> SelectAllVersions();
        List<string> SelectAllAreas();
        List<string> SelectAllStatus();
        List<string> SelectAllFeatures();
        
        // Modify Bug Tickets
        bool UpdateBugReport(BugTicket oldBugTicket, BugTicket newBugTicket);
        int AddBugReport(BugTicket bugTicket);

        List<KeyValuePair<string, string>> SelectStatistics();

    }
}
