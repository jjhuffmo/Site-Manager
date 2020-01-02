using System;
using System.Collections.Generic;
using System.Text;

namespace Site_Manager
{
    public class Opened_Sites
    {
        private List<Sites> _site_info;
        private List<int> _tab_index;

        public List<int> tab_index
        {
            get { return _tab_index; }
            set
            {
                _tab_index = value;
            }
        }
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
            tab_index = new List<int>();
            site_info = new List<Sites>();
        }

        public void Insert_New(Sites site)
        {
            // Shift all tab_indexes by 1
            for (int i = 0; i < tab_index.Count; i++)
            {
                tab_index[i] += 1;
            }
            tab_index.Add(0);
            site_info.Add(site);
        }

        public void Close_Site(int tab_no)
        {
            int x;

            x = tab_index.IndexOf(tab_no);

            // If the tab number is found
            if (x >= 0)
            {
                // Shift all tab_indexes greater than the selected by 1
                for (int i = 0; i < tab_index.Count; i++)
                {
                    if (tab_index[i] > tab_no)
                        tab_index[i]--;
                }
                // Delete the record from the stack
                tab_index.RemoveAt(x);
                site_info.RemoveAt(x);
            }
        }
    }
}
