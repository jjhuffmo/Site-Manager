using System;
using System.Collections.Generic;
using System.Text;
using static Site_Manager.Resources;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Data;
using System.Runtime.CompilerServices;

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

        private List<long> _Site_User_ID;
        private List<long> _Site_ID;
        private List<int> _User_ID;
        private List<string> _User_Name;
        private List<int> _Access;
        private List<bool> _View_Resources;
        private List<bool> _Add_Resources;
        private List<bool> _Modify_Resources;
        private List<bool> _Del_Resources;
        private List<bool> _View_Tickets;
        private List<bool> _Add_Tickets;
        private List<bool> _Mark_Delete;

        public List<long> Site_User_ID
        {
            get { return _Site_User_ID; }
            set
            {
                _Site_User_ID = value;
            }
        }

        public List<long> Site_ID
        {
            get { return _Site_ID; }
            set
            {
                _Site_ID = value;
            }
        }

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

        public List<bool> Mark_Delete
        {
            get { return _Mark_Delete; }
            set
            {
                _Mark_Delete = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            Site_User_ID = new List<long>();
            Site_ID = new List<long>();
            User_Name = new List<string>();
            User_ID = new List<int>();
            Access = new List<int>();
            View_Resources = new List<bool>();
            Add_Resources = new List<bool>();
            Modify_Resources = new List<bool>();
            Del_Resources = new List<bool>();
            View_Tickets = new List<bool>();
            Add_Tickets = new List<bool>();
            Mark_Delete = new List<bool>();
        }

        //
        //  Function:   public void Remove_User(int user_type, int index_no)
        //
        //  Arguments:  int user_type = Type of user to remove (0 = System User, 1 = Site)
        //              int index_no = Index number of user to remove from list
        //
        //  Purpose:    Removes an entire record of a user at a certain index
        //
        public void Remove_User(int user_type, int index_no)
        {
            User_Name.RemoveAt(index_no);
            User_ID.RemoveAt(index_no);
            Access.RemoveAt(index_no);
            if (user_type == 1)
            {
                Site_User_ID.RemoveAt(index_no);
                Site_ID.RemoveAt(index_no);
                View_Resources.RemoveAt(index_no);
                Add_Resources.RemoveAt(index_no);
                Modify_Resources.RemoveAt(index_no);
                Del_Resources.RemoveAt(index_no);
                View_Tickets.RemoveAt(index_no);
                Add_Tickets.RemoveAt(index_no);
            }
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
                    query.Append(" ORDER BY User_Name");
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
                            Site_User_ID.Add((long)reader[0]);
                            Site_ID.Add((long)reader[1]);
                            site_users.Add((int)reader[2]);
                            User_ID.Add((int)reader[2]);
                            View_Resources.Add((bool)reader[3]);
                            Add_Resources.Add((bool)reader[4]);
                            Modify_Resources.Add((bool)reader[5]);
                            Del_Resources.Add((bool)reader[6]);
                            View_Tickets.Add((bool)reader[7]);
                            Add_Tickets.Add((bool)reader[8]);
                            Mark_Delete.Add(false);
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

        //
        //  Function:   public void Save_List(int mode, long site_id = 0)
        //
        //  Arguments:  int mode = Mode of list to save (0 = All Users, 1 = Only Users For This Site)
        //              long site_id = Site ID to get the list for (if mode is 1)
        //
        //  Purpose:    Saves a list of all users (mode = 0) or ones granted rights to see a site (mode = 1)
        //
        public bool Save_List(int mode, long site_id = 0)
        {
            List<int> site_users = new List<int>();
            bool return_code = true;

            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;
            string SaveCmd = "";
            int SqlResult = 0;

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                switch (mode)
                {
                    // All Users
                    case 0:
                        for (int j = 0; j < User_ID.Count; j++)
                        {
                            StringBuilder query = new StringBuilder("");
                            if (User_ID[j] == 0)
                            {
                                query.Append("INSERT INTO ");
                                query.Append(tblUserInfo);
                                query.Append(" ");
                                query.Append(tblUserFields);
                                query.Append(" ");
                                query.Append(tblUserInsert);
                                SaveCmd = query.ToString();
                                using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                                {
                                    // Write the current site information to the database
                                    SqlCmd.Connection = sqlCon;
                                    SqlCmd.Parameters.AddWithValue("@user_name", User_Name[j]);
                                    SqlCmd.Parameters.AddWithValue("@access_level", Access[j]);

                                    try
                                    {
                                        if (sqlCon.State != ConnectionState.Open)
                                            sqlCon.Open();
                                        SqlResult = SqlCmd.ExecuteNonQuery();
                                    }
                                    catch (SqlException e)
                                    {
                                        if (e.Number == 2627)
                                            MessageBox.Show("A user with this name already exists.  Please select a unique user name and try again.", "Site Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                                        else
                                            MessageBox.Show(e.Message, "Failed To Save User", MessageBoxButton.OK, MessageBoxImage.Error);
                                        SqlResult = 0;
                                        return_code = false;
                                    }

                                }
                            }
                            else
                            {
                                query.Append("UPDATE ");
                                query.Append(tblUserInfo);
                                query.Append(" ");
                                query.Append(tblUserUpdate);
                                query.Append(User_ID.ToString());
                                SaveCmd = query.ToString(); using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                                {
                                    SqlCmd.Connection = sqlCon;
                                    SqlCmd.Parameters.AddWithValue("@user_name", User_Name);
                                    SqlCmd.Parameters.AddWithValue("@access_level", Access);

                                    try
                                    {
                                        if (sqlCon.State != ConnectionState.Open)
                                            sqlCon.Open();
                                        SqlResult = SqlCmd.ExecuteNonQuery();
                                    }
                                    catch (SqlException e)
                                    {
                                        MessageBox.Show(e.Message, "Failed To Save Site", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return_code = false;
                                    }

                                }
                            }
                        }
                        sqlCon.Close();
                        break;

                    case 1:
                        for (int j = 0; j < Site_ID.Count; j++)
                        {
                            StringBuilder query = new StringBuilder("");
                            if (Site_User_ID[j] == 0)  // New site user
                            {
                                query.Append("INSERT ");
                                query.Append(tblSiteUsers);
                                query.Append(" ");
                                query.Append(tblSiteUserFields);
                                query.Append(" ");
                                query.Append(tblSiteUserInsert);
                                SaveCmd = query.ToString();
                                using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                                {
                                    SqlCmd.Connection = sqlCon;
                                    SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID[j]);
                                    SqlCmd.Parameters.AddWithValue("@user_id", (int)User_ID[j]);
                                    SqlCmd.Parameters.AddWithValue("@view_all_resources", (bool)View_Resources[j]);
                                    SqlCmd.Parameters.AddWithValue("@add_resources", (bool)Add_Resources[j]);
                                    SqlCmd.Parameters.AddWithValue("@modify_resources", (bool)Modify_Resources[j]);
                                    SqlCmd.Parameters.AddWithValue("@delete_resources", (bool)Del_Resources[j]);
                                    SqlCmd.Parameters.AddWithValue("@view_all_tickets", (bool)View_Tickets[j]);
                                    SqlCmd.Parameters.AddWithValue("@add_tickets", (bool)Add_Tickets[j]);

                                    try
                                    {
                                        if (sqlCon.State != ConnectionState.Open)
                                            sqlCon.Open();
                                        SqlResult = SqlCmd.ExecuteNonQuery();
                                    }
                                    catch (SqlException e)
                                    {
                                        MessageBox.Show(e.Message, "Failed To Save Site Users", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return_code = false;
                                    }
                                }
                            }
                            else // Update existing site user
                            {
                                if (Mark_Delete[j] == true)
                                {
                                    query.Append("DELETE FROM ");
                                    query.Append(tblSiteUsers);
                                    query.Append(" WHERE Site_ID = '");
                                    query.Append(Site_ID[j].ToString());
                                    query.Append("' and User_ID = '");
                                    query.Append(User_ID[j].ToString());
                                    query.Append("'");
                                }
                                else
                                {
                                    query.Append("UPDATE ");
                                    query.Append(tblSiteUsers);
                                    query.Append(" ");
                                    query.Append(tblSiteUserUpdate);
                                    query.Append(Site_User_ID[j].ToString());
                                }
                                SaveCmd = query.ToString();
                                using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                                {
                                    SqlCmd.Connection = sqlCon;
                                    if (Mark_Delete[j] == false)
                                    {
                                        SqlCmd.Parameters.AddWithValue("@site_id", (long)Site_ID[j]);
                                        SqlCmd.Parameters.AddWithValue("@user_id", (int)User_ID[j]);
                                        SqlCmd.Parameters.AddWithValue("@view_all_resources", (bool)View_Resources[j]);
                                        SqlCmd.Parameters.AddWithValue("@add_resources", (bool)Add_Resources[j]);
                                        SqlCmd.Parameters.AddWithValue("@modify_resources", (bool)Modify_Resources[j]);
                                        SqlCmd.Parameters.AddWithValue("@delete_resources", (bool)Del_Resources[j]);
                                        SqlCmd.Parameters.AddWithValue("@view_all_tickets", (bool)View_Tickets[j]);
                                        SqlCmd.Parameters.AddWithValue("@add_tickets", (bool)Add_Tickets[j]);
                                    }
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
                                        MessageBox.Show(e.Message, "Failed To Save Site Users", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return_code = false;
                                    }

                                }
                            }
                        }
                        sqlCon.Close();
                        break;
                }
            }
            return return_code;
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
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private long _Site_User_ID;
        private long _Site_ID;
        private int _User_ID;
        private string _User_Name;
        private bool _View_Resources;
        private bool _Add_Resources;
        private bool _Modify_Resources;
        private bool _Del_Resources;
        private bool _View_Tickets;
        private bool _Add_Tickets;
        private bool _Changed;
        private bool _Mark_Delete;

        public long Site_User_ID
        {
            get { return _Site_User_ID; }
            set
            {
                _Site_User_ID = value;
                Changed = true;
            }
        }

        public long Site_ID
        {
            get { return _Site_ID; }
            set
            {
                _Site_ID = value;
                Changed = true;
            }
        }

        public int User_ID
        {
            get { return _User_ID; }
            set
            {
                _User_ID = value;
                Changed = true;
            }
        }

        public string User_Name
        {
            get { return _User_Name; }
            set
            {
                _User_Name = value;
                Changed = true;
            }
        }

        public bool View_Resources
        {
            get { return _View_Resources; }
            set
            {
                _View_Resources = value;
                Changed = true;
                OnPropertyChanged("View_Resources");
            }
        }

        public bool Add_Resources
        {
            get { return _Add_Resources; }
            set
            {
                _Add_Resources = value;
                Changed = true;
            }
        }

        public bool Modify_Resources
        {
            get { return _Modify_Resources; }
            set
            {
                _Modify_Resources = value;
                Changed = true;
            }
        }

        public bool Del_Resources
        {
            get { return _Del_Resources; }
            set
            {
                _Del_Resources = value;
                Changed = true;
            }
        }

        public bool View_Tickets
        {
            get { return _View_Tickets; }
            set
            {
                _View_Tickets = value;
                Changed = true;
            }
        }

        public bool Add_Tickets
        {
            get { return _Add_Tickets; }
            set
            {
                _Add_Tickets = value;
                Changed = true;
                OnPropertyChanged();
            }
        }

        public bool Changed
        {
            get { return _Changed; }
            set
            {
                _Changed = value;
                OnPropertyChanged("Changed");
            }
        }

        public bool Mark_Delete
        {
            get { return _Mark_Delete; }
            set
            {
                _Mark_Delete = value;
                OnPropertyChanged();
            }
        }

        public string Changed_Ind
        {
            get 
            {
                //OnPropertyChanged("Changed_Ind");
                return !Changed ? " " : "*";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Convert_DB_Users(DB_Users db_users, int index)
        {
            this.Site_User_ID = db_users.Site_User_ID[index];
            this.Site_ID = db_users.Site_ID[index];
            this.User_ID = db_users.User_ID[index];
            this.User_Name = db_users.User_Name[index];
            this.View_Resources = db_users.View_Resources[index];
            this.Add_Resources = db_users.Add_Resources[index];
            this.Modify_Resources = db_users.Modify_Resources[index];
            this.Del_Resources = db_users.Del_Resources[index];
            this.View_Tickets = db_users.View_Tickets[index];
            this.Add_Tickets = db_users.Add_Tickets[index];
            this.Mark_Delete = db_users.Mark_Delete[index];
            this.Changed = false;
        }

        // Update the current users options
        public void Update_Current_User(Users update_data )
        {
            this.View_Resources = update_data.View_Resources;
            this.Add_Resources = update_data.Add_Resources;
            this.Modify_Resources = update_data.Modify_Resources;
            this.Del_Resources = update_data.Del_Resources;
            this.View_Tickets = update_data.View_Tickets;
            this.Add_Tickets = update_data.Add_Tickets;
            this.Mark_Delete = update_data.Mark_Delete;
        }

        public Users Get_User_Settings(long site_id, int user_id)
        {
            DB_Users users = new DB_Users();
            Users blank_user = new Users();
            users.Get_List(1, site_id);
            for (int q = 0; q < users.User_Name.Count; q++)
            {
                Users user = new Users();
                user.Convert_DB_Users(users, q);
                if (user.User_ID == user_id)
                    return user;
            }
            return blank_user;
        }
    }
}
