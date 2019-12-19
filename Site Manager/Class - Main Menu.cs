using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Site_Manager
{
    public class Main_Menu
    {
        public Menu menu = new Menu()
        {
            Name = "main_menu"
        };
        public MenuItem mnuUser = new MenuItem()
        {
            Name = "mmenu_user",
            Header = "User",
            FontWeight = FontWeights.Bold
        };
        public MenuItem mnuLogin = new MenuItem()
        {
            Name = "mmenu_login"
        };

        public void Initialize()
        {
            menu.Items.Add(mnuUser);
            mnuUser.Items.Add(mnuLogin);
        }

    }
}
