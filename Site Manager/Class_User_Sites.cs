using System;
using System.Collections.Generic;
using System.Text;

namespace Site_Manager
{
    public class User_Sites
    {
        private List<long> _site_id;
        private List<int> _site_access;
        private List<string> _site_name;

        public List<long> site_id
        {
            get { return _site_id; }
            set
            {
                _site_id = value;
            }
        }
        public List<int> site_access
        {
            get { return _site_access; }
            set
            {
                _site_access = value;
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

        public void Initialize()
        {
            site_id = new List<long>();
            site_access = new List<int>();
            site_name = new List<string>();
        }
    }
}
