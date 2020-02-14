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
    /// Interaction logic for UC_User_Selection.xaml
    /// </summary>
    public partial class UC_User_Selection : UserControl
    {

        public ObservableCollection<DB_Users> avail_users { get; set; }
        public List<Users> chosen_users;

        public UC_User_Selection(List<Users> exclude_users)
        {
            chosen_users = new List<Users>();

            InitializeComponent();
            Generate_User_List(exclude_users);

            btn_Add_User.Visibility = Visibility.Hidden;
            User_List.DataContext = avail_users;
        }

        private void Generate_User_List(List<Users> exclude_users)
        {
            DB_Users all_users = new DB_Users();

            all_users.Get_List(0);

            for (int i = 0; i < exclude_users.Count; i++)
            {
                int x = all_users.User_ID.IndexOf(exclude_users[i].User_ID);
                if (x != -1)
                {
                    all_users.Remove_User(0, x);
                }
            }

            avail_users = new ObservableCollection<DB_Users>();


            avail_users.Add(all_users);
        }
        private void User_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (User_List.SelectedItems.Count < 1)
            {
                btn_Add_User.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_Add_User.Visibility = Visibility.Visible;
                if (User_List.SelectedItems.Count > 1)
                    btn_Add_User.Content = "Add Users";
                else
                {
                    btn_Add_User.Content = "Add User";
                }
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Window Parent = (Window)this.Parent;
            Parent.DialogResult = false;
        }

        private void btn_Add_User_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Add selected users to this site?", "Add Users", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in User_List.SelectedItems)
                {
                    Users hold_user = new Users();
                    int i = User_List.Items.IndexOf(item);

                    hold_user.User_ID = avail_users[0].User_ID[i];
                    hold_user.User_Name = avail_users[0].User_Name[i];

                    chosen_users.Add(hold_user);
                }
                Window Parent = (Window)this.Parent;
                Parent.DialogResult = true;
            }
        }
    }
}
