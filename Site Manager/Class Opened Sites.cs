using System;
using System.Collections.Generic;
using System.Text;

namespace Site_Manager
{
    public class Opened_Sites
    {
        private List<Sites> _site_info;

        public List<Sites> site_info
        {
            get { return _site_info; }
            set
            {
                _site_info = value;
            }
        }

        public void Initialize()
        {
            site_info = new List<Sites>();
        }
    }
}
