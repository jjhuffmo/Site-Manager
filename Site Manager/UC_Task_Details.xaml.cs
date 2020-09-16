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
    public partial class UC_Task_Details : UserControl
    {
        public Tickets_Tasks Active_Task = new Tickets_Tasks();

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
                    btn_Cancel_Task.Visibility = Visibility.Visible;
                    btn_Save_Task.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_Cancel_Task.Visibility = Visibility.Hidden;
                    btn_Save_Task.Visibility = Visibility.Hidden;
                }
            }
        }

        public UC_Task_Details(Tickets_Tasks Ticket_Info)
        {
            Active_Task = Ticket_Info;

            // Set binding attributes to the Active Ticket
            this.DataContext = Active_Task;

            InitializeComponent();

            Active_Task.PropertyChanged += Active_Ticket_PropertyChanged;

            if (Active_Task.Just_Created)
            {
                btn_Cancel_Task.Visibility = Visibility.Visible;
                btn_Save_Task.Visibility = Visibility.Visible;
            }
            else
            {
                btn_Cancel_Task.Visibility = Visibility.Hidden;
                btn_Save_Task.Visibility = Visibility.Hidden;
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

        private void btn_Save_Task_Click(object sender, RoutedEventArgs e)
        {
            if (Active_Task.Save_Task(Active_Task.Just_Created) == true)
            {
                MessageBox.Show("Ticket Successfully Saved");
                Active_Task.Saved = true;
            }
            else
                MessageBox.Show("Failed To Save Ticket");
        }

        private void btn_Cancel_Task_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel changes to the current ticket task?", "Cancel Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Active_Task.Canceled = true;
            }
        }

    }
}
