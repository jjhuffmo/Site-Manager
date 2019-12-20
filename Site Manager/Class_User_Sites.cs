using System;
using System.Collections.Generic;
using System.Text;

namespace Site_Manager
{
    public class User_Sites
    {
        public List<long> site_id
        {
            get;
            set;
        }
        public List<int> site_access
        {
            get;
            set;
        }
        public List<string> site_name
        {
            get;
            set;
        }
    }
}
