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
        public UC_Ticket_Details(Site_Tickets Ticket_Info)
        {
            Active_Ticket = Ticket_Info;

            // Set binding attributes to the Active Ticket
            this.DataContext = Active_Ticket;

            InitializeComponent();

            if (Active_Ticket.Just_Created)
            {
                txt_Ticket_No.Visibility = Visibility.Hidden;
                tb_Ticket_ID.Visibility = Visibility.Hidden;
            }
            else
            {
                txt_Ticket_No.Visibility = Visibility.Visible;
                tb_Ticket_ID.Visibility = Visibility.Visible;
            }
        }

        private void btn_Complete_Ticket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Save_Ticket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
