﻿using System;
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
    /// Interaction logic for UC_Ticket_Tasks.xaml
    /// </summary>
    public partial class UC_Ticket_Tasks : UserControl
    {
        public ObservableCollection<Tickets_Tasks> ticket_tasks { get; set; }

        public UC_Ticket_Tasks()
        {
            this.DataContext = this;
            ticket_tasks = new ObservableCollection<Tickets_Tasks>();
            Tickets_Tasks new_task = new Tickets_Tasks();
            InitializeComponent();
        }
    }
}