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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Site_Users.xaml
    /// </summary>
    public partial class UC_Site_Users : UserControl
    {
        public ObservableCollection<Users> View_Users { get; set; }
        public User_Info cuser = new User_Info();
        public Sites csite;
        public int Mode = 0;
        public bool _Changes_Made = false;
        public bool Init_Complete = false;
        public bool Changes_Made
        {
            get
            { return _Changes_Made; }
            set
            {
                _Changes_Made = value;
                if (value == true)
                    btn_Save.Visibility = Visibility.Visible;
                else
                    btn_Save.Visibility = Visibility.Hidden;
            }
        }

        //
        //  Function:   public UC_Site_Users(Sites current_site, int mode, User_Info current_user)
        //
        //  Arguments:  Sites current_site = Site to display/edit/enter information
        //              int mode = Mode of operation
        //              (0 = Edit Users Enabled, 1 = View Users Only)
        //              User_Info current_user = Currently logged in user to valid and store
        //
        //  Purpose:    Display the user information 
        //
        public UC_Site_Users(Sites current_site, int mode, User_Info current_user)
        {
            View_Users = new ObservableCollection<Users>();
            cuser = current_user;

            csite = new Sites();
            csite = current_site;

            Mode = mode;

            this.DataContext = this;

            InitializeComponent();

            Make_User_List(Mode);

            btn_Save.Visibility = Visibility.Hidden;

            Init_Complete = true;
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
            DB_Users users = new DB_Users();

            if (mode == 0)
                users.Get_List(0);
            else
                users.Get_List(1, csite.Site_ID);
            for (int i = 0; i < users.User_Name.Count; i++)
            {
                Users user = new Users();
                user.PropertyChanged += User_PropertyChanged;
                user.Convert_DB_Users(users, i);
                View_Users.Add(user);
                View_Users[i].Changed = false;
            }
        }

        private void User_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (Init_Complete)
            {
                Site_Users_List.Items.Refresh();
            }
        }

        //
        //  Function:   private void btn_Remove_User_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Cancel current changes to the site after confirmation from a messagebox
        //
        private void btn_Remove_User_Clicked(object sender, RoutedEventArgs e)
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
                    //Load_Values();
                }
            }
        }

        //
        //  Function:   private void btn_Add_User_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Cancel current changes to the site after confirmation from a messagebox
        //
        private void btn_Add_User_Clicked(object sender, RoutedEventArgs e)
        {
            /*if (Mode == 0)
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
                    //Load_Values();
                }
            }*/
        }

        private void Site_Users_List_CurrentCellChanged(object sender, EventArgs e)
        {
            //var Current_Row = Site_Users_List.Items.IndexOf(Site_Users_List.CurrentItem);

            Changes_Made = true;
            //Site_Users_List.Items.Refresh();
        }

        private void Site_Users_List_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Site_Users_List_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Site_Users_List.Items.Refresh();
        }
    }
}
