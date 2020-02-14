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
        public Opened_Sites Open_Sites = new Opened_Sites();

        public MainWindow()
        {
            InitializeComponent();
            Open_Sites.Initialize();

            this.DataContext = this;

            current_user.PropertyChanged += new PropertyChangedEventHandler(CUser_PropertyChanged);
            SiteList.SelectionChanged += new SelectionChangedEventHandler(SiteList_Changed);
            System_Settings.PropertyChanged += new PropertyChangedEventHandler(System_Settings_Changed);
            Sites_Tabs.SelectionChanged += new SelectionChangedEventHandler(Sites_Tabs_Changed);

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
        //  Function:  private void View_Sites_click(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Show or hide the sites list column based on window option
        //
        private void View_Sites_click(object sender, RoutedEventArgs e)
        {
            System_Settings.Show_Sites = mmenu_view_sites.IsChecked;
        }

        //
        //  Function:   private void ShowAll_Sites_click(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Toggle between showing all sites or only ones assigned to you (management only)
        //
        private void ShowAll_Sites_click(object sender, RoutedEventArgs e)
        {
            System_Settings.Show_All_Sites = mmenu_showall_sites.IsChecked;
        }

        //
        //  Function:   public void Menu_Exit_click(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Exit the app through menu instead of X
        //
        public void Menu_Exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //
        //  Function:   private void Login_Logout_click(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public void Login_Logout_click(object sender, RoutedEventArgs e)
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
        public void Create_Site_click(object sender, RoutedEventArgs e)
        {
            // Create the "New Site" listboxitem to pass to load site
            ListBoxItem new_site = new ListBoxItem();
            new_site.Content = "New Site";

            // Create a new tab to show the site info. 
            Load_Site(new_site, 0);
        }

        //
        //  Function:   public void Modify_Site(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = Built in variable of calling object (menuitem)
        //              RoutedEventArgs e = Built in variable to hold sending objects arguments
        //
        //  Purpose:    Modifies and existing site
        //
        public void Modify_Site_click(object sender, RoutedEventArgs e)
        {
            // Get the site name (short name in DB)
            if (SiteList.SelectedItem != null)
            {
                Load_Site((ListBoxItem)SiteList.SelectedItem, 1);
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
        public void Close_Site_click(object sender, RoutedEventArgs e)
        {
            if (Sites_Tabs.SelectedIndex > -1)
                Close_Site(Sites_Tabs.SelectedIndex);
        }

        private void Delete_Site_click(object sender, RoutedEventArgs e)
        {

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
                    mmenu_view_sites.IsChecked = System_Settings.Show_Sites;
                    break;

                case "Show_All_Sites":
                    Load_My_Sites(current_user);
                    mmenu_showall_sites.IsChecked = System_Settings.Show_All_Sites;
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
                        Load_My_Sites(current_user);
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
        //  Function:   public void Close_Site(int site_no)
        //
        //  Arguments:  int site_no = Number of the site (in open_sites) to close
        //
        //  Purpose:    Close the currently selected site
        //
        public void Close_Site(int site_no)
        {
            // Find the site in the site_list box and re-enable it
            for (int i = 0; i < SiteList.Items.Count; i++)
            {
                ListBoxItem site_list = (ListBoxItem)SiteList.Items[i];
                TabItem tab_site = (TabItem)Sites_Tabs.Items[site_no];
                if ((string)site_list.Content == (string)tab_site.Header)
                    site_list.IsEnabled = true;
            }
            // Remove the site from the Open_Sites variable
            Open_Sites.Close_Site(Sites_Tabs.SelectedIndex);

            // Find the sites tab for this site and remove it
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
                Load_Site(sel_site, 1);
            }

        }

        //
        //  Function:   public void Load_Site(string Site_Name, int mode)
        //
        //  Arguments:  string Site_Name = Name of site to load
        //              int mode = Mode of load (0 = Create, 1 = Edit/View)
        //
        //  Purpose:    Loads a site for viewing or modifying
        //
        public void Load_Site(ListBoxItem Site_Name, int mode)
        {
            if (mode == 0)
            {
                var NS = new Sites();
                var newsite = new UC_Site(NS, 0, current_user);

                var New_Site_Page = new Popup_Info();

                Site_Name.HorizontalContentAlignment = HorizontalAlignment.Left;
                Site_Name.VerticalContentAlignment = VerticalAlignment.Top;
                New_Site_Page.Content = newsite;
                New_Site_Page.Title = "Create New Site";
                Site_Name.DataContext = New_Site_Page;
                New_Site_Page.ShowDialog();

                // New site was saved, so update the tabs
                if (New_Site_Page.DialogResult == true)
                {
                    Load_My_Sites(current_user);
                }
            }

            if (SiteList.SelectedItem != null && mode != 0)
            {
                bool found = false;
                int found_tab = 0;

                Sites View_Site = new Sites();

                // See if it's already open
                for (int i = 0; i < Open_Sites.site_info.Count; i++)
                {
                    if (Int32.Parse(Site_Name.Uid) == Open_Sites.site_info[i].Site_ID)
                    {
                        found = true;
                        found_tab = Open_Sites.tab_index[i];
                    }
                }

                // Try to the load the site.  If successful, display it otherwise don't (add not found handling later)
                if (View_Site.Load_Site((string)Site_Name.Content) == true && found == false)
                {
                    // Generate a new Site_Info tab using the user control UC Site
                    var editsite = new UC_Site(View_Site, mode, current_user);

                    // Create a new tab for the Site Tab itself
                    TabItem sites_tab = new TabItem();
                    sites_tab.Header = View_Site.Short_Name;
                    Sites_Tabs.Items.Add(sites_tab);

                    // Create the site tabs for info, tickets, resources, etc
                    TabControl site_tab = new TabControl();
                    site_tab.SizeChanged += new SizeChangedEventHandler(Sites_Tabs_SizeChanged);
                    TabItem info_tab = new TabItem();
                    info_tab.Header = "Info";
                    info_tab.Content = editsite;
                    site_tab.Items.Add(info_tab);

                    // Create the users tab
                    TabItem users_tab = new TabItem();
                    users_tab.Header = "Users";
                    // Generate a new Site Users tab using the user control UC Site Tickets
                    var siteusers = new UC_Site_Users(View_Site, mode, current_user);
                    users_tab.Content = siteusers;
                    site_tab.Items.Add(users_tab);

                    // Create the tickets tab
                    TabItem tickets_tab = new TabItem();
                    tickets_tab.Header = "Tickets";
                    // Generate a new Site_Tickets tab using the user control UC Site Tickets
                    var sitetickets = new UC_Site_Tickets(); tickets_tab.Content = sitetickets;
                    site_tab.Items.Add(tickets_tab);

                    // Create the resources tab
                    TabItem resources_tab = new TabItem();
                    resources_tab.Header = "Resources";
                    //resources_tab.Content = sitetickets;
                    site_tab.Items.Add(resources_tab);
                    sites_tab.Content = site_tab;

                    // Add the site to the Open_Sites variable
                    Open_Sites.Insert_New(View_Site);

                    // Block out the selected site so they don't try to click on it again
                    ListBoxItem sel_site = (ListBoxItem)SiteList.ItemContainerGenerator.ContainerFromIndex(SiteList.SelectedIndex);
                    sel_site.IsEnabled = false;

                    // Switch focus to the first tab
                    Sites_Tabs.SelectedIndex = Sites_Tabs.Items.Count - 1;
                }

                // If it was found, but we want to edit it then just enable the modifying of the proper tab
                if (found == true && mode == 1)
                {
                    // Generate a new Site_Info tab using the user control UC Site
                    var editsite = new UC_Site(View_Site, mode, current_user);

                    TabItem site_mod = (TabItem)Sites_Tabs.Items[found_tab];
                    site_mod.Content = editsite;
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
        //              mode = Mode of list to retrieve (0 = Just Mine 'Default', 1 = All For Supervisors Only)
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

            // Read the user's sites unless they're management
            if (user.Access != 9999 || System_Settings.Show_All_Sites == false)
            {
                query.Append(" WHERE User_ID = ");
                query.Append(user.User_ID.ToString());
            }

            // Read all the sites associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    if (sites.site_id.Contains((long)reader[1]) == false)
                    {
                        sites.site_id.Add((long)reader[1]);
                        sites.View_Resources.Add((bool)reader[3]);
                        sites.Add_Resources.Add((bool)reader[4]);
                        sites.Modify_Resources.Add((bool)reader[5]);
                        sites.Del_Resources.Add((bool)reader[6]);
                        sites.View_Tickets.Add((bool)reader[7]);
                        sites.Add_Tickets.Add((bool)reader[8]);
                    }
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
            Update_Sites_List(sites);
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
            int i = 0;
            SiteList.Items.Clear();
            foreach (string sname in sites.site_name)
            {
                ListBoxItem site = new ListBoxItem();
                site.Uid = sites.site_id[i].ToString();
                site.Content = sname;
                // Block out any open site so they don't try to click on it again
                if (Open_Sites.site_info.Any(x => x.Site_ID == sites.site_id[i]))
                {
                    site.IsEnabled = false;
                }
                SiteList.Items.Add(site);
                // Increment index
                i++;
            }
        }

        private void Sites_Tabs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            // Get the full length of the tab area and the # of tabs
            double width = tabControl.ActualWidth;
            int tabs = tabControl.Items.Count;

            // Divide the width by the # of tabs to get the width of each tab
            double eachtab = width / tabs - 20;

            for (int i = 0; i < tabs; i++)
            {
                TabItem tabItem = (TabItem)tabControl.Items[i];
                tabItem.Width = eachtab;
            }
        }

        private void Sites_Tabs_Changed(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            // Get the full length of the tab area and the # of tabs
            double width = tabControl.ActualWidth;
            int tabs = tabControl.Items.Count;

            // Divide the width by the # of tabs to get the width of each tab
            double eachtab = width / tabs - 20;

            for (int i = 0; i < tabs; i++)
            {
                TabItem tabItem = (TabItem)tabControl.Items[i];
                tabItem.Width = eachtab;
            }
        }

        private void Create_User_click(object sender, RoutedEventArgs e)
        {
            Create_User();
        }
        private void Modify_User_click(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_User_click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_User()
        {

        }
    }
}

