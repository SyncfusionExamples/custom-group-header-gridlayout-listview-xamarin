using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
public class Behavior : Behavior<SfListView>
{
    protected override void OnAttachedTo(SfListView bindable)
    {
        bindable.QueryItemSize += Bindable_QueryItemSize;
        bindable.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "IsScrumMaster",
            Comparer = new GroupComparer()
        });
        base.OnAttachedTo(bindable);
    }
    private void Bindable_QueryItemSize(object sender, QueryItemSizeEventArgs e)
    {
        var bindingContext = (sender as SfListView).BindingContext as TeamInfoRepository;
        var collection = bindingContext.MemberDetails;
        var type = e.ItemType;
        if (type == ItemType.GroupHeader && (bool)(e.ItemData as GroupResult).Key) ;
        else if (type == ItemType.GroupHeader 
            && !(bool)(e.ItemData as GroupResult).Key)
        {
            e.ItemSize = 0;
            e.Handled = true;
        }
        if (type == ItemType.Record)
        {
            var isManager = (e.ItemData as TeamInfo).IsScrumMaster;
            if (isManager)
            {
                e.ItemSize = 0;
                e.Handled = true;
            }
        }
    }
    protected override void OnDetachingFrom(SfListView bindable)
    {
        bindable.QueryItemSize -= Bindable_QueryItemSize;
        base.OnDetachingFrom(bindable);
    }
}
}
