using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class BugReportManager : IBugReportManager
    {
        IBugReportAccessor _bugReportAccessor = null;
        IEmployeeAccessor _employeeAccessor = null;
        
        public BugReportManager()
        {
            _bugReportAccessor = new BugReportAccessor();
            _employeeAccessor = new EmployeeAccessor();
        }

        public BugReportManager(IBugReportAccessor bugReportAccessor, IEmployeeAccessor employeeAccessor)
        {
            _bugReportAccessor = bugReportAccessor;
            _employeeAccessor = employeeAccessor;
        }

        public List<string> GetAllAreas()
        {
            return _bugReportAccessor.SelectAllAreas();
        }

        public List<BugTicketVM> GetAllBugTickets()
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectAllBugTickets();
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        private List<BugTicketVM> CreateBugTicketVMListFromBugTicketAndEmployees(List<Employee> employees, List<BugTicket> bugTickets)
        {
            List < BugTicketVM > bugTicketsVM = new List<BugTicketVM>();
            foreach (var ticket in bugTickets)
            {
                string assignedToName = "";
                string lastWorkedName = "";

                foreach (var employee in employees)
                {
                    if (employee.EmployeeID == ticket.AssignedTo)
                    {
                        assignedToName = employee.GivenName + " " + employee.FamilyName;
                    }
                    if (employee.EmployeeID == ticket.LastWorkedEmployee)
                    {
                        lastWorkedName = employee.GivenName + " " + employee.FamilyName;
                    }
                }

                bugTicketsVM.Add(new BugTicketVM()
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
                    LastWorkedEmployee = ticket.LastWorkedEmployee,
                    Active = ticket.Active,
                    AssignedToName = assignedToName,
                    LastWorkedName = lastWorkedName
                });
            }
            return bugTicketsVM;
        }

        public List<string> GetAllFeatures()
        {
            return _bugReportAccessor.SelectAllFeatures();
        }

        public List<string> GetAllStatus()
        {
            return _bugReportAccessor.SelectAllStatus();
        }

        public List<string> GetAllVersions()
        {
            return _bugReportAccessor.SelectAllVersions();
        }

        public BugTicketVM GetBugTicket(int bugTicketID)
        {
            BugTicket bugTicket = _bugReportAccessor.SelectBugTicketByBugTicketID(bugTicketID);
            BugTicketVM bugTicketVM = CreateBugTicketVMFromBugTicket(bugTicket);
            return bugTicketVM;
        }

        public List<BugTicketVM> GetBugTicketsByArea(string area)
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByArea(area);
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByAssignedTo(int assignedTo)
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketByAssignedTo(assignedTo);
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByFeature(string feature)
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketByFeature(feature);
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByStatus(string status)
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByStatus(status);
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByVersion(string version)
        {
            List<Employee> employees = _employeeAccessor.SelectAllEmployees();
            List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByVersion(version);
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();
            bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);

            return bugTicketVMs;
        }

        public bool UpdateBugReport(BugTicket oldBugTicket, BugTicket newBugTicket)
        {
            throw new NotImplementedException();
        }

        private BugTicketVM CreateBugTicketVMFromBugTicket(BugTicket bugTicket)
        {
            EmployeeVM assignedTo = _employeeAccessor.SelectEmployeeByEmployeeID(bugTicket.AssignedTo);
            EmployeeVM lastWorked = _employeeAccessor.SelectEmployeeByEmployeeID(bugTicket.LastWorkedEmployee);

            return new BugTicketVM()
            {
                BugTicketID = bugTicket.BugTicketID,
                BugDate = bugTicket.BugDate,
                SubmitID = bugTicket.SubmitID,
                VersionNumber = bugTicket.VersionNumber,
                AreaName = bugTicket.AreaName,
                Description = bugTicket.Description,
                Status = bugTicket.Status,
                Feature = bugTicket.Feature,
                AssignedTo = bugTicket.AssignedTo,
                LastWorkedDate = bugTicket.LastWorkedDate,
                LastWorkedEmployee = bugTicket.LastWorkedEmployee,
                Active = bugTicket.Active,
                AssignedToName = assignedTo.GivenName + " " + assignedTo.FamilyName,
                LastWorkedName = lastWorked.GivenName + " " + lastWorked.FamilyName
            };
        }
    }
}
