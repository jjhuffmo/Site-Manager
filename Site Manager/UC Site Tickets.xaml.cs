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

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Site_Tickets.xaml
    /// </summary>
    public partial class UC_Site_Tickets : UserControl
    {
        public ObservableCollection<Site_Tickets> site_tickets { get; set; }
        public UC_Site_Tickets()
        {
            this.DataContext = this;
            site_tickets = new ObservableCollection<Site_Tickets>();
            Site_Tickets new_ticket = new Site_Tickets();

            new_ticket.Created_On = DateTime.Now;
            new_ticket.Creator = "Jeremy";
            new_ticket.Desc = "Test Ticket";
            new_ticket.Due_On = DateTime.Now;
            new_ticket.Ticket_ID = 123456;

            site_tickets.Add(new_ticket);

            InitializeComponent();
        }

        //
        //  Function:   private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Save/Update the site info when clicked (after confirmation from a messagebox)
        //
        private void btn_Add_Ticket_Clicked(object sender, RoutedEventArgs e)
        {
            Site_Tickets new_ticket = new Site_Tickets();

            new_ticket.Created_On = DateTime.Now;
            new_ticket.Creator = "Jeremy2";
            new_ticket.Desc = "Test Ticket2";
            new_ticket.Due_On = DateTime.Now;
            new_ticket.Ticket_ID = 23456789;

            site_tickets.Add(new_ticket);
        }

        //
        //  Function:   private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Save/Update the site info when clicked (after confirmation from a messagebox)
        //
        private void btn_Remove_Ticket_Clicked(object sender, RoutedEventArgs e)
        {
            site_tickets.RemoveAt(0);
        }
    }
}
