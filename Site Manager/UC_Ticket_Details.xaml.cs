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

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Ticket_Details.xaml
    /// </summary>
    public partial class UC_Ticket_Details : UserControl
    {
        public Site_Tickets Active_Ticket = new Site_Tickets();
        public bool _Changes_Made = false;
        public bool Changes_Made
        {
            get
            { return _Changes_Made; }
            set
            {
                _Changes_Made = value;
                if (value == true)
                {
                    btn_Cancel_Ticket.Visibility = Visibility.Visible;
                    btn_Save_Ticket.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_Cancel_Ticket.Visibility = Visibility.Hidden;
                    btn_Save_Ticket.Visibility = Visibility.Hidden;
                }
            }
        }

        public UC_Ticket_Details(Site_Tickets Ticket_Info)
        {
            Active_Ticket = Ticket_Info;

            // Set binding attributes to the Active Ticket
            this.DataContext = Active_Ticket;

            InitializeComponent();

            Active_Ticket.PropertyChanged += Active_Ticket_PropertyChanged;

            if (Active_Ticket.Just_Created)
            {
                txt_Ticket_No.Visibility = Visibility.Hidden;
                tb_Ticket_ID.Visibility = Visibility.Hidden;
                btn_Cancel_Ticket.Visibility = Visibility.Visible;
                btn_Save_Ticket.Visibility = Visibility.Visible;
            }
            else
            {
                txt_Ticket_No.Visibility = Visibility.Visible;
                tb_Ticket_ID.Visibility = Visibility.Visible;
                btn_Cancel_Ticket.Visibility = Visibility.Hidden;
                btn_Save_Ticket.Visibility = Visibility.Hidden;
            }
        }

        private void Active_Ticket_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Saved" || e.PropertyName == "Canceled")
                Changes_Made = false;
            else
                Changes_Made = true;
        }

        private void btn_Complete_Ticket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Save_Ticket_Click(object sender, RoutedEventArgs e)
        {
            if (Active_Ticket.Save_Ticket(Active_Ticket.Just_Created) == true)
            {
                MessageBox.Show("Ticket Successfully Saved");
                Active_Ticket.Saved = true;
            }
            else
                MessageBox.Show("Failed To Save Ticket");
        }

        private void btn_Cancel_Ticket_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel changes to the current ticket task?", "Cancel Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Active_Ticket.Canceled = true;
            }
        }
    }
}
