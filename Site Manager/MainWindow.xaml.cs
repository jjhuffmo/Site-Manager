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
        public Settings System_Settings = new Settings();
        public Opened_Sites Opened_Sites = new Opened_Sites();

        public MainWindow()
        {
            InitializeComponent();
            Opened_Sites.Initialize();

            this.DataContext = this;

            current_user.PropertyChanged += new PropertyChangedEventHandler(CUser_PropertyChanged);
            SiteList.SelectionChanged += new SelectionChangedEventHandler(SiteList_Changed);
            System_Settings.PropertyChanged += new PropertyChangedEventHandler(System_Settings_Changed);

            LoginLogout(current_user, 1);
        }

        //
        //  Function:  private void System_Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              PropertyChangedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Check for the modified system settings and respond appropriately
        //
        private void System_Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //
        //  Function:  private void View_Sites_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Show or hide the sites list column based on window option
        //
        private void View_Sites_Clicked(object sender, RoutedEventArgs e)
        {
            System_Settings.Show_Sites = mmenu_view_sites.IsChecked;
        }

        //
        //  Function:  private void SiteList_Changed(object sender, SelectionChangedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              SelectionChangedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Change the modify and delete site menu options based on if a site is selected
        //              on the site list box.
        //
        private void SiteList_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (SiteList.SelectedItem != null)
            {
                mmenu_delete_site.Visibility = Visibility.Visible;
                mmenu_modify_site.Visibility = Visibility.Visible;
            }
            else
            {
                mmenu_delete_site.Visibility = Visibility.Collapsed;
                mmenu_modify_site.Visibility = Visibility.Collapsed;
            }
        }

        //
        //  Function:  private void System_Settings_Changed(object sender, PropertyChangedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Check for the modified system settings and respond appropriately
        //
        private void System_Settings_Changed(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Show_Sites":
                    if (System_Settings.Show_Sites)
                    {
                        Splitter.Visibility = Visibility.Visible;
                        Sites_Panel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Splitter.Visibility = Visibility.Hidden;
                        Sites_Panel.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
        }
            //
            //  Function:   private void CUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
            //
            //  Arguments:  object sender = Built in variable of calling object (menuitem)
            //              RoutedEventArgs e = Built in variable to hold sending objects arguments
            //
            //  Purpose:    Check for the modified user settings and respond appropriately
            //
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
                case "Modified":
                    if (current_user.Modified)
                    {
                        Update_Sites_List(Load_My_Sites(current_user));
                        SiteList.Visibility = Visibility.Visible;
                        current_user.Modified = false;
                    }
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
        //  Purpose:    Display the custom control to create a new site
        //
        public void Create_Site(object sender, RoutedEventArgs e)
        {
            var NS = new Sites();

            var newsite = new UC_Site(NS, 0, current_user);

            //this.Content_Control.Content = newsite;

            //this.Content_Control.Visibility = Visibility.Visible;
        }

        //
        //  Function:   public void Modify_Site(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Modifies and existing site
        //
        public void Modify_Site(object sender, RoutedEventArgs e)
        {
            // Get the site name (short name in DB)
            if (SiteList.SelectedItem != null)
            {
                string Site_Name = (string)SiteList.SelectedItem;

                Load_Site(Site_Name, 1);
            }
        }

        //
        //  Function:   public void Close_Site(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Close the currently selected site
        //
        public void Close_Site(object sender, RoutedEventArgs e)
        {
            for (int i=0; i < SiteList.Items.Count; i++)
            {
                ListBoxItem site_list = (ListBoxItem)SiteList.Items[i];
                TabItem tab_site = (TabItem)Sites_Tabs.SelectedItem;
                if (site_list.Content == tab_site.Header)
                    site_list.IsEnabled = true;
            }
            Sites_Tabs.Items.Remove(Sites_Tabs.SelectedItem);
        }

        //
        //  Function:   public void SiteList_dblClick(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Open the currently selected site (in SiteList) and display the information in the custom control
        //
        public void SiteList_dblClick(object sender, RoutedEventArgs e)
        {
            // Get the site name (short name in DB)
            if (SiteList.SelectedItem != null)
            {
                ListBoxItem sel_site = (ListBoxItem)SiteList.SelectedItem;
                string Site_Name = sel_site.Content.ToString();

                Load_Site(Site_Name, 2);
            }

        }

        //
        //  Function:   public void Load_Site(string Site_Name, int mode)
        //
        //  Arguments:  string Site_Name = Name of site to load
        //              int mode = Mode of load (1 = Edit, 2 = View Only)
        //
        //  Purpose:    Loads a site for viewing or modifying
        //
        public void Load_Site(string Site_Name, int mode)
        {
            if (SiteList.SelectedItem != null)
            {
                Sites View_Site = new Sites();
                // Make sure it's not already open
                for (int i = 0; i < Opened_Sites.site_info.Count; i++)
                {

                }

                // Try to the load the site.  If successful, display it otherwise don't (add not found handling later)
                if (View_Site.Load_Site(Site_Name) == true)
                {
                    var editsite = new UC_Site(View_Site, mode, current_user);
                    TabItem new_tab = new TabItem();
                    
                    TabControl site_info = new TabControl();
                    new_tab.Header = Site_Name;
                    
                    Sites_Tabs.Items.Add(new_tab);
                    TabItem new_tab2 = new TabItem();
                    new_tab2.Header = "Info";
                    new_tab2.Content = editsite;
                    site_info.Items.Add(new_tab2);
                    new_tab.Content = site_info;
                    ListBoxItem sel_site = (ListBoxItem)SiteList.ItemContainerGenerator.ContainerFromIndex(SiteList.SelectedIndex);
                    sel_site.IsEnabled = false;
                    Sites_Tabs.SelectedIndex = Sites_Tabs.Items.Count;
                }
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
            if (SiteList.SelectedItem == null)
            {
                mmenu_delete_site.Visibility = Visibility.Collapsed;
                mmenu_modify_site.Visibility = Visibility.Collapsed;
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
            sites.Initialize();

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
                ListBoxItem site = new ListBoxItem();
                site.Uid = sites.site_id.ToString();
                site.Content = sname;
                SiteList.Items.Add(site);
            }
        }
    }
}

