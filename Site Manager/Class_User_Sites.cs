using System;
using System.Collections.Generic;
using System.Text;

namespace Site_Manager
{
    public class User_Sites
    {
        private List<long> _site_id;
        private List<string> _site_name;
        private List<bool> _View_Resources;
        private List<bool> _Add_Resources;
        private List<bool> _Modify_Resources;
        private List<bool> _Del_Resources;
        private List<bool> _View_Tickets;
        private List<bool> _Add_Tickets;

        public List<long> site_id
        {
            get { return _site_id; }
            set
            {
                _site_id = value;
            }
        }

        public List<string> site_name
        {
            get { return _site_name; }
            set
            {
                _site_name = value;
            }
        }

        public List<bool> View_Resources
        {
            get { return _View_Resources; }
            set
            {
                _View_Resources = value;
            }
        }

        public List<bool> Add_Resources
        {
            get { return _Add_Resources; }
            set
            {
                _Add_Resources = value;
            }
        }

        public List<bool> Modify_Resources
        {
            get { return _Modify_Resources; }
            set
            {
                _Modify_Resources = value;
            }
        }

        public List<bool> Del_Resources
        {
            get { return _Del_Resources; }
            set
            {
                _Del_Resources = value;
            }
        }

        public List<bool> View_Tickets
        {
            get { return _View_Tickets; }
            set
            {
                _View_Tickets = value;
            }
        }

        public List<bool> Add_Tickets
        {
            get { return _Add_Tickets; }
            set
            {
                _Add_Tickets = value;
            }
        }



        public void Initialize()
        {
            site_id = new List<long>();
            site_name = new List<string>();
            View_Resources = new List<bool>();
            Add_Resources = new List<bool>();
            Modify_Resources = new List<bool>();
            Del_Resources = new List<bool>();
            View_Tickets = new List<bool>();
            Add_Tickets = new List<bool>();
        }
    }
}
