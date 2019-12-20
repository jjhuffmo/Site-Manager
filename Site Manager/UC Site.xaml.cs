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
    /// Interaction logic for UC_Site.xaml
    /// </summary>
    public partial class UC_Site : UserControl
    {
       
        public UC_Site(Sites current_site)
        {
            InitializeComponent();

             var bind_Short_Name = new Binding("ShortName")
            {
                Source = current_site.Short_Name
            };
            this.Short_Name.Text = "Test";
            this.Short_Name.SetBinding(TextBox.TextProperty, bind_Short_Name);
        }
    }
}
