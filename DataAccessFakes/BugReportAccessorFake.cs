using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class BugReportAccessorFake : IBugReportAccessor
    {
        List<BugTicket> fakeBugTickets = new List<BugTicket>();
        List<string> employees = new List<string> { "Chris Baenziger", "Jim Glasgow" };

        public BugReportAccessorFake()
        {
            fakeBugTickets.Add(new BugTicket()
            {
                BugTicketID = 1,
                BugDate = DateTime.Now,
                SubmitID = 1000,
                VersionNumber = "0.1.2",
                AreaName = "Core",
                Description = "Description 1",
                Status = "Testing",
                Feature = "Test fake 1",
                AssignedTo = 2,
                LastWorkedDate = DateTime.Now,
                LastWorkedEmployee = 2,
                Active = true
            });

            fakeBugTickets.Add(new BugTicket()
            {
                BugTicketID = 2,
                BugDate = DateTime.Now,
                SubmitID = 1001,
                VersionNumber = "0.1.2",
                AreaName = "Error checking",
                Description = "Description 2",
                Status = "New",
                Feature = "Test fake 2",
                AssignedTo = 1,
                LastWorkedDate = DateTime.Now,
                LastWorkedEmployee = 1,
                Active = true
            });

            fakeBugTickets.Add(new BugTicket()
            {
                BugTicketID = 3,
                BugDate = DateTime.Now,
                SubmitID = 1002,
                VersionNumber = "0.1.1",
                AreaName = "User Interface",
                Description = "Description 3",
                Status = "Resolved",
                Feature = "Test fake 3",
                AssignedTo = 2,
                LastWorkedDate = DateTime.Now,
                LastWorkedEmployee = 2,
                Active = false
            });

            fakeBugTickets.Add(new BugTicket()
            {
                BugTicketID = 4,
                BugDate = DateTime.Now,
                SubmitID = 1004,
                VersionNumber = "0.1.1",
                AreaName = "Database",
                Description = "Description 4",
                Status = "Testing",
                Feature = "Test fake 4",
                AssignedTo = 1,
                LastWorkedDate = DateTime.Now,
                LastWorkedEmployee = 1,
                Active = true
            });

        }

        public BugTicket SelectBugTicketByBugTicketID(int bugTicketID)
        {
            BugTicket bugTicket = null;

            foreach (BugTicket ticket in fakeBugTickets)
            {
                if (ticket.BugTicketID == bugTicketID)
                { bugTicket = ticket; break; }
            }
            if (bugTicket == null)
            {
                throw new ArgumentException("Bug ticket not found.");
            }

            return bugTicket;
        }

        public List<string> SelectAllAreas()
        {
            List<string> result = new List<string>();
            foreach (var ticket in fakeBugTickets)
            {
                if (!result.Contains(ticket.AreaName))
                {
                    result.Add(ticket.AreaName);
                }
            }
            if (result.Count == 0)
            {
                throw new ArgumentException("No areas found.");
            }
            return result;
        }

        public List<BugTicket> SelectAllBugTickets()
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.Active == true)
                {
                    results.Add(new BugTicket()
                    {
                        BugTicketID = ticket.BugTicketID,
                        BugDate = ticket.BugDate,
                        SubmitID = ticket.SubmitID,
                        VersionNumber = ticket.VersionNumber,
                        AreaName = ticket.AreaName,
                        Description = ticket.Description,
                        Status = ticket.Status,
                        Feature = ticket.Feature,
                        AssignedTo = ticket.AssignedTo,
                        LastWorkedDate = ticket.LastWorkedDate,
                        LastWorkedEmployee = ticket.LastWorkedEmployee
                    });
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No bug tickets found.");
            }

            return results;
        }

        public List<string> SelectAllFeatures()
        {
            List<string> result = new List<string>();
            foreach (var ticket in fakeBugTickets)
            {
                if (!result.Contains(ticket.Feature))
                {
                    result.Add(ticket.Feature);
                }
            }
            if (result.Count == 0)
            {
                throw new ArgumentException("No features found.");
            }
            return result;
        }

        public List<string> SelectAllStatus()
        {
            List<string> result = new List<string>();
            foreach (var ticket in fakeBugTickets)
            {
                if (!result.Contains(ticket.Status))
                {
                    result.Add(ticket.Status);
                }
            }
            if (result.Count == 0)
            {
                throw new ArgumentException("No statuses found.");
            }
            return result;
        }

        public List<string> SelectAllVersions()
        {
            List<string> result = new List<string>();
            foreach (var ticket in fakeBugTickets)
            {
                if (!result.Contains(ticket.VersionNumber))
                {
                    result.Add(ticket.VersionNumber);
                }
            }
            if (result.Count == 0)
            {
                throw new ArgumentException("No versions found.");
            }
            return result;
        }

        public List<BugTicket> SelectBugTicketByAssignedTo(int AssignedTo)
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.AssignedTo == AssignedTo)
                {
                    results.Add(ticket);
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No matching bug tickets found.");
            }

            return results;
        }

        public List<BugTicket> SelectBugTicketByFeature(string feature)
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.Feature == feature)
                {
                    results.Add(ticket);
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No matching bug tickets found.");
            }

            return results;
        }

        public List<BugTicket> SelectBugTicketsByArea(string area)
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.AreaName == area)
                {
                    results.Add(ticket);
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No matching bug tickets found.");
            }

            return results;
        }

        public List<BugTicket> SelectBugTicketsByStatus(string status)
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.Status == status)
                {
                    results.Add(ticket);
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No matching bug tickets found.");
            }

            return results;
        }

        public List<BugTicket> SelectBugTicketsByVersion(string version)
        {
            List<BugTicket> results = new List<BugTicket>();

            foreach (var ticket in fakeBugTickets)
            {
                if (ticket.VersionNumber == version)
                {
                    results.Add(ticket);
                }
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("No matching bug tickets found.");
            }

            return results;
        }

        public int UpdateBugReport(BugTicket oldBugTicket, BugTicket newBugTicket)
        {
            int result = 0;

            for(int i = 0; i < fakeBugTickets.Count; i++)
            {
                if (fakeBugTickets[i].BugTicketID == oldBugTicket.BugTicketID)
                {
                    fakeBugTickets[i] = newBugTicket;
                    result++;
                }
            }
        
            if (result != 1)
            {
               throw new ApplicationException();
            }

            return result;
        }

        public int AddBugReport(BugTicket bugTicket)
        {
            foreach (var ticket in fakeBugTickets)
            {
                if(ticket.BugTicketID == bugTicket.BugTicketID)
                {
                    throw new ApplicationException();
                }
            }
            fakeBugTickets.Add(bugTicket);
            return 1;
        }

        public List<ReportingItem> SelectStatistics()
        {
            List<ReportingItem> list = new List<ReportingItem>();
            ReportingItem reportingItem = new ReportingItem();
            reportingItem.Key = "Key";
            reportingItem.Value = "Value";
            list.Add(reportingItem);

            return list;
        }
    }
}
