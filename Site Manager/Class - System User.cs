using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Site_Manager
{
    // Defines access for various system wide options including Menus
    class System_User : INotifyPropertyChanged
    {
        private int _Access;

        public string User_Name = "";
        public int Access = 0;

       /* protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }*/

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            
        }

        public int User_Access
        {
            get { return _Access; }
            set
            {
                _Access = value;
                OnPropertyChanged("User_Access");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
