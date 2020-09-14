using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static Site_Manager.Resources;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Site_Manager
{
    public class Site_Tickets : INotifyPropertyChanged
    {
        private long _Site_ID = 0;
        private long _Ticket_ID = 0;
        private long _Creator_User_ID = 0;
        private DateTime _Created_On;
        private DateTime _Due_On;
        private string _Brief_Desc = "";
        private string _Desc = "";
        private string _Notes = "";
        private int _Ticket_Status = 0;
        private int _Total_Tasks = 0;
        private int _Completed_Tasks = 0;
        private int _Active_Tasks = 0;
        private bool _Just_Created = false;

        private DateTime _Completed_TS;

        private string _Site = "";
        private string _Creator = "";

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public long Site_ID
        {
            get { return _Site_ID; }
            set
            {
                _Site_ID = value;
            }
        }

        public long Ticket_ID
        {
            get { return _Ticket_ID; }
            set
            {
                _Ticket_ID = value;
            }
        }

        public long Creator_User_ID
        {
            get { return _Creator_User_ID; }
            set
            {
                _Creator_User_ID = value;
            }
        }

        public DateTime Created_On
        {
            get { return _Created_On; }
            set
            {
                _Created_On = value;
            }
        }

        public DateTime Due_On
        {
            get { return _Due_On; }
            set
            {
                _Due_On = value;
            }
        }


        public DateTime Completed_TS
        {
            get { return _Completed_TS; }
            set
            {
                _Completed_TS = value;
            }
        }

        public string Brief_Desc
        {
            get { return _Brief_Desc; }
            set
            {
                _Brief_Desc = value;
            }
        }

        public string Desc
        {
            get { return _Desc; }
            set
            {
                _Desc = value;
            }
        }

        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
            }
        }

        public string Site
        {
            get { return _Site; }
            set
            {
                _Site = value;
            }
        }

        public string Creator
        {
            get { return _Creator; }
            set
            {
                _Creator = value;
            }
        }

        public int Ticket_Status
        {
            get { return _Ticket_Status; }
            set
            {
                _Ticket_Status = value;
            }
        }

        public int Total_Tasks
        {
            get { return _Total_Tasks; }
            set
            {
                _Total_Tasks = value;
            }
        }

        public int Completed_Tasks
        {
            get { return _Completed_Tasks; }
            set
            {
                _Completed_Tasks = value;
            }
        }

        public int Active_Tasks
        {
            get { return _Active_Tasks; }
            set
            {
                _Active_Tasks = value;
            }
        }

        public bool Just_Created
        {
            get { return _Just_Created; }
            set
            {
                _Just_Created = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //
        //  Function:   public int Generate_New()
        //
        //  Purpose:    Create a new ticket with some default values so we don't run into NULL's
        //
        public void Generate_New(long site_id, string site_name, long user_id, string user)
        {
            Site_ID = site_id;
            Ticket_ID = 0;
            Creator_User_ID = user_id;
            Created_On = DateTime.Now;
            Due_On = DateTime.Now;
            Brief_Desc = "New Ticket";
            Desc = "Basic New Ticket For Testing";
            Ticket_Status = 0;
            Completed_TS = DateTime.Now;
            Notes = "";
            Site = site_name;
            Creator = user;
            Total_Tasks = 0;
            Completed_Tasks = 0;
            Active_Tasks = 0;
            Just_Created = true;
        }

        public bool Load_Site_Ticket(long site_id, long user_id, bool all_tickets)
        {
            // Open the database and retrieve tickets for the current user or all of them if all is selected
            string connString = SQLConnString;

            // Query the tickets for this site for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblTickets);
            query.Append(" WHERE Site_ID = '");
            query.Append(site_id);
            query.Append("'");
            // If select all is not enabled, only show this users tickets
            if (all_tickets == false)
            {
                query.Append(" AND Creator_ID = '");
                query.Append(user_id);
                query.Append("'");
            }

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    using (reader)
                    {
                        reader.Read();
                        Site_ID = ((long)reader[0]);
                        Ticket_ID = ((long)reader[1]);
                        Creator_User_ID = ((long)reader[2]);
                        Created_On = ((DateTime)reader[3]);
                        Due_On = ((DateTime)reader[4]);
                        Brief_Desc = ((string)reader[5]);
                        Desc = ((string)reader[6]);
                        Ticket_Status = ((int)reader[7]);
                        Notes = ((string)reader[8]);
                        Total_Tasks = ((int)reader[9]);
                        Completed_Tasks = ((int)reader[10]);
                        Active_Tasks = ((int)reader[11]);
                    }
                    return true;
                }
                else
                    return false;
            }
        }

        public string Get_Site_Name(long Site_No)
        {
            Site = "Not Found";
            string connString = SQLConnString;

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("SELECT Short_Name FROM ");
            query.Append(tblSiteInfo);
            query.Append(" WHERE Site_ID = '");
            query.Append(Site_No.ToString());
            query.Append("'");

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    using (reader)
                    {
                        reader.Read();
                        Site = ((string)reader[0]);
                    }
                    return Site;
                }
                else
                    return Site;
            }
        }

        public bool Save_Ticket(bool New_Ticket)
        {
            string connString = SQLConnString;
            string SaveCmd = "";
            int SqlResult = 0;
            bool return_code = false;

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("");
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                if (New_Ticket)  // New site user
                {
                    query.Append("INSERT ");
                    query.Append(tblTickets);
                    query.Append(" ");
                    query.Append(tblTicketsFields);
                    query.Append(" ");
                    query.Append(tblTicketsInsert);
                    SaveCmd = query.ToString();
                    using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                    {
                        SqlCmd.Connection = sqlCon;
                        SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID);
                        SqlCmd.Parameters.AddWithValue("@creator_id", (int)Creator_User_ID);
                        SqlCmd.Parameters.AddWithValue("@created_on", (DateTime)Created_On);
                        SqlCmd.Parameters.AddWithValue("@due_on", (DateTime)Due_On);
                        SqlCmd.Parameters.AddWithValue("@brief_desc", (string)Brief_Desc);
                        SqlCmd.Parameters.AddWithValue("@desc", (string)Desc);
                        SqlCmd.Parameters.AddWithValue("@status", (int)Ticket_Status);
                        SqlCmd.Parameters.AddWithValue("@completed_ts", (DateTime)Completed_TS);
                        SqlCmd.Parameters.AddWithValue("@notes", (string)Notes);
                        SqlCmd.Parameters.AddWithValue("@total_tasks", (int)Total_Tasks);
                        SqlCmd.Parameters.AddWithValue("@completed_tasks", (int)Completed_Tasks);
                        SqlCmd.Parameters.AddWithValue("@active_tasks", (int)Active_Tasks);

                        try
                        {
                            if (sqlCon.State != ConnectionState.Open)
                                sqlCon.Open();
                            SqlResult = SqlCmd.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            MessageBox.Show(e.Message, "Failed To Save Ticket", MessageBoxButton.OK, MessageBoxImage.Error);
                            return_code = false;
                        }
                    }
                }
                else // Update existing ticket
                {
                    query.Append("UPDATE ");
                    query.Append(tblTickets);
                    query.Append(" ");
                    query.Append(tblTicketsUpdate);
                    query.Append(Ticket_ID.ToString());
                    SaveCmd = query.ToString();
                    using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                    {
                        SqlCmd.Connection = sqlCon;
                        SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID);
                        SqlCmd.Parameters.AddWithValue("@creator_id", (int)Creator_User_ID);
                        SqlCmd.Parameters.AddWithValue("@created_on", (DateTime)Created_On);
                        SqlCmd.Parameters.AddWithValue("@due_on", (DateTime)Due_On);
                        SqlCmd.Parameters.AddWithValue("@brief_desc", (string)Brief_Desc);
                        SqlCmd.Parameters.AddWithValue("@desc", (string)Desc);
                        SqlCmd.Parameters.AddWithValue("@status", (int)Ticket_Status);
                        SqlCmd.Parameters.AddWithValue("@completed_ts", (DateTime)Completed_TS);
                        SqlCmd.Parameters.AddWithValue("@notes", (string)Notes);
                        SqlCmd.Parameters.AddWithValue("@total_tasks", (int)Total_Tasks);
                        SqlCmd.Parameters.AddWithValue("@completed_tasks", (int)Completed_Tasks);
                        SqlCmd.Parameters.AddWithValue("@active_tasks", (int)Active_Tasks);

                        try
                        {
                            if (sqlCon.State != ConnectionState.Open)
                                sqlCon.Open();
                            SqlResult = SqlCmd.ExecuteNonQuery();
                            if (SqlResult == 0)
                                return_code = false;
                        }
                        catch (SqlException e)
                        {
                            MessageBox.Show(e.Message, "Failed To Save Ticket", MessageBoxButton.OK, MessageBoxImage.Error);
                            return_code = false;
                        }

                    }
                }
            }
            return return_code;
        }
    }
}
