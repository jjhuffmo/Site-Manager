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
                {
                    btn_Save.Visibility = Visibility.Visible;
                    btn_Cancel.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_Save.Visibility = Visibility.Hidden;
                    btn_Cancel.Visibility = Visibility.Hidden;
                }
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
            btn_Cancel.Visibility = Visibility.Hidden;
            btn_Remove_User.Visibility = Visibility.Hidden;

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
            View_Users.Clear();
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
            Changes_Made = false;
        }

        private void User_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Changed")
                Changes_Made = true;

        }

        //
        //  Function:   private void btn_Add_User_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Calls the Add_User() function
        //
        private void btn_Add_User_Clicked(object sender, RoutedEventArgs e)
        {
            Add_User();
        }

        //
        //  Function:   private void btn_Remove_User_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Call Remove_User() function
        //
        private void btn_Remove_User_Clicked(object sender, RoutedEventArgs e)
        {
            Remove_User();
        }

        //
        //  Function:   private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Save current changes to the site users after confirmation from a messagebox
        //
        private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to save your changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DB_Users changed_users = new DB_Users();
                changed_users.Initialize();

                for (int i = 0; i < View_Users.Count; i++)
                {
                    if (View_Users[i].Changed == true)
                    {
                        changed_users.Site_User_ID.Add(View_Users[i].Site_User_ID);
                        changed_users.Site_ID.Add(csite.Site_ID);
                        changed_users.User_ID.Add(View_Users[i].User_ID);
                        changed_users.View_Resources.Add(View_Users[i].View_Resources);
                        changed_users.Add_Resources.Add(View_Users[i].Add_Resources);
                        changed_users.Modify_Resources.Add(View_Users[i].Modify_Resources);
                        changed_users.Del_Resources.Add(View_Users[i].Del_Resources);
                        changed_users.View_Tickets.Add(View_Users[i].View_Tickets);
                        changed_users.Add_Tickets.Add(View_Users[i].Add_Tickets);
                    }
                }
                if (changed_users.Save_List(1, csite.Site_ID) == true)
                {
                    MessageBox.Show("Changes Successfully Saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Init_Complete = false;
                    Make_User_List(Mode);
                    Init_Complete = true;
                }
                else
                {
                    MessageBox.Show("Changes Could Not Be Saved", "Failed To Save", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //
        //  Function:   private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function (Short_Name textbox)
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Cancel current changes to the site users after confirmation from a messagebox
        //
        private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel your changes?", "Cancel Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Init_Complete = false;
                Make_User_List(Mode);
                Init_Complete = true;
            }
        }

        private void Site_Users_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Site_Users_List.SelectedIndex > -1)
            {
                btn_Remove_User.Visibility = Visibility.Visible;
                User_CM_Delete.Visibility = Visibility.Visible;
            }
            else
            {
                btn_Remove_User.Visibility = Visibility.Hidden;
                User_CM_Delete.Visibility = Visibility.Collapsed;
            }
        }

        private void User_CM_Add_Click(object sender, RoutedEventArgs e)
        {
            Add_User();
        }

        private void User_CM_Delete_Click(object sender, RoutedEventArgs e)
        {
            Remove_User();
        }

        //
        //  Function:   private void Add_User()
        //
        //  Purpose:    Add a new site user after confirmation from a messagebox
        //
        private void Add_User()
        {
            Users new_user = new Users();
            List<Users> exist_users = new List<Users>();

            for (int i = 0; i < View_Users.Count; i++)
            {
                exist_users.Add(View_Users[i]);
            }

            var addusers = new UC_User_Selection(exist_users);

            var Add_User_Page = new Popup_Info();

            Add_User_Page.Title = "Add Users To Site";
            Add_User_Page.Content = addusers;
            Add_User_Page.ShowDialog();

            // New site was saved, so update the tabs
            if (Add_User_Page.DialogResult == true)
            {
                List<Users> chosen_users = new List<Users>();

                chosen_users = addusers.chosen_users;

                for (int q = 0; q < chosen_users.Count; q++)
                {
                    View_Users.Add(chosen_users[q]);
                }

                Changes_Made = true;
            }
        }

        //
        //  Function:   private void Remove_User()
        //
        //  Purpose:    Remove the highlighted site user after confirmation from a messagebox
        //
        private void Remove_User()
        {
            if (Site_Users_List.SelectedIndex > -1)
            {
                //Find all the selected users and mark them for deletion
                for (int i = 0; i < Site_Users_List.SelectedItems.Count; i++)
                {
                    Users selected_user = new Users();
                    selected_user = (Users)Site_Users_List.SelectedItems[i];
                    //MessageBox.Show("Selected User: " + selected_user.User_ID.ToString(), "Selected User", MessageBoxButton.OK, MessageBoxImage.Information);
                    for (int j = 0; j < View_Users.Count; j++)
                    {
                        if (View_Users[j].User_ID == selected_user.User_ID)
                            View_Users.RemoveAt(j);
                    }
                }
                Changes_Made = true;
            }
        }
    }
}
