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
            List<string> areas = new List<string>();

            try
            {
                areas = _bugReportAccessor.SelectAllAreas();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retireving program areas.", ex);
            }
            return areas;
        }

        public List<BugTicketVM> GetAllBugTickets()
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = new List<BugTicket>();
                try
                {
                    bugTickets = _bugReportAccessor.SelectAllBugTickets();

                }
                catch (Exception ex)
                {

                    throw new ApplicationException("bugTickets error", ex);
                }
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public List<string> GetAllFeatures()
        {
            List<string> features = new List<string>();
            try
            {
                features = _bugReportAccessor.SelectAllFeatures();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving features.", ex);
            }
            return features;
        }

        public List<string> GetAllStatus()
        {
            List<string> statuses = new List<string>();
            try
            {
                statuses = _bugReportAccessor.SelectAllStatus();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving statuses.", ex);
            }
            return statuses;
        }

        public List<string> GetAllVersions()
        {
            List<string> versions = new List<string>();
            try
            {
                versions = _bugReportAccessor.SelectAllVersions();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving versions.", ex);
            }
            return versions;
        }

        public BugTicketVM GetBugTicket(int bugTicketID)
        {
            BugTicketVM bugTicketVM = null;
            try
            {
                BugTicket bugTicket = _bugReportAccessor.SelectBugTicketByBugTicketID(bugTicketID);
                bugTicketVM = CreateBugTicketVMFromBugTicket(bugTicket);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving bug ticket.", ex);
            }
            return bugTicketVM;
        }

        public List<BugTicketVM> GetBugTicketsByArea(string area)
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByArea(area);
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByAssignedTo(int assignedTo)
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketByAssignedTo(assignedTo);
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByFeature(string feature)
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketByFeature(feature);
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByStatus(string status)
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByStatus(status);
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public List<BugTicketVM> GetBugTicketsByVersion(string version)
        {
            List<BugTicketVM> bugTicketVMs = new List<BugTicketVM>();

            try
            {
                List<Employee> employees = _employeeAccessor.SelectAllEmployees();
                List<BugTicket> bugTickets = _bugReportAccessor.SelectBugTicketsByVersion(version);
                bugTicketVMs = CreateBugTicketVMListFromBugTicketAndEmployees(employees, bugTickets);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving tickets.", ex);
            }
            return bugTicketVMs;
        }

        public bool UpdateBugReport(BugTicket oldBugTicket, BugTicket newBugTicket)
        {
            throw new NotImplementedException();
        }

        private BugTicketVM CreateBugTicketVMFromBugTicket(BugTicket bugTicket)
        {
            BugTicketVM bugTicketVM = new BugTicketVM();
            try
            {
                if(bugTicket.AssignedTo == 1)
                {
                    bugTicketVM.AssignedToName = "Unassigned";
                } 
                else
                {
                    EmployeeVM assignedTo = _employeeAccessor.SelectEmployeeByEmployeeID(bugTicket.AssignedTo);
                    bugTicketVM.AssignedToName = assignedTo.GivenName + " " + assignedTo.FamilyName;
                }

                if(bugTicket.LastWorkedEmployee == 1)
                {
                    bugTicketVM.LastWorkedName = "Unassigned";
                }
                else
                {
                    EmployeeVM lastWorked = _employeeAccessor.SelectEmployeeByEmployeeID(bugTicket.LastWorkedEmployee);
                    bugTicketVM.LastWorkedName = lastWorked.GivenName + " " + lastWorked.FamilyName;
                }

                bugTicketVM.BugTicketID = bugTicket.BugTicketID;
                bugTicketVM.BugDate = bugTicket.BugDate;
                bugTicketVM.SubmitID = bugTicket.SubmitID;
                bugTicketVM.VersionNumber = bugTicket.VersionNumber;
                bugTicketVM.AreaName = bugTicket.AreaName;
                bugTicketVM.Description = bugTicket.Description;
                bugTicketVM.Status = bugTicket.Status;
                bugTicketVM.Feature = bugTicket.Feature;
                bugTicketVM.AssignedTo = bugTicket.AssignedTo;
                bugTicketVM.LastWorkedDate = bugTicket.LastWorkedDate;
                bugTicketVM.LastWorkedEmployee = bugTicket.LastWorkedEmployee;
                bugTicketVM.Active = bugTicket.Active;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error converting ticket.", ex);
            }
            return bugTicketVM;
        }

        private List<BugTicketVM> CreateBugTicketVMListFromBugTicketAndEmployees(List<Employee> employees, List<BugTicket> bugTickets)
        {
            List<BugTicketVM> bugTicketsVM = new List<BugTicketVM>();

            try
            {
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
                        else
                        {
                            assignedToName = "Unassigned";
                        }
                        if (employee.EmployeeID == ticket.LastWorkedEmployee)
                        {
                            lastWorkedName = employee.GivenName + " " + employee.FamilyName;
                        }
                        else
                        {
                            lastWorkedName = "Unassigned";
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

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error populating list.", ex);
            }
            return bugTicketsVM;
        }
    }
}
