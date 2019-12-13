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
            Get_User(CUser);
        }

        private void Toggle_Mgt(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Logout(object sender, RoutedEventArgs e)
        {
            if ((string)mmenu_login.Header == "Login")
            {
                if(Get_User(CUser) > 0)  
                    mmenu_login.Header = "Logout";
            }
            else
            {
                CUser.User_ID = 0;
                mmenu_login.Header = "Login";
            }
        }

        public int Get_User(User_Info CUser2)
        {
            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Site_Management;Integrated Security=True";
            string UserName = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

            int duser = 0;  // See if it's a domain user

            duser = UserName.IndexOf("\\");

            if (duser > 0)
            {
                UserName = UserName.Substring(duser + 1);
            }

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
            return 1;
        }
    }

}
