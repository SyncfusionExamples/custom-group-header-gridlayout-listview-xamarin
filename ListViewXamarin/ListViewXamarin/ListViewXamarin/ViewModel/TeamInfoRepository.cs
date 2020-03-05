using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    class TeamInfoRepository
    {
        public ObservableCollection<TeamInfo> MemberDetails { get; set; }

        #region Get Team Details

        public TeamInfoRepository()
        {
            MemberDetails = new ObservableCollection<TeamInfo>();
            GenerateCollection();
        }

        public void GenerateCollection()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            TeamInfo details;

            for (int i = 0; i < 7; i++)
            {
                details = new TeamInfo()
                {
                    MemberName = MemberNames[i],
                    MemberImage = ImageSource.FromResource("ListViewXamarin.Images.Image" + i + ".png", typeof(MainPage)),
                    Role = "Developer"
                };
                if (details.MemberName == "Gina")
                {
                    details.IsScrumMaster = true;
                    details.Role = "Scrum Master";
                }
                MemberDetails.Add(details);
            }
        }
        #endregion

        #region Team Information

        string[] MemberNames = new string[]
        {
            "Kyle",
            "Gina",
            "John",
            "Katie",
            "Rosy",
            "Oscar",
            "Kelly",
        };
        #endregion
    }
}
