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
using System.ComponentModel;
using static Site_Manager.Resources;


namespace Site_Manager
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User_Info CUser = new User_Info();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            CUser.PropertyChanged += new PropertyChangedEventHandler(CUser_PropertyChanged);

            LoginLogout(CUser,1);
        }

        private void CUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case "User_ID":
                    if (CUser.User_ID > 0)
                    {
                        mmenu_login.Header = "Logout";
                        this.Title = AppTitle + " - " + CUser.User_Name;
                    }
                    else
                    {
                        mmenu_login.Header = "Login...";
                        this.Title = AppTitle + " - Not Logged In";
                    }
                    Update_Main_Menu(CUser.Access);
                    break;
                case "Access":
                    break;
                default:
                    break;
            }
        }

        //
        //  Function:   private void Login_Logout(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public void Menu_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //
        //  Function:   private void Login_Logout(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public void Login_Logout(object sender, RoutedEventArgs e)
        {
            if (CUser.User_ID == 0)
            {
                var Login = new dlgLogin();
                if (Login.ShowDialog() == true)
                {
                    LoginLogout(CUser, 2, Login.Entered_User);
                }
            }
            else
            {
                LoginLogout(CUser, 0);
            }
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

        public int LoginLogout(User_Info CUser, int pvAction, string pvUserName = "")
        {
            string UserName = pvUserName;

            // If the user name is blank then get it from the system and try to log in with AD security
            string connString = SQLConnString;

            // If logging out, then just wipe out the current user and return the blank one
            if (pvAction == 0)
            {
                CUser.User_Name = "";
                CUser.Access = 0;
                CUser.User_ID = 0;
                return 1;
            }

            // If using system user, then get it and process accordingly
            if (pvAction == 1)
            {
                UserName = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;

                int duser;  // See if it's a domain user

                duser = UserName.IndexOf("\\");

                if (duser > 0)
                {
                    UserName = UserName.Substring(duser + 1);
                }
            }

            // If using an entered user, we don't need to do anything different so just carry on

            // Query the User Database for the user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblUserInfo);
            query.Append(" WHERE User_Name ='");
            query.Append(UserName);
            query.Append("'");

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    CUser.User_Name = String.Format("{0}", reader[1]);
                    CUser.Access = (int)reader[2];
                    CUser.User_ID = (int)reader[0];
                }
                sqlCon.Close();
            }
            return 1;
        }

        //
        //  Function:   public int Update_Main_Menu(int Access)
        //
        //  Arguments:  int Access = Access Level of current user
        //
        //  Return Value:   int = Status of update (0 = Failed, 1 = Success)
        //
        //  Purpose:    Updates
        public int Update_Main_Menu(int Access)
        {
            MenuItem item;
            MenuItem subitem1;
            MenuItem subitem2;
            MenuItem subitem3;

            // Parse the main menu and hide anything above the user's access level
            // Access levels of menu items are defined in each menu items UID
            for (int i = 0; i < MainMenu.Items.Count; i++)
            {
                item = (MenuItem)MainMenu.Items.GetItemAt(i);
                if (item.Items.Count > 1)
                {
                    for (int j = 0; j < item.Items.Count; j++)
                    {
                        subitem1 = (MenuItem)item.Items.GetItemAt(j);
                        if (subitem1.Items.Count > 1)
                        {
                            for (int k = 0; k < item.Items.Count; k++)
                            {
                                subitem2 = (MenuItem)subitem1.Items.GetItemAt(k);
                                if (subitem2.Items.Count > 1)
                                {
                                    for (int l = 0; l < item.Items.Count; l++)
                                    {
                                        subitem3 = (MenuItem)subitem2.Items.GetItemAt(l);
                                        Hide_Show_Menu(subitem3, Access);
                                    }
                                    Hide_Show_Menu(subitem2, Access);
                                }
                                else
                                    Hide_Show_Menu(subitem2, Access);
                            }
                            Hide_Show_Menu(subitem1, Access);
                        }
                        else
                            Hide_Show_Menu(subitem1, Access);
                    }
                    Hide_Show_Menu(item, Access);
                }
                else
                    Hide_Show_Menu(item, Access);
            }
            return 1;
        }

        private void Hide_Show_Menu(MenuItem item, int Access)
        {
            if (String.IsNullOrEmpty(item.Uid) == false)
            {
                if (Convert.ToInt32(item.Uid) > Access)
                    item.Visibility = Visibility.Collapsed;
                else
                    item.Visibility = Visibility.Visible;
            }
        }

    }
}
