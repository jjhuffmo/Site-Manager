using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Site_Manager
{
    public class Tickets_Tasks : INotifyPropertyChanged
    {
        private long _Site_ID = 0;
        private long _Ticket_ID = 0;
        private long _Ticket_Task_ID = 0;
        private DateTime _Created_On;
        private int _Creator_ID = 0;
        private string _Creator = "";
        private int _Task_Status = 0;
        private long _Task_ID = 0;
        private int _Priority = 0;
        private string _Task_Overview = "";
        private string _Task_Details = "";
        private int _Assigned_User_ID = 0;
        private int _Progress = 0;
        private DateTime _Due_On;
        private DateTime _Started_TS;
        private DateTime _Completed_TS;
        private string _Notes = "";
        private bool _Alarm1_Enabled = false;
        private DateTime _Alarm1;
        private DateTime _Alarm1_Ack_TS;
        private bool _Alarm2_Enabled = false;
        private DateTime _Alarm2;
        private DateTime _Alarm2_Ack_TS;
        private bool _Alarm3_Enabled = false;
        private DateTime _Alarm3;
        private DateTime _Alarm3_Ack_TS;
        private bool _Late_Alarm = false;

        private string _Site = "";
        private string _Assigned_User = "";

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public long Site_ID
        {
            get { return _Site_ID; }
            set
            {
                _Site_ID = value;
            }
        }

        public long Ticket_ID
        {
            get { return _Ticket_ID; }
            set
            {
                _Ticket_ID = value;
            }
        }

        public long Ticket_Task_ID
        {
            get { return _Ticket_Task_ID; }
            set
            {
                _Ticket_Task_ID = value;
            }
        }

        public long Task_ID
        {
            get { return _Task_ID; }
            set
            {
                _Task_ID = value;
            }
        }

        public int Creator_ID
        {
            get { return _Creator_ID; }
            set
            {
                _Creator_ID = value;
            }
        }

        public int Assigned_User_ID
        {
            get { return _Assigned_User_ID; }
            set
            {
                _Assigned_User_ID = value;
            }
        }

        public int Task_Status
        {
            get { return _Task_Status; }
            set
            {
                _Task_Status = value;
            }
        }

        public int Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
            }
        }

        public int Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
            }
        }

        public DateTime Created_On
        {
            get { return _Created_On; }
            set
            {
                _Created_On = value;
            }
        }

        public DateTime Due_On
        {
            get { return _Due_On; }
            set
            {
                _Due_On = value;
            }
        }

        public DateTime Started_TS
        {
            get { return _Started_TS; }
            set
            {
                _Started_TS = value;
            }
        }

        public DateTime Completed_TS
        {
            get { return _Completed_TS; }
            set
            {
                _Completed_TS = value;
            }
        }

        public DateTime Alarm1
        {
            get { return _Alarm1; }
            set
            {
                _Alarm1 = value;
            }
        }

        public DateTime Alarm1_Ack_TS
        {
            get { return _Alarm1_Ack_TS; }
            set
            {
                _Alarm1_Ack_TS = value;
            }
        }

        public DateTime Alarm2
        {
            get { return _Alarm2; }
            set
            {
                _Alarm2 = value;
            }
        }

        public DateTime Alarm2_Ack_TS
        {
            get { return _Alarm2_Ack_TS; }
            set
            {
                _Alarm2_Ack_TS = value;
            }
        }

        public DateTime Alarm3
        {
            get { return _Alarm3; }
            set
            {
                _Alarm3 = value;
            }
        }

        public DateTime Alarm3_Ack_TS
        {
            get { return _Alarm3_Ack_TS; }
            set
            {
                _Alarm3_Ack_TS = value;
            }
        }

        public string Creator
        {
            get { return _Creator; }
            set
            {
                _Creator = value;
            }
        }

        public string Task_Overview
        {
            get { return _Task_Overview; }
            set
            {
                _Task_Overview = value;
            }
        }

        public string Task_Details
        {
            get { return _Task_Details; }
            set
            {
                _Task_Details = value;
            }
        }

        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
            }
        }

        public string Site
        {
            get { return _Site; }
            set
            {
                _Site = value;
            }
        }

        public string Assigned_User
        {
            get { return _Assigned_User; }
            set
            {
                _Assigned_User = value;
            }
        }

        public bool Alarm1_Enabled
        {
            get { return _Alarm1_Enabled; }
            set
            {
                _Alarm1_Enabled = value;
            }
        }

        public bool Alarm2_Enabled
        {
            get { return _Alarm2_Enabled; }
            set
            {
                _Alarm2_Enabled = value;
            }
        }
        public bool Alarm3_Enabled

        {
            get { return _Alarm3_Enabled; }
            set
            {
                _Alarm3_Enabled = value;
            }
        }

        public bool Late_Alarm
        {
            get { return _Late_Alarm; }
            set
            {
                _Late_Alarm = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //
        //  Function:   public int Generate_New()
        //
        //  Purpose:    Create a new ticket with some default values so we don't run into NULL's
        //
        public void Generate_New(long site_id, string site_name, int user_id, string user)
        {
            Site_ID = site_id;
            Ticket_ID = 0;
            Ticket_Task_ID = 0;
            Creator_ID = 0;
            Creator = "";
            Created_On = DateTime.Now;
            Task_Status = 0;
            Task_ID = 0;
            Priority = 0;
            Task_Overview = "New Task";
            Task_Details = "";
            Assigned_User_ID = user_id;
            Progress = 0;
            Due_On = DateTime.Now;
            Started_TS = DateTime.Now;
            Completed_TS = DateTime.Now;
            Notes = "";
            Alarm1 = DateTime.Now;
            Alarm1_Enabled = false;
            Alarm1_Ack_TS = DateTime.Now;
            Alarm2 = DateTime.Now;
            Alarm2_Enabled = false;
            Alarm2_Ack_TS = DateTime.Now;
            Alarm3 = DateTime.Now;
            Alarm3_Enabled = false;
            Alarm3_Ack_TS = DateTime.Now;
            Late_Alarm = false;
            Site = site_name;
            Assigned_User = user;
        }
    }
}
