using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListViewXamarin
{
    public class GroupComparer : IComparer<GroupResult>
    {
        public int Compare(GroupResult x, GroupResult y)
        {
            if (x.Count > y.Count) 
            {
                return 1;
            }
            else if (x.Count < y.Count)
            {
                return -1;
            }

            return 0;
        }
    }
}
