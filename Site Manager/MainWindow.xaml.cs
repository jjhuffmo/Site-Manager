using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public User_Info CUser;
        public MainWindow()
        {
            CUser = new User_Info();

            InitializeComponent();
            CUser = LoginLogout(1);

        }

        private void Toggle_Mgt(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Logout(object sender, RoutedEventArgs e)
        {
            CUser.User_ID = 1;
            //if (window.ShowDialog() == DialogResult.Value) ;

            /*if ((string)mmenu_login.Header == "Login")
            {
                //if(LoginLogout(1) > 0)  
                //    mmenu_login.Header = "Logout";
            }
            else
            {
                CUser.User_ID = 0;
                mmenu_login.Header = "Login";
            }*/
        }

        //
        //  Function: public int LoginLogout(int Action, string UserName)
        //
        //  Arguments:  int pvAction = Type of action (0 = Logout, 1 = Login with system user, 2 = Login with Name)
        //              string pvUserName = User name if not using AD
        //
        //  Return Value: User_Info filled in with user data if successful login or blank if unsuccessful
        //
        //  Purpose: Logs a user in or out of the system.  Without being logged in, no actions can be performed

        public User_Info LoginLogout(int pvAction, string pvUserName = "")
        {
            User_Info CUser2 = new User_Info();
            string UserName = pvUserName;

            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Site_Management;Integrated Security=True";

            // If logging out, then just wipe out the current user and return the blank one
            if (pvAction == 0)
            {
                return CUser;
            }

            // If using system user, then get it and process accordingly
            if (pvAction == 1)
            {
                UserName = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

                int duser = 0;  // See if it's a domain user

                duser = UserName.IndexOf("\\");

                if (duser > 0)
                {
                    UserName = UserName.Substring(duser + 1);
                }
            }

            // If using an entered user, we don't need to do anything different so just carry on

            string query = "SELECT User_ID, User_Name, Access_Level FROM dbo.User_Info WHERE User_Name ='" + UserName + "'";

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query, sqlCon);
                using (SqlDataReader reader = SqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CUser2.User_ID = (int)reader[0];
                        CUser2.User_Name = String.Format("{0}", reader[1]);
                        CUser2.Access = (int)reader[2];
                    }
                }
            }
            return CUser2;
        }

        public void Update_LoginStatus(int pvUserID)
        {
            // Update the login/logout
            if (pvUserID == 0)
            {
                mmenu_login.Header = "Login...";
            }
            else
            {
                mmenu_login.Header = "Logout";
            }
        }
    }
}
