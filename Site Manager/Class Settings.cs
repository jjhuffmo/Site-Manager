using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Site_Manager
{
    // Defines all the optional settings throughout the program
    // Stored in a class so it can easily be written to registry
    // and parsed as necessary.
    public class Settings
    {
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private bool _Show_Sites;
        private bool _Show_All_Sites;

        public bool Show_Sites
        {
            get { return _Show_Sites; }
            set
            {
                _Show_Sites = value;
                OnPropertyChanged();
            }
        }

        public bool Show_All_Sites
        {
            get { return _Show_All_Sites; }
            set
            {
                _Show_All_Sites = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
