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
using static Site_Manager.Resources;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Site_Tickets.xaml
    /// </summary>
    public partial class UC_Site_Tickets : UserControl
    {
        public ObservableCollection<Site_Tickets> site_tickets { get; set; }
        public User_Info user { get; set; }
        public Sites site_id { get; set; }
        public Users cuser { get; set; }

        public UC_Site_Tickets(Sites Site_ID, User_Info current_user, Users current_site_user)
        {
            this.DataContext = this;
            site_tickets = new ObservableCollection<Site_Tickets>();

            // Load the user and site information into local variables
            user = current_user;
            site_id = Site_ID;
            cuser = current_site_user;

            //Site_Tickets new_ticket = new Site_Tickets();

            InitializeComponent();

            Update_User_Options();
            //site_tickets_viewall += site_tickets_viewall.AddHandler(IsCh)
            btn_Ticket_Detail.Visibility = Visibility.Hidden;
            Load_Site_Tickets(Site_ID.Site_ID, current_user.User_ID, Site_ID.Short_Name, current_user.User_Name, (bool)site_tickets_viewall.IsChecked);

        }

        //
        //  Function:   private void btn_Save_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Add a new ticket to the site
        //
        private void btn_Add_Ticket_Clicked(object sender, RoutedEventArgs e)
        {
            Add_Ticket();
        }

        //
        //  Function:   private void btn_Ticket_Details_Clicked(object sender, RoutedEventArgs e)
        //
        //  Arguments:  object sender = object that called function
        //              RoutedEventArgs e = arguments for the event
        //
        //  Purpose:    Show the currently selected ticket's details
        //
        private void btn_Ticket_Details_Clicked(object sender, RoutedEventArgs e)
        {
            //Add_Ticket();
        }

        //
        //  Function:   private void Add_Ticket()
        //
        //  Purpose:    Add a new ticket to the current site
        //
        private void Add_Ticket()
        {
            Site_Tickets new_ticket = new Site_Tickets();

            new_ticket.Generate_New(site_id.Site_ID, "", cuser.User_ID, cuser.User_Name);
            new_ticket.Get_Site_Name(site_id.Site_ID);
            site_tickets.Add(new_ticket);

            //Task_Detail.Content = new UC_Ticket_Tasks(new_ticket.Ticket_ID, cuser.User_ID);
            //Task_Detail.Visibility = Visibility.Visible;
        }

        //
        //  Function:   private void Remove_Ticket()
        //
        //  Purpose:    Remove the currently selected ticket after confirmation.
        //
        private void Remove_Ticket()
        {
            site_tickets.RemoveAt(0);
        }

        public void Load_Site_Tickets(long site_id, long user_id, string site_name, string user_name, bool all_tickets)
        {
            int current_tick = 0;
            DB_Users users = new DB_Users();
            users.Get_List(0);

            // Delete the site tickets array before starting
            site_tickets.Clear();

            // Open the database and retrieve tickets for the current user or all of them if all is selected
            string connString = SQLConnString;

            // Query the tickets for this site for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblTickets);
            query.Append(" WHERE Site_ID = '");
            query.Append(site_id);
            query.Append("'");
            // If select all is not enabled, only show this users tickets
            if (all_tickets == false)
            {
                query.Append(" AND Creator_ID = '");
                query.Append(user_id);
                query.Append("'");
            }

            // Read all the tickets associated with this user
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    Site_Tickets site_ticket = new Site_Tickets();

                    site_ticket.Site_ID = ((long)reader[0]);
                    site_ticket.Get_Site_Name(site_ticket.Site_ID);
                    site_ticket.Ticket_ID = ((long)reader[1]);
                    site_ticket.Creator_User_ID = ((long)reader[2]);
                    for (int i = 0; i < users.User_ID.Count; i++)
                    {
                        if (users.User_ID[i] == (int)site_ticket.Creator_User_ID)
                        {
                            site_ticket.Creator = users.User_Name[i];
                            break;
                        }
                    }
                    site_ticket.Created_On = ((DateTime)reader[3]);
                    if (!DBNull.Value.Equals(reader[4]))
                        site_ticket.Due_On = ((DateTime)reader[4]);
                    if (!DBNull.Value.Equals(reader[5]))
                        site_ticket.Brief_Desc = ((string)reader[5]);
                    if (!DBNull.Value.Equals(reader[6]))
                        site_ticket.Desc = ((string)reader[6]);
                    if (!DBNull.Value.Equals(reader[7]))
                        site_ticket.Ticket_Status = ((int)reader[7]);
                    if (!DBNull.Value.Equals(reader[8]))
                        site_ticket.Notes = ((string)reader[8]);
                    if (!DBNull.Value.Equals(reader[9]))
                        site_ticket.Total_Tasks = ((int)reader[9]);
                    if (!DBNull.Value.Equals(reader[10]))
                        site_ticket.Completed_Tasks = ((int)reader[10]);
                    if (!DBNull.Value.Equals(reader[11]))
                        site_ticket.Active_Tasks = ((int)reader[11]);
                    site_tickets.Add(site_ticket);
                    current_tick++;
                }
            }
            if (current_tick == 0)
                Site_Ticket_List.Visibility = Visibility.Hidden;
            else
                Site_Ticket_List.Visibility = Visibility.Visible;
        }

        public void Update_User_Options()
        {
            // if the current user is an admin, just give them all rights to all settings
            // if (current_user.)
            // Define the current site user for access to the remaining portions
            cuser = cuser.Get_User_Settings(site_id.Site_ID, cuser.User_ID);

            // Determine if the current user can view all tickets
            if (cuser.View_Tickets == true)
                site_tickets_viewall.Visibility = Visibility.Visible;
            else
                site_tickets_viewall.Visibility = Visibility.Hidden;

            // Determine if the current user can add tickets
            if (cuser.Add_Tickets == true)
                btn_Add_Ticket.Visibility = Visibility.Visible;
            else
                btn_Add_Ticket.Visibility = Visibility.Hidden;
        }

        private void site_tickets_viewall_Click(object sender, RoutedEventArgs e)
        {
            Load_Site_Tickets(site_id.Site_ID, user.User_ID, site_id.Short_Name, user.User_Name, (bool)site_tickets_viewall.IsChecked);
        }

        public void Open_Site_Changed(object sender, PropertyChangedEventArgs e)
        {
            Update_User_Options();
        }

        private void Site_Ticket_List_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Site_Ticket_List.SelectedIndex > -1)
            {
                Site_Tickets sel_row;
                // Get the ticket_id from the list
                if (Site_Ticket_List.SelectedItem is null)
                    btn_Ticket_Detail.Visibility = Visibility.Hidden;
                else
                {
                    sel_row = (Site_Tickets)Site_Ticket_List.SelectedItem;
                    long ticket_id = sel_row.Ticket_ID;
                    Ticket_Detail.Content = new UC_Ticket_Details(sel_row);
                    Task_List.Content = new UC_Ticket_Tasks(ticket_id, cuser.User_ID);
                    Task_List.Visibility = Visibility.Visible;
                    btn_Ticket_Detail.Visibility = Visibility.Visible;
                }
            }
            else
                btn_Ticket_Detail.Visibility = Visibility.Hidden;
        }
    }
}
