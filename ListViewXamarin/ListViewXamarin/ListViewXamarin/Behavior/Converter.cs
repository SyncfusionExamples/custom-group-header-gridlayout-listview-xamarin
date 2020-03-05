using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string id = parameter as string;

            var groupResult = value as GroupResult;
            var item = groupResult.Items.ToList<TeamInfo>().First();

            if (id == "ScrumMasterImage")
            {
                return (item as TeamInfo).MemberImage;
            }
            else if (id == "ScrumMasterRole")
            {
                return (item as TeamInfo).Role;
            }
            else if (id == "ScrumMasterName")
            {
                return (item as TeamInfo).MemberName;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
