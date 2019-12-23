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

        public long Site_ID = new long();
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

        public int Save_Site()
        {
            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;
            string InsertCmd = "";
            int SqlResult = 0;

            // Write the current site information to the database
            StringBuilder query = new StringBuilder("INSERT INTO ");
            query.Append(tblSiteInfo);
            query.Append(" ");
            query.Append(tblSiteFields);
            query.Append(" ");
            query.Append(tblSiteInsert);
            InsertCmd = query.ToString();

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                using (SqlCommand SqlCmd = new SqlCommand(InsertCmd))
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
                    catch(SqlException e)
                    {
                        if (e.Number == 2627)
                            MessageBox.Show("A site with this name already exists.  Please select a unique site name and try again.", "Site Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            MessageBox.Show(e.Message, "Failed To Save Site", MessageBoxButton.OK, MessageBoxImage.Error);
                        SqlResult = 0;
                    }

                }
                sqlCon.Close();
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
    }

}
