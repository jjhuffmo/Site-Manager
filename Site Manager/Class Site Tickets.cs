using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Site_Manager
{
    public class Site_Tickets : INotifyPropertyChanged
    {
        private long _Site_ID = 0;
        private long _Ticket_ID = 0;
        private long _Creator_User_ID = 0;
        private DateTime _Created_On;
        private DateTime _Due_On;
        private string _Desc = "";
        private string _Notes = "";
        private int _Ticket_Status = 0;
        private int _Total_Tasks = 0;
        private int _Completed_Tasks = 0;
        private int _Incomplete_Tasks = 0;

        private DateTime _Started_TS;
        private DateTime _Completed_TS;

        private string _Site = "";
        private string _Creator = "";

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

        public long Creator_User_ID
        {
            get { return _Creator_User_ID; }
            set
            {
                _Creator_User_ID = value;
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

        public string Desc
        {
            get { return _Desc; }
            set
            {
                _Desc = value;
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

        public string Creator
        {
            get { return _Creator; }
            set
            {
                _Creator = value;
            }
        }

        public int Ticket_Status
        {
            get { return _Ticket_Status; }
            set
            {
                _Ticket_Status = value;
            }
        }

        public int Total_Tasks
        {
            get { return _Total_Tasks; }
            set
            {
                _Total_Tasks = value;
            }
        }

        public int Completed_Tasks
        {
            get { return _Completed_Tasks; }
            set
            {
                _Completed_Tasks = value;
            }
        }

        public int Incomplete_Tasks
        {
            get { return _Incomplete_Tasks; }
            set
            {
                _Incomplete_Tasks = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //
        //  Function:   public int Generate_New()
        //
        //  Purpose:    Create a new ticket with some default values so we don't run into NULL's
        //
        public void Generate_New(long site_id, string site_name, long user_id, string user)
        {
            Site_ID = site_id;
            Ticket_ID = 0;
            Creator_User_ID = user_id;
            Created_On = DateTime.Now;
            Due_On = DateTime.Now;
            Desc = "New Ticket";
            Ticket_Status = 0;
            Started_TS = DateTime.Now;
            Completed_TS = DateTime.Now;
            Notes = "";
            Site = site_name;
            Creator = user;
            Total_Tasks = 0;
            Completed_Tasks = 0;
            Incomplete_Tasks = 0;
        }

        public void Load_Site_Tickets(long site_id, string site_name, long user_id, string user)
        {

        }
    }
}
