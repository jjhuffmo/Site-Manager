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
using System.Windows.Shapes;

namespace Site_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class dlgLogin : Window
    {
        public string Entered_User { get; set; }

        public dlgLogin()
        {

            InitializeComponent();
            uiUserName.Focus();
            uiUserName.TextChanged += UiUserName_TextChanged;
        }

        private void UiUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uiUserName.Text == "")
                btnLogin.IsEnabled = false;
            else
                btnLogin.IsEnabled = true;
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            Entered_User = uiUserName.Text;
            if (Entered_User != "")
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

}
