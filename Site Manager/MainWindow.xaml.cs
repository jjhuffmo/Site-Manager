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
        public User_Info current_user = new User_Info();
        public User_Sites user_sites = new User_Sites();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            current_user.PropertyChanged += new PropertyChangedEventHandler(CUser_PropertyChanged);

            LoginLogout(current_user, 1);
        }

        private void CUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case "User_ID":
                    if (current_user.User_ID > 0)
                    {
                        mmenu_login.Header = "Logout";
                        this.Title = AppTitle + " - " + current_user.User_Name;
                        user_sites = Load_My_Sites(current_user);
                        Update_Sites_List(user_sites);
                        SiteList.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        mmenu_login.Header = "Login...";
                        this.Title = AppTitle + " - Not Logged In";
                        SiteList.Visibility = Visibility.Hidden;
                    }
                    Update_Main_Menu(current_user.Access);
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
            if (current_user.User_ID == 0)
            {
                var Login = new dlgLogin();
                if (Login.ShowDialog() == true)
                {
                    LoginLogout(current_user, 2, Login.Entered_User);
                }
            }
            else
            {
                LoginLogout(current_user, 0);
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
        public void Create_Site(object sender, RoutedEventArgs e)
        {
            var NS = new Sites();

            NS.Short_Name = "Test Name";

            var newsite = new UC_Site(NS);

            this.Content_Control.Content = newsite;
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

        //
        //  Function:   public void Load_My_Sites(User_Info user)
        //
        //  Arguments:  user = User Info of currently logged in user
        //
        //  Return:     User_Sites = Structure holding all the sites
        //              accessible to the current user.
        //
        //  Purpose:    Loads the sites accessible to the current user
        //              into a structure for parsing/using.
        //
        public User_Sites Load_My_Sites(User_Info user)
        {
            string connString = SQLConnString;

            var sites = new User_Sites();

            // Query the Site Users Database for the sites for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblSiteUsers);
            query.Append(" WHERE User_ID = ");
            query.Append(user.User_ID.ToString());

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    sites.site_id.Add((long)reader[1]);
                    sites.site_access.Add((int)reader[3]);
                }
            }

            // Query the Site Info Database for each of the sites names
            query = new StringBuilder("SELECT Short_Name FROM ");
            query.Append(tblSiteInfo);
            query.Append(" WHERE Site_ID = ");
            foreach(int sid in sites.site_id)
            {
                query.Append(sid.ToString());
                query.Append(" OR Site_ID = ");
            }
            query.Remove(query.Length - 14, 14);

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    sites.site_name.Add((string)reader[0]);
                }
                sqlCon.Close();
            }
            return sites;
        }

        //
        //  Function:   public void Update_Sites_List(User_Sites sites)
        //
        //  Arguments:  sites = User_Sites array of accessible sites
        //
        //  Purpose:    Displays all of the Sites associated with this user.
        //
        public void Update_Sites_List(User_Sites sites)
        {
            SiteList.Items.Clear();
            foreach (string sname in sites.site_name)
            {
                SiteList.Items.Add(sname);
            }
        }
    }
}

