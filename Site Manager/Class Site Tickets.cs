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
        private long _Creator_ID = 0;
        private DateTime _Created_On;
        private DateTime _Due_On;
        private string _Desc = "";
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

        public long Creator_ID
        {
            get { return _Creator_ID; }
            set
            {
                _Creator_ID = value;
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

        public string Desc
        {
            get { return _Desc; }
            set
            {
                _Desc = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
