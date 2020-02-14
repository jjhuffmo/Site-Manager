using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using static Site_Manager.Resources;
using System.Data.SqlClient;

namespace Site_Manager
{
    public class Sites : INotifyPropertyChanged
    {

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public string _Short_Name;
        public string _Full_Name;
        public string _Customer_Name;
        public string _Address;
        public long _Site_ID;

        public long Site_ID
        {
            get { return _Site_ID; }
            set
            {
                _Site_ID = value;
                OnPropertyChanged();
            }
        }
        public string Short_Name
        {
            get { return _Short_Name; }
            set
            {
                _Short_Name = value;
                OnPropertyChanged();
            }
        }
        public string Full_Name
        {
            get { return _Full_Name; }
            set
            {
                _Full_Name = value;
                OnPropertyChanged();
            }
        }
        public string Customer_Name
        {
            get { return _Customer_Name; }
            set
            {
                _Customer_Name = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        //
        //  Function:   public int Save_Site(int mode)
        //
        //  Arguments:  int mode = Mode of save (0 = Save New, 1 = Update)
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public void Initialize()
        {
            Short_Name = "New Site";
        }

        //
        //  Function:   public int Save_Site(int mode)
        //
        //  Arguments:  int mode = Mode of save (0 = Save New, 1 = Update)
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public int Save_Site(int mode)
        {
            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;
            string SaveCmd = "";
            int SqlResult = 0;
            StringBuilder query = new StringBuilder("");

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                switch (mode)
                {
                    case 0:
                        query.Append("INSERT INTO ");
                        query.Append(tblSiteInfo);
                        query.Append(" ");
                        query.Append(tblSiteFields);
                        query.Append(" ");
                        query.Append(tblSiteInsert);
                        SaveCmd = query.ToString();
                        using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                        {
                            // Write the current site information to the database
                            SqlCmd.Connection = sqlCon;
                            SqlCmd.Parameters.AddWithValue("@short_name", Short_Name);
                            SqlCmd.Parameters.AddWithValue("@full_name", Full_Name);
                            SqlCmd.Parameters.AddWithValue("@customer_name", Customer_Name);
                            SqlCmd.Parameters.AddWithValue("@address", Address);

                            try
                            {
                                sqlCon.Open();
                                SqlResult = SqlCmd.ExecuteNonQuery();
                            }
                            catch (SqlException e)
                            {
                                if (e.Number == 2627)
                                    MessageBox.Show("A site with this name already exists.  Please select a unique site name and try again.", "Site Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                    MessageBox.Show(e.Message, "Failed To Save Site", MessageBoxButton.OK, MessageBoxImage.Error);
                                SqlResult = 0;
                            }

                        }
                        sqlCon.Close();
                        break;

                    case 1:
                        query.Append("UPDATE ");
                        query.Append(tblSiteInfo);
                        query.Append(" ");
                        query.Append(tblSiteUpdate);
                        query.Append(Site_ID.ToString());
                        SaveCmd = query.ToString(); 
                        using (SqlCommand SqlCmd = new SqlCommand(SaveCmd))
                        {
                            SqlCmd.Connection = sqlCon;
                            SqlCmd.Parameters.AddWithValue("@short_name", Short_Name);
                            SqlCmd.Parameters.AddWithValue("@full_name", Full_Name);
                            SqlCmd.Parameters.AddWithValue("@customer_name", Customer_Name);
                            SqlCmd.Parameters.AddWithValue("@address", Address);

                            try
                            {
                                sqlCon.Open();
                                SqlResult = SqlCmd.ExecuteNonQuery();
                            }
                            catch (SqlException e)
                            {
                                if (e.Number == 2627)
                                    MessageBox.Show("A site with this name already exists.  Please select a unique site name and try again.", "Site Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                    MessageBox.Show(e.Message, "Failed To Save Site", MessageBoxButton.OK, MessageBoxImage.Error);
                                SqlResult = 0;
                            }

                        }
                        sqlCon.Close();
                        break;
                }

            }
            return SqlResult;
        }

        public bool Check_Site(string Site_Name)
        {
            string connString = SQLConnString;

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblSiteInfo);
            query.Append(" WHERE Short_Name = '");
            query.Append(Site_Name);
            query.Append("'");

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                if (reader.HasRows == true)
                    return true;
                else
                    return false;
            }
        }
        public bool Load_Site(string Site_Name)
        {
            string connString = SQLConnString;

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblSiteInfo);
            query.Append(" WHERE Short_Name = '");
            query.Append(Site_Name);
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
                        Site_ID = ((long)reader[0]);
                        Short_Name = ((string)reader[1]);
                        Full_Name = ((string)reader[2]);
                        Customer_Name = ((string)reader[3]);
                        Address = ((string)reader[4]);
                    }
                    return true;
                }
                else
                    return false;
            }
        }
    }

}
