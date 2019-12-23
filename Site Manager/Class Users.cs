using System;
using System.Collections.Generic;
using System.Text;
using static Site_Manager.Resources;
using System.Data.SqlClient;

namespace Site_Manager
{
    class Users
    {
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

        public void Initialize()
        {
            User_Name = new List<string>();
            User_ID = new List<int>();
            Access = new List<int>();
        }
        public void Get_List()
        {

            Initialize();

            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;

            // Query the User Database for the user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
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
        }
    }
}
