using System;
using System.Collections.Generic;
using System.Text;
using static Site_Manager.Resources;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Site_Manager
{
    public class Users : INotifyPropertyChanged
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

        public List<int> User_ID
        {
            get { return _User_ID; }
            set
            {
                _User_ID = value;
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

        public List<string> User_Name
        {
            get { return _User_Name; }
            set
            {
                _User_Name = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            User_Name = new List<string>();
            User_ID = new List<int>();
            Access = new List<int>();
        }

        //
        //  Function:   public void Get_List(int mode, long site_id)
        //
        //  Arguments:  int mode = Mode of list to get (0 = All Users, 1 = Only Users For This Site)
        //              long site_id = Site ID to get the list for (if mode is 1)
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
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
                            User_Name.Add(String.Format("{0}", reader[1]));
                            Access.Add((int)reader[2]);
                            User_ID.Add((int)reader[0]);
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
                                Access.Add((int)reader[2]);
                                User_ID.Add((int)reader[0]);
                            }
                            sqlCon.Close();
                        }
                    }
                    break;
            }


        }
    }
}
