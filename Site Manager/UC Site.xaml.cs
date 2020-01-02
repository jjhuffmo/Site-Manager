using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Site.xaml
    /// </summary>
    public partial class UC_Site : UserControl
    {

        public Sites csite;
        DispatcherTimer _typingTimer;
        private int Mode = 0;
        User_Info cuser = new User_Info();

        //
        //  Function:   public UC_Site(Sites current_site, int mode)
        //
        //  Arguments:  Sites current_site = Site to display/edit/enter information
        //              int mode = Mode of operation
        //              (0 = Create New Site, 1 = Edit Existing Site, 2 = View Site Info
        //
        //  Purpose:    Display the Login dialog and try logging in if a name was entered and Login button pressed
        //
        public UC_Site(Sites current_site, int mode, User_Info current_user)
        {
            InitializeComponent();

            cuser = current_user;

            csite = new Sites();

            csite.PropertyChanged += new PropertyChangedEventHandler(current_site_PropertyChanged);
            
            csite = current_site;

            // Prepare fields based on mode
            switch (mode)
            {
                // Create New
                case 0:
                    Make_User_List(0);
                    btn_Save.Content = "Save";
                    break;

                // Edit
                case 1:
                    btn_Save.Content = "Update";
                    Load_Values();
                    Make_User_List(1);

                    break;

                // View Only
                case 2:
                    Site_Grid.IsEnabled = false;
                    btn_Save.Content = "OK";
                    Load_Values();
                    Make_User_List(1);
                    break;
            }
        }

        private void Change_Mode(int Mode)
        {
            // Prepare fields based on mode
            switch (Mode)
            {
                // Create New
                case 0:
                    btn_Save.Content = "Save";
                    break;

                // Edit
                case 1:
                    btn_Save.Content = "Update";
                    break;

                // View Only
                case 2:
                    Site_Grid.IsEnabled = false;
                    btn_Save.Content = "OK";
                    break;
            }
        }
        //
        //  Function:   private void Load_Values()
        //
        //  Purpose:    Displays the currently selected site information on live values
        //
        private void Load_Values()
        {
            Short_Name.Text = csite.Short_Name;
            Full_Name.Text = csite.Full_Name;
            Customer_Name.Text = csite.Customer_Name;
            Site_Address.Text = csite.Address;
        }

        //
        //  Function:   private void Short_Name_Changed(object sender, TextChangedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              TextChangedEventArgs e = arguments for the text changed function
        //
        //  Purpose:    Monitor when the short name text has changed (for checking existence)
        //              Auto check every 1 second when typing has not occurred
        //
        private void Short_Name_Changed(object sender, TextChangedEventArgs e)
        {
            if (_typingTimer == null)
            {
                _typingTimer = new System.Windows.Threading.DispatcherTimer();
                _typingTimer.Interval = TimeSpan.FromMilliseconds(1000);
                _typingTimer.Tick += new EventHandler(this.handleTypingTimerTimeout);
            }
            _typingTimer.Stop(); // Resets the timer
            _typingTimer.Tag = (sender as TextBox).Text; // This should be done with EventArgs
            _typingTimer.Start();
        }

        //
        //  Function:   private void handleTypingTimerTimeout(object sender, EventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              EventArgs e = arguments for the function
        //
        //  Purpose:    When the short_name text box is being modified and no typing has occurred for 1 second
        //              Check the database to see if the short_name (site_name) is already in use
        //
        private void handleTypingTimerTimeout(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer; 

            if (timer == null)
            {
                return;
            }

            if (csite.Check_Site(Short_Name.Text) == true && Mode != 2 && (csite.Short_Name.CompareTo(Short_Name.Text) != 0))
            {
                Site_Exists.Visibility = Visibility.Visible;
                btn_Save.Visibility = Visibility.Hidden;
            }
            else
            {
                Site_Exists.Visibility = Visibility.Hidden;
                btn_Save.Visibility = Visibility.Visible;
            }
            // The timer must be stopped! We want to act only once per keystroke.
            timer.Stop();
        }

        //
        //  Function:   private void current_site_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              PropertyChangedEventArgs e = arguments for the property changed function
        //
        //  Purpose:    Monitor each of the fields and update the screen fields with the internal tags
        //
        private void current_site_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Short_Name":
                {
                    this.Short_Name.Text = csite.Short_Name;
                    break;
                }
                case "Full_Name":
                    {
                        this.Full_Name.Text = csite.Full_Name;
                        break;
                    }
                case "Customer_Name":
                    {
                        this.Customer_Name.Text = csite.Customer_Name;
                        break;
                    }
                case "Address":
                    {
                        this.Site_Address.Text = csite.Address;
                        break;
                    }
                default:
                    break;
            }
        }

        private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        {
            // Save local variables to structure
            csite.Short_Name = this.Short_Name.Text;
            csite.Full_Name = this.Full_Name.Text;
            csite.Customer_Name = this.Customer_Name.Text;
            csite.Address = this.Site_Address.Text;

            switch (btn_Save.Content)
            {
                case "OK":
                    this.Visibility = Visibility.Hidden;
                    break;
                case "Save":
                    if (MessageBox.Show("Are you sure you want to save this site?", "Confirm Save", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (csite.Save_Site(0) == 1)
                        {
                            this.Visibility = Visibility.Hidden;
                            cuser.Modified = true;
                        }
                    }
                    break;
                case "Update":
                    if (MessageBox.Show("Are you sure you want to update this site?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (csite.Save_Site(1) == 1)
                        {
                            this.Visibility = Visibility.Hidden;
                            cuser.Modified = true;
                        }
                    }
                    break;
            }
        }

        private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            csite.Short_Name = csite.Short_Name;

            this.Visibility = Visibility.Hidden;
        }

        //
        //  Function:   private void Make_User_List(int mode)
        //
        //  Arguments:  int mode = Mode of operation (0 = All Users, 1 = Just this site's users
        //
        //  Purpose:    Display the User List for a new or existing site
        //
        private void Make_User_List(int mode)
        {
            Users users = new Users();
            if (mode == 0)
                users.Get_List(0);
            else
                users.Get_List(1, csite.Site_ID);

            Users_List.Items.Clear();

            for (int i = 0; i < users.User_Name.Count; i++)
            {
                Users_List.Items.Add(users.User_Name[i]);
            }
        }
    }
}
