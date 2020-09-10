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
using System.Data.SqlClient;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for UC_Task_Detail.xaml
    /// </summary>
    
    public partial class UC_Task_Detail : UserControl
    {
        public ObservableCollection<Tickets_Tasks> active_tasks { get; set; }

        public UC_Task_Detail(long Ticket_ID, int User_ID, bool All_Tasks)
        {
            this.DataContext = this;

            active_tasks = new ObservableCollection<Tickets_Tasks>();

            InitializeComponent();

            Load_Task_Details(Ticket_ID, User_ID, All_Tasks);
        }

        public void Load_Task_Details(long Ticket_ID, int User_ID, bool All_Tasks)
        {
            DB_Users users = new DB_Users();
            users.Get_List(0);

            // Read all the tickets for the assigned ticket ID
            active_tasks.Clear();

            // Open the database and retrieve tickets for the current user or all of them if all is selected
            string connString = SQLConnString;

            // Query the tickets for this site for this user
            StringBuilder query = new StringBuilder("SELECT * FROM ");
            query.Append(tblTasks);
            query.Append(" WHERE Ticket_ID = '");
            query.Append(Ticket_ID);
            query.Append("'");
            // If select all is not enabled, only show this users tickets
            if (All_Tasks == false)
            {
                query.Append(" AND (Assigned_User_ID = '");
                query.Append(User_ID);
                query.Append("' OR Creator_ID = '");
                query.Append(User_ID);
                query.Append("')");
            }

            // Read all the tasks associated with this ticket
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand(query.ToString(), sqlCon);
                using SqlDataReader reader = SqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    Tickets_Tasks ticket_task = new Tickets_Tasks();

                    ticket_task.Site_ID = ((long)reader[0]);
                    ticket_task.Ticket_ID = ((long)reader[1]);
                    ticket_task.Ticket_Task_ID = ((long)reader[2]);
                    ticket_task.Creator_ID = ((int)reader[3]);
                    for (int i = 0; i < users.User_ID.Count; i++)
                    {
                        if (users.User_ID[i] == ticket_task.Creator_ID)
                            ticket_task.Creator = users.User_Name[i];
                    }
                    if (!DBNull.Value.Equals(reader[4]))
                        ticket_task.Created_On = ((DateTime)reader[4]);
                    if (!DBNull.Value.Equals(reader[5]))
                        ticket_task.Task_Status = ((int)reader[5]);
                    if (!DBNull.Value.Equals(reader[6]))
                        ticket_task.Task_ID = ((long)reader[6]);
                    if (!DBNull.Value.Equals(reader[7]))
                        ticket_task.Priority = ((int)reader[7]);
                    if (!DBNull.Value.Equals(reader[8]))
                        ticket_task.Task_Overview = ((string)reader[8]);
                    if (!DBNull.Value.Equals(reader[9]))
                        ticket_task.Task_Details = ((string)reader[9]);
                    if (!DBNull.Value.Equals(reader[10]))
                        ticket_task.Assigned_User_ID = ((int)reader[10]);
                    for (int i = 0; i < users.User_ID.Count; i++)
                    {
                        if (users.User_ID[i] == ticket_task.Assigned_User_ID)
                            ticket_task.Assigned_User = users.User_Name[i];
                    }
                    if (!DBNull.Value.Equals(reader[11]))
                        ticket_task.Progress = ((int)reader[11]);
                    if (!DBNull.Value.Equals(reader[12]))
                        ticket_task.Due_On = ((DateTime)reader[12]);
                    if (!DBNull.Value.Equals(reader[13]))
                        ticket_task.Started_TS = ((DateTime)reader[13]);
                    if (!DBNull.Value.Equals(reader[14]))
                        ticket_task.Completed_TS = ((DateTime)reader[14]);
                    if (!DBNull.Value.Equals(reader[15]))
                        ticket_task.Notes = ((string)reader[15]);
                    if (!DBNull.Value.Equals(reader[16]))
                        ticket_task.Alarm1_Enabled = ((bool)reader[16]);
                    if (!DBNull.Value.Equals(reader[17]))
                        ticket_task.Alarm1 = ((DateTime)reader[17]);
                    if (!DBNull.Value.Equals(reader[18]))
                        ticket_task.Alarm1_Ack_TS = ((DateTime)reader[18]);
                    if (!DBNull.Value.Equals(reader[19]))
                        ticket_task.Alarm2_Enabled = ((bool)reader[19]);
                    if (!DBNull.Value.Equals(reader[20]))
                        ticket_task.Alarm2 = ((DateTime)reader[20]);
                    if (!DBNull.Value.Equals(reader[21]))
                        ticket_task.Alarm2_Ack_TS = ((DateTime)reader[21]);
                    if (!DBNull.Value.Equals(reader[22]))
                        ticket_task.Alarm3_Enabled = ((bool)reader[22]);
                    if (!DBNull.Value.Equals(reader[23]))
                        ticket_task.Alarm3 = ((DateTime)reader[23]);
                    if (!DBNull.Value.Equals(reader[24]))
                        ticket_task.Alarm3_Ack_TS = ((DateTime)reader[24]);
                    if (!DBNull.Value.Equals(reader[25]))
                        ticket_task.Late_Alarm = ((bool)reader[25]);
                    active_tasks.Add(ticket_task);
                }
            }
        }
    }
}
