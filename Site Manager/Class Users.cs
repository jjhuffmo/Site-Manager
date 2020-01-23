using System;
using System.Collections.Generic;
using System.Text;
using static Site_Manager.Resources;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Site_Manager
{
    public class DB_Users : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private List<int> _User_ID;
        private List<string> _User_Name;
        private List<int> _Access;
        private List<bool> _View_Resources;
        private List<bool> _Add_Resources;
        private List<bool> _Modify_Resources;
        private List<bool> _Del_Resources;
        private List<bool> _View_Tickets;
        private List<bool> _Add_Tickets;

        public List<int> User_ID
        {
            get { return _User_ID; }
            set
            {
                _User_ID = value;
            }
        }

        public List<string> User_Name
        {
            get { return _User_Name; }
            set
            {
                _User_Name = value;
            }
        }

        public List<int> Access
        {
            get { return _Access; }
            set
            {
                _Access = value;
            }
        }

        public List<bool> View_Resources
        {
            get { return _View_Resources; }
            set
            {
                _View_Resources = value;
            }
        }

        public List<bool> Add_Resources
        {
            get { return _Add_Resources; }
            set
            {
                _Add_Resources = value;
            }
        }

        public List<bool> Modify_Resources
        {
            get { return _Modify_Resources; }
            set
            {
                _Modify_Resources = value;
            }
        }

        public List<bool> Del_Resources
        {
            get { return _Del_Resources; }
            set
            {
                _Del_Resources = value;
            }
        }

        public List<bool> View_Tickets
        {
            get { return _View_Tickets; }
            set
            {
                _View_Tickets = value;
            }
        }

        public List<bool> Add_Tickets
        {
            get { return _Add_Tickets; }
            set
            {
                _Add_Tickets = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            User_Name = new List<string>();
            User_ID = new List<int>();
            Access = new List<int>();
            View_Resources = new List<bool>();
            Add_Resources = new List<bool>();
            Modify_Resources = new List<bool>();
            Del_Resources = new List<bool>();
            View_Tickets = new List<bool>();
            Add_Tickets = new List<bool>();
        }

        //
        //  Function:   public void Get_List(int mode, long site_id)
        //
        //  Arguments:  int mode = Mode of list to get (0 = All Users, 1 = Only Users For This Site)
        //              long site_id = Site ID to get the list for (if mode is 1)
        //
        //  Purpose:    Gets a list of all users (mode = 0) or ones granted rights to see a site (mode = 1)
        //
        public void Get_List(int mode, long site_id=0)
        {
            List<int> site_users = new List<int>();

            Initialize();

            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;

            // Query the User Database for the user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            switch (mode)
            {
                // All Users
                case 0:
                    query.Append(tblUserInfo);
                    using (SqlConnection sqlCon = new SqlConnection(connString))
                    {
                        sqlCon.Open();
                        SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                        using SqlDataReader reader = SqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            User_ID.Add((int)reader[0]);
                            User_Name.Add(String.Format("{0}", reader[1]));
                            Access.Add((int)reader[2]);

                        }
                        sqlCon.Close();
                    }
                    break;

                // Site_ID users
                case 1:
                    query.Append(tblSiteUsers);
                    query.Append(" WHERE Site_ID = ");
                    query.Append(site_id.ToString());
                    // Get list of User_ID's
                    using (SqlConnection sqlCon = new SqlConnection(connString))
                    {
                        sqlCon.Open();
                        SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                        using SqlDataReader reader = SqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            site_users.Add((int)reader[2]);
                            User_ID.Add((int)reader[2]);
                            View_Resources.Add((bool)reader[3]);
                            Add_Resources.Add((bool)reader[4]);
                            Modify_Resources.Add((bool)reader[5]);
                            Del_Resources.Add((bool)reader[6]);
                            View_Tickets.Add((bool)reader[7]);
                            Add_Tickets.Add((bool)reader[8]);
                        }
                        sqlCon.Close();
                    }

                    // Convert that list to actual names for display
                    foreach (int id in site_users)
                    {
                        query.Clear();
                        query.Append("SELECT * FROM ");
                        query.Append(tblUserInfo);
                        query.Append(" WHERE User_ID = ");
                        query.Append(id.ToString());
                        using (SqlConnection sqlCon = new SqlConnection(connString))
                        {
                            sqlCon.Open();
                            SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                            using SqlDataReader reader = SqlCmd.ExecuteReader();
                            while (reader.Read())
                            {
                                User_Name.Add(String.Format("{0}", reader[1]));
                            }
                            sqlCon.Close();
                        }
                    }
                    break;
            }


        }
    }
    public class Users : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _Changed = true;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private int _User_ID;
        private string _User_Name;
        private bool _View_Resources;
        private bool _Add_Resources;
        private bool _Modify_Resources;
        private bool _Del_Resources;
        private bool _View_Tickets;
        private bool _Add_Tickets;
        private bool _Changed;

        public int User_ID
        {
            get { return _User_ID; }
            set
            {
                _User_ID = value;
                _Changed = true;
            }
        }

        public string User_Name
        {
            get { return _User_Name; }
            set
            {
                _User_Name = value;
                _Changed = true;
            }
        }

        public bool View_Resources
        {
            get { return _View_Resources; }
            set
            {
                _View_Resources = value;
                _Changed = true;
            }
        }

        public bool Add_Resources
        {
            get { return _Add_Resources; }
            set
            {
                _Add_Resources = value;
                _Changed = true;
            }
        }

        public bool Modify_Resources
        {
            get { return _Modify_Resources; }
            set
            {
                _Modify_Resources = value;
                _Changed = true;
            }
        }

        public bool Del_Resources
        {
            get { return _Del_Resources; }
            set
            {
                _Del_Resources = value;
                _Changed = true;
            }
        }

        public bool View_Tickets
        {
            get { return _View_Tickets; }
            set
            {
                _View_Tickets = value;
                _Changed = true;
            }
        }

        public bool Add_Tickets
        {
            get { return _Add_Tickets; }
            set
            {
                _Add_Tickets = value;
                _Changed = true;
            }
        }

        public bool Changed
        {
            get { return _Changed; }
            set
            {
                _Changed = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Convert_DB_Users(DB_Users db_users, int index)
        {
            this.User_ID = db_users.User_ID[index];
            this.User_Name = db_users.User_Name[index];
            this.View_Resources = db_users.View_Resources[index];
            this.Add_Resources = db_users.Add_Resources[index];
            this.Modify_Resources = db_users.Modify_Resources[index];
            this.Del_Resources = db_users.Del_Resources[index];
            this.View_Tickets = db_users.View_Tickets[index];
            this.Add_Tickets = db_users.Add_Tickets[index];
            this.Changed = false;
        }
    }
}
