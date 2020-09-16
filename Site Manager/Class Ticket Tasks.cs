using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using static Site_Manager.Resources;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Site_Manager
{
    public class Tickets_Tasks : INotifyPropertyChanged
    {
        private long _Site_ID = 0;
        private long _Ticket_ID = 0;
        private long _Ticket_Task_ID = 0;
        private DateTime _Created_On;
        private int _Creator_ID = 0;
        private string _Creator = "";
        private int _Task_Status = 0;
        private long _Task_ID = 0;
        private int _Priority = 0;
        private string _Task_Overview = "";
        private string _Task_Details = "";
        private int _Assigned_User_ID = 0;
        private int _Progress = 0;
        private DateTime _Due_On;
        private DateTime _Started_TS;
        private DateTime _Completed_TS;
        private string _Notes = "";
        private bool _Alarm1_Enabled = false;
        private DateTime _Alarm1;
        private DateTime _Alarm1_Ack_TS;
        private bool _Alarm2_Enabled = false;
        private DateTime _Alarm2;
        private DateTime _Alarm2_Ack_TS;
        private bool _Alarm3_Enabled = false;
        private DateTime _Alarm3;
        private DateTime _Alarm3_Ack_TS;
        private bool _Late_Alarm = false;
        private bool _Just_Created = false;
        private bool _Saved = false;
        private bool _Canceled = false;
        private bool _Changed = false;

        private string _Site = "";
        private string _Assigned_User = "";

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

        public long Ticket_Task_ID
        {
            get { return _Ticket_Task_ID; }
            set
            {
                _Ticket_Task_ID = value;
            }
        }

        public long Task_ID
        {
            get { return _Task_ID; }
            set
            {
                _Task_ID = value;
            }
        }

        public int Creator_ID
        {
            get { return _Creator_ID; }
            set
            {
                _Creator_ID = value;
            }
        }

        public int Assigned_User_ID
        {
            get { return _Assigned_User_ID; }
            set
            {
                _Assigned_User_ID = value;
            }
        }

        public int Task_Status
        {
            get { return _Task_Status; }
            set
            {
                _Task_Status = value;
            }
        }

        public int Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
            }
        }

        public int Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
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

        public DateTime Started_TS
        {
            get { return _Started_TS; }
            set
            {
                _Started_TS = value;
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

        public DateTime Alarm1
        {
            get { return _Alarm1; }
            set
            {
                _Alarm1 = value;
            }
        }

        public DateTime Alarm1_Ack_TS
        {
            get { return _Alarm1_Ack_TS; }
            set
            {
                _Alarm1_Ack_TS = value;
            }
        }

        public DateTime Alarm2
        {
            get { return _Alarm2; }
            set
            {
                _Alarm2 = value;
            }
        }

        public DateTime Alarm2_Ack_TS
        {
            get { return _Alarm2_Ack_TS; }
            set
            {
                _Alarm2_Ack_TS = value;
            }
        }

        public DateTime Alarm3
        {
            get { return _Alarm3; }
            set
            {
                _Alarm3 = value;
            }
        }

        public DateTime Alarm3_Ack_TS
        {
            get { return _Alarm3_Ack_TS; }
            set
            {
                _Alarm3_Ack_TS = value;
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

        public string Task_Overview
        {
            get { return _Task_Overview; }
            set
            {
                _Task_Overview = value;
            }
        }

        public string Task_Details
        {
            get { return _Task_Details; }
            set
            {
                _Task_Details = value;
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

        public string Assigned_User
        {
            get { return _Assigned_User; }
            set
            {
                _Assigned_User = value;
            }
        }

        public bool Alarm1_Enabled
        {
            get { return _Alarm1_Enabled; }
            set
            {
                _Alarm1_Enabled = value;
            }
        }

        public bool Alarm2_Enabled
        {
            get { return _Alarm2_Enabled; }
            set
            {
                _Alarm2_Enabled = value;
            }
        }
        public bool Alarm3_Enabled

        {
            get { return _Alarm3_Enabled; }
            set
            {
                _Alarm3_Enabled = value;
            }
        }

        public bool Late_Alarm
        {
            get { return _Late_Alarm; }
            set
            {
                _Late_Alarm = value;
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
        public bool Changed
        {
            get { return _Changed; }
            set
            {
                _Changed = value;
                OnPropertyChanged();
            }
        }


        public bool Saved
        {
            get { return _Saved; }
            set
            {
                _Saved = value;
                OnPropertyChanged();
            }
        }

        public bool Canceled
        {
            get { return _Canceled; }
            set
            {
                _Canceled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //
        //  Function:   public int Generate_New()
        //
        //  Purpose:    Create a new ticket with some default values so we don't run into NULL's
        //
        public void Generate_New(Users cuser, Site_Tickets cticket)
        {
            Site_ID = cticket.Site_ID;
            Ticket_ID = cticket.Ticket_ID;
            Ticket_Task_ID = 0;
            Creator_ID = 0;
            Creator = cuser.User_Name;
            Created_On = DateTime.Now;
            Task_Status = 0;
            Task_ID = 0;
            Priority = 0;
            Task_Overview = "New Task";
            Task_Details = "";
            Assigned_User_ID = cuser.User_ID;
            Progress = 0;
            Due_On = DateTime.Now;
            Started_TS = DateTime.Parse("Jan 1, 1980");
            Completed_TS = DateTime.Parse("Jan 1, 1980");
            Notes = "";
            Alarm1 = DateTime.Now;
            Alarm1_Enabled = false;
            Alarm1_Ack_TS = DateTime.Now;
            Alarm2 = DateTime.Now;
            Alarm2_Enabled = false;
            Alarm2_Ack_TS = DateTime.Now;
            Alarm3 = DateTime.Now;
            Alarm3_Enabled = false;
            Alarm3_Ack_TS = DateTime.Now;
            Late_Alarm = false;
            Site = cticket.Site;
            Assigned_User = cuser.User_Name;
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

        public bool Save_Task(bool New_Task)
        {
            string connString = SQLConnString;
            string SaveCmd = "";
            int SqlResult = 0;
            bool return_code = false;

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("");
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                if (New_Task)  // New site user
                {
                    query.Append("INSERT INTO ");
                    query.Append(tblTasks);
                    query.Append(" ");
                    query.Append(tblTasksFields);
                    query.Append(" ");
                    query.Append(tblTasksInsert);
                    SaveCmd = query.ToString();
                    using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                    {
                        SqlCmd.Connection = sqlCon;
                        SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID);
                        SqlCmd.Parameters.AddWithValue("@creator_id", (int)Creator_ID);
                        SqlCmd.Parameters.AddWithValue("@created_on", (DateTime)Created_On);
                        SqlCmd.Parameters.AddWithValue("@task_status", (int)Task_Status);
                        SqlCmd.Parameters.AddWithValue("@task_id", (long)Task_ID);
                        SqlCmd.Parameters.AddWithValue("@priority", (int)Priority);
                        SqlCmd.Parameters.AddWithValue("@task_overview", (string)Task_Overview);
                        SqlCmd.Parameters.AddWithValue("@task_details", (string)Task_Details);
                        SqlCmd.Parameters.AddWithValue("@assigned_user_id", (int)Assigned_User_ID);
                        SqlCmd.Parameters.AddWithValue("@progress", (int)Progress);
                        SqlCmd.Parameters.AddWithValue("@due_on", (DateTime)Due_On);
                        SqlCmd.Parameters.AddWithValue("@started_ts", (DateTime)Started_TS);
                        SqlCmd.Parameters.AddWithValue("@completed_ts", (DateTime)Completed_TS);
                        SqlCmd.Parameters.AddWithValue("@notes", (string)Notes);
                        SqlCmd.Parameters.AddWithValue("@alarm1_enabled", (bool)Alarm1_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm1", (DateTime)Alarm1);
                        SqlCmd.Parameters.AddWithValue("@alarm1_ack_ts", (DateTime)Alarm1_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@alarm2_enabled", (bool)Alarm2_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm2", (DateTime)Alarm2);
                        SqlCmd.Parameters.AddWithValue("@alarm2_ack_ts", (DateTime)Alarm2_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@alarm3_enabled", (bool)Alarm3_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm3", (DateTime)Alarm3);
                        SqlCmd.Parameters.AddWithValue("@alarm3_ack_ts", (DateTime)Alarm3_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@late_alarm", (bool)Late_Alarm);

                        try
                        {
                            if (sqlCon.State != ConnectionState.Open)
                                sqlCon.Open();
                            SqlResult = SqlCmd.ExecuteNonQuery();
                            Changed = false;
                            return_code = true;
                        }
                        catch (SqlException e)
                        {
                            MessageBox.Show(e.Message, "Failed To Save Task", MessageBoxButton.OK, MessageBoxImage.Error);
                            return_code = false;
                        }
                    }
                }
                else // Update existing ticket
                {
                    query.Append("UPDATE ");
                    query.Append(tblTasks);
                    query.Append(" ");
                    query.Append(tblTasksUpdate);
                    query.Append(Ticket_Task_ID.ToString());
                    SaveCmd = query.ToString();
                    using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                    {
                        SqlCmd.Connection = sqlCon;
                        SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID);
                        SqlCmd.Parameters.AddWithValue("@creator_id", (int)Creator_ID);
                        SqlCmd.Parameters.AddWithValue("@created_on", (DateTime)Created_On);
                        SqlCmd.Parameters.AddWithValue("@task_status", (int)Task_Status);
                        SqlCmd.Parameters.AddWithValue("@task_id", (long)Task_ID);
                        SqlCmd.Parameters.AddWithValue("@priority", (int)Priority);
                        SqlCmd.Parameters.AddWithValue("@task_overview", (string)Task_Overview);
                        SqlCmd.Parameters.AddWithValue("@task_details", (string)Task_Details);
                        SqlCmd.Parameters.AddWithValue("@assigned_user_id", (int)Assigned_User_ID);
                        SqlCmd.Parameters.AddWithValue("@progress", (int)Progress);
                        SqlCmd.Parameters.AddWithValue("@due_on", (DateTime)Due_On);
                        SqlCmd.Parameters.AddWithValue("@started_ts", (DateTime)Started_TS);
                        SqlCmd.Parameters.AddWithValue("@completed_ts", (DateTime)Completed_TS);
                        SqlCmd.Parameters.AddWithValue("@notes", (string)Notes);
                        SqlCmd.Parameters.AddWithValue("@alarm1_enabled", (bool)Alarm1_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm1", (DateTime)Alarm1);
                        SqlCmd.Parameters.AddWithValue("@alarm1_ack_ts", (DateTime)Alarm1_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@alarm2_enabled", (bool)Alarm2_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm2", (DateTime)Alarm2);
                        SqlCmd.Parameters.AddWithValue("@alarm2_ack_ts", (DateTime)Alarm2_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@alarm3_enabled", (bool)Alarm3_Enabled);
                        SqlCmd.Parameters.AddWithValue("@alarm3", (DateTime)Alarm3);
                        SqlCmd.Parameters.AddWithValue("@alarm3_ack_ts", (DateTime)Alarm3_Ack_TS);
                        SqlCmd.Parameters.AddWithValue("@late_alarm", (bool)Late_Alarm);

                        try
                        {
                            if (sqlCon.State != ConnectionState.Open)
                                sqlCon.Open();
                            SqlResult = SqlCmd.ExecuteNonQuery();
                            if (SqlResult == 0)
                                return_code = false;
                            else
                            {
                                return_code = true;
                                Changed = false;
                            }
                        }
                        catch (SqlException e)
                        {
                            MessageBox.Show(e.Message, "Failed To Update Task", MessageBoxButton.OK, MessageBoxImage.Error);
                            return_code = false;
                        }

                    }
                }
            }
            return return_code;
        }
    }
}
