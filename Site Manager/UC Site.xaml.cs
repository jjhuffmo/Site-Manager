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

        public UC_Site(Sites current_site)
        {
            InitializeComponent();

            current_site.PropertyChanged += new PropertyChangedEventHandler(current_site_PropertyChanged);

            csite = current_site;
            Make_User_List();
            
        }

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

        private void handleTypingTimerTimeout(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer; 

            if (timer == null)
            {
                return;
            }

            if (csite.Check_Site(Short_Name.Text) == true)
                Site_Exists.Visibility = Visibility.Visible;
            else
                Site_Exists.Visibility = Visibility.Hidden;
            // The timer must be stopped! We want to act only once per keystroke.
            timer.Stop();
        }

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

            if (csite.Save_Site() == 1)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            csite.Short_Name = csite.Short_Name;

            this.Visibility = Visibility.Hidden;
        }

        private void Make_User_List()
        {
            Users users = new Users();
            users.Get_List();

            Users_List.Items.Clear();

            for (int i = 0; i < users.User_Name.Count; i++)
            {
                Users_List.Items.Add(users.User_Name[i]);
                //Users_List.
            }
        }
    }
}
