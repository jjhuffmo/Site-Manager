﻿using System;
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
using System.Collections.ObjectModel;

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
        public User_Info cuser = new User_Info();
        bool loaded = false;
        private bool _changed = false;
        private bool changed
        {
            get
            { return _changed; }
            set
            {
                _changed = value;
                if (value == true)
                {
                    btn_Cancel.Visibility = Visibility.Visible;
                    btn_Save.Visibility = Visibility.Visible;                }
                else
                {
                    btn_Save.Visibility = Visibility.Hidden;
                    if (Mode != 0)
                        btn_Cancel.Visibility = Visibility.Hidden;
                }
            }
        }

        //
        //  Function:   public UC_Site(Sites current_site, int mode)
        //
        //  Arguments:  Sites current_site = Site to display/edit/enter information
        //              int mode = Mode of operation
        //              (0 = Create New Site, 1 = Edit Existing Site, 2 = View Site Info
        //              User_Info current_user = Currently logged in user to valid and store
        //
        //  Purpose:    Display the site information tabs for viewing/editing based on security.
        //
        public UC_Site(Sites current_site, int mode, User_Info current_user)
        {
            InitializeComponent();

            cuser = current_user;

            csite = new Sites();
            csite = current_site;
            csite.PropertyChanged += new PropertyChangedEventHandler(current_site_PropertyChanged);
            changed = false;

            Mode = mode;

            this.DataContext = this;



            //btn_Save.Visibility = Visibility.Hidden;

            // Prepare fields based on mode
            switch (mode)
            {
                // Create New
                case 0:
                    csite.Initialize();
                    Load_Values();
                    btn_Save.Content = "Save";
                    //btn_Cancel.Visibility = Visibility.Hidden;
                    break;

                // Edit
                case 1:
                    btn_Save.Content = "Update";
                    //btn_Cancel.Visibility = Visibility.Hidden;
                    Load_Values();
                    break;

                // View Only
                case 2:
                    Site_Grid.IsEnabled = false;
                    btn_Save.Content = "OK";
                    //btn_Cancel.Visibility = Visibility.Hidden;
                    Load_Values();
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
            Job_Number.Text = csite.Job_Number;
            loaded = true;
            changed = false;
        }

        //
        //  Function:   private void Field_Changed(object sender, TextChangedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              TextChangedEventArgs e = arguments for the text changed function
        //
        //  Purpose:    Monitor when any of the text blocks have changed 
        //              so we can enable the Save and Cancel buttons.
        //
        private void Field_Changed(object sender, TextChangedEventArgs e)
        {
            if (loaded == true)
            {
                if ((csite.Full_Name != this.Full_Name.Text) || (csite.Customer_Name != this.Customer_Name.Text) ||
                    (csite.Address != this.Site_Address.Text) || (csite.Job_Number != this.Job_Number.Text) ||  
                    (csite.Short_Name != (string)Short_Name.Text && Site_Exists.Visibility == Visibility.Hidden))
                {
                    changed = true;
                }
                else
                {
                    changed = false;
                }
            }

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
            if (loaded)
                _typingTimer.Start();
        }

        //
        //  Function:   private void handleTypingTimerTimeout(object sender, EventArgs e)
        //
        //  Arguments:  object sender = object that called function (Users_List listbox)
        //              SelectionChangedEventArgs e = arguments for the function
        //
        //  Purpose:    Enable/disable the remove users button when a selection is active in the box
        //
        private void User_List_Selection(object sender, SelectionChangedEventArgs e)
        {


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

            if (csite.Check_Site(Short_Name.Text) == true && Mode != 2 && csite.Short_Name != (string)Short_Name.Text)
                Site_Exists.Visibility = Visibility.Visible;
            else
                Site_Exists.Visibility = Visibility.Hidden;

            if (csite.Short_Name != (string)Short_Name.Text && Site_Exists.Visibility == Visibility.Hidden)
                changed = true;
            else
                changed = false;

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
                case "Job_Number":
                    {
                        this.Job_Number.Text = csite.Job_Number;
                        break;
                    }
                default:
                    break;
            }
        }

        //
        //  Function:   private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Save/Update the site info when clicked (after confirmation from a messagebox)
        //
        private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        {
            // Save local variables to structure
            csite.Short_Name = this.Short_Name.Text;
            csite.Full_Name = this.Full_Name.Text;
            csite.Customer_Name = this.Customer_Name.Text;
            csite.Address = this.Site_Address.Text;
            csite.Job_Number = this.Job_Number.Text;

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
                            cuser.Modified = true;
                            Window Parent = (Window)this.Parent;
                            Parent.DialogResult = true;
                        }
                    }
                    break;
                case "Update":
                    if (MessageBox.Show("Are you sure you want to update this site?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (csite.Save_Site(1) == 1)
                        {
                            cuser.Modified = true;
                            changed = false;
                        }
                    }
                    break;
            }
        }

        //
        //  Function:   private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Cancel current changes to the site after confirmation from a messagebox
        //
        private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            if (Mode == 0)
            {
                if (MessageBox.Show("Do you want to exit without saving the new site?", "Exit Without Saving", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Window Parent = (Window)this.Parent;
                    Parent.DialogResult = false;
                }
            }
            else
            {
                if (MessageBox.Show("Do you want to discard changes to site?", "Discard Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Load_Values();
                }
            }
        }
    }
}
