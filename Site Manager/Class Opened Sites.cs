using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Xps.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Site_Manager
{
    public class Opened_Sites : INotifyPropertyChanged
    {
        private Sites _site_info;
        private ObservableCollection<Users> _site_users = new ObservableCollection<Users>();
        private ObservableCollection<Site_Tickets> _site_tickets = new ObservableCollection<Site_Tickets>();
        private int _tab_index;
        private bool _changed;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public int tab_index
        {
            get { return _tab_index; }
            set
            {
                _tab_index = value;
            }
        }
        public Sites site_info
        {
            get { return _site_info; }
            set
            {
                _site_info = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Users> site_users
        {
            get { return _site_users; }
            set
            {
                _site_users = site_users;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Site_Tickets> site_tickets
        {
            get { return _site_tickets; }
            set
            {
                _site_tickets = site_tickets;
                OnPropertyChanged();
            }
        }
        public bool changed
        {
            get { return _changed; }
            set
            {
                _changed = value;
                OnPropertyChanged();
            }
        }
        public void Initialize()
        {
            site_users = new ObservableCollection<Users>();
            site_tickets = new ObservableCollection<Site_Tickets>();
            tab_index = new int();
            changed = new bool();
        }
    }
}
