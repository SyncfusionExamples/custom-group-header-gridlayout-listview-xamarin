using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class TeamInfo :INotifyPropertyChanged
    {
        #region Fields

        private string memberName;
        private ImageSource memberImage;
        public bool isScrumMaster = false;
        private string role;

        #endregion

        #region Constructor

        public TeamInfo()
        {

        }

        #endregion

        #region Public Properties

        public string Role
        {
            get { return this.role; }
            set
            {
                role = value;
                RaisePropertyChanged();
            }
        }

        public bool IsScrumMaster
        {
            get { return this.isScrumMaster; }
            set
            {
                this.isScrumMaster = value;
                RaisePropertyChanged();
            }
        }

        public string MemberName
        {
            get { return this.memberName; }
            set
            {
                this.memberName = value;
                RaisePropertyChanged();
            }
        }
        public ImageSource MemberImage
        {
            get { return this.memberImage; }
            set
            {
                this.memberImage = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
