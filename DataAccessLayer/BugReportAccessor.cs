using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace DataAccessLayer
{
    public class BugReportAccessor : IBugReportAccessor
    {
        public List<string> SelectAllAreas()
        {
            List<string> areas = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_product_areas";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    areas = new List<string>();
                    while (reader.Read())
                    {
                        areas.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No areas found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return areas;
        }

        public List<BugTicket> SelectAllBugTickets()
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_active_bug_tickets";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }



                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public List<string> SelectAllFeatures()
        {
            List<string> features = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_feature";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    features = new List<string>();
                    while (reader.Read())
                    {
                        features.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No features found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return features;
        }

        public List<string> SelectAllStatus()
        {
            List<string> statuses = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_bug_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    statuses = new List<string>();
                    while (reader.Read())
                    {
                        statuses.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No statuses found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return statuses;
        }

        public List<string> SelectAllVersions()
        {
            List<string> versions = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_product_versions";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    versions = new List<string>();
                    while (reader.Read())
                    {
                        versions.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("No versions found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return versions;
        }

        public List<BugTicket> SelectBugTicketByAssignedTo(int AssignedTo)
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_AssignedTo";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AssignedTo", SqlDbType.Int);
            cmd.Parameters["@AssignedTo"].Value = AssignedTo;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }

                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public BugTicket SelectBugTicketByBugTicketID(int bugTicketID)
        {
            BugTicket bugTicket = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_BugTicketID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BugTicketID", SqlDbType.Int);
            cmd.Parameters["@BugTicketID"].Value = bugTicketID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    bugTicket = new BugTicket();

                    bugTicket.BugTicketID = reader.GetInt32(0);
                    bugTicket.BugDate = reader.GetDateTime(1);
                    bugTicket.SubmitID = reader.GetInt32(2);
                    bugTicket.VersionNumber = reader.GetString(3);
                    bugTicket.AreaName = reader.GetString(4);
                    bugTicket.Description = reader.GetString(5);
                    bugTicket.Status = reader.GetString(6);
                    bugTicket.Active = reader.GetBoolean(11);

                    if (reader.IsDBNull(7))
                    {
                        bugTicket.Feature = "";
                    }
                    else
                    {
                        bugTicket.Feature = reader.GetString(7);
                    }

                    if (reader.IsDBNull(8))
                    {
                        bugTicket.AssignedTo = 1;
                    }
                    else
                    {
                        bugTicket.AssignedTo = reader.GetInt32(8);

                    }

                    if (reader.IsDBNull(9))
                    {
                        bugTicket.LastWorkedDate = new DateTime();
                    }
                    else
                    {
                        bugTicket.LastWorkedDate = reader.GetDateTime(9);
                    }

                    if (reader.IsDBNull(10))
                    {
                        bugTicket.LastWorkedEmployee = 1;
                    }
                    else
                    {
                        bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug ticket found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTicket;
        }

        public List<BugTicket> SelectBugTicketByFeature(string feature)
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_Feature";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Feature", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Feature"].Value = feature;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }

                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public List<BugTicket> SelectBugTicketsByArea(string area)
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_AreaName";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AreaName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@AreaName"].Value = area;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }

                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public List<BugTicket> SelectBugTicketsByStatus(string status)
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_Status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            cmd.Parameters["@Status"].Value = status;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }

                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public List<BugTicket> SelectBugTicketsByVersion(string version)
        {
            List<BugTicket> bugTickets = null;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_active_bug_ticket_by_VersionNumber";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VersionNumber", SqlDbType.NVarChar, 16);
            cmd.Parameters["@VersionNumber"].Value = version;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    bugTickets = new List<BugTicket>();
                    while (reader.Read())
                    {
                        BugTicket bugTicket = new BugTicket();

                        bugTicket.BugTicketID = reader.GetInt32(0);
                        bugTicket.BugDate = reader.GetDateTime(1);
                        bugTicket.SubmitID = reader.GetInt32(2);
                        bugTicket.VersionNumber = reader.GetString(3);
                        bugTicket.AreaName = reader.GetString(4);
                        bugTicket.Description = reader.GetString(5);
                        bugTicket.Status = reader.GetString(6);
                        bugTicket.Active = reader.GetBoolean(11);

                        if (reader.IsDBNull(7))
                        {
                            bugTicket.Feature = "";
                        }
                        else
                        {
                            bugTicket.Feature = reader.GetString(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            bugTicket.AssignedTo = 1;
                        }
                        else
                        {
                            bugTicket.AssignedTo = reader.GetInt32(8);

                        }

                        if (reader.IsDBNull(9))
                        {
                            bugTicket.LastWorkedDate = new DateTime();
                        }
                        else
                        {
                            bugTicket.LastWorkedDate = reader.GetDateTime(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            bugTicket.LastWorkedEmployee = 1;
                        }
                        else
                        {
                            bugTicket.LastWorkedEmployee = reader.GetInt32(10);
                        }

                        bugTickets.Add(bugTicket);
                    }
                }
                else
                {
                    throw new ApplicationException("No matching bug tickets found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return bugTickets;
        }

        public int UpdateBugReport(BugTicket oldBugTicket, BugTicket newBugTicket)
        {
            throw new NotImplementedException();
        }

        public int AddBugReport(BugTicket bugTicket)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_create_bug_ticket";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BugDate", SqlDbType.Date);
            cmd.Parameters.Add("@SubmitID", SqlDbType.Int);
            cmd.Parameters.Add("@VersionNumber", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@AreaName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 4000);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Feature", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@AssignedTo", SqlDbType.Int);

            cmd.Parameters["@BugDate"].Value = bugTicket.BugDate;
            cmd.Parameters["@SubmitID"].Value = bugTicket.SubmitID;
            cmd.Parameters["@VersionNumber"].Value = bugTicket.VersionNumber;
            cmd.Parameters["@AreaName"].Value = bugTicket.AreaName;
            cmd.Parameters["@Description"].Value = bugTicket.Description;
            cmd.Parameters["@Status"].Value = bugTicket.Status;
            cmd.Parameters["@Feature"].Value = bugTicket.Feature;
            cmd.Parameters["@AssignedTo"].Value = bugTicket.AssignedTo;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<ReportingItem> SelectStatistics()
        {
            throw new NotImplementedException();
        }
    }
}
