# How to customize the ListView grouping with grid columns in Xamarin.Forms (SfListView) ?

You can customize grouping by a Comparer in [GroupDescriptor](https://help.syncfusion.com/cr/xamarin/Syncfusion.DataSource.Portable~Syncfusion.DataSource.GroupDescriptor.html) and GroupHeader using the [GroupHeaderTemplate](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~GroupHeaderTemplate.html) with converter.

Here, the GroupHeader is customized as ListViewItem using the converter in GroupHeaderTemplate.
``` xml
<syncfusion:SfListView x:Name="listView" GroupHeaderSize="300"
    AutoFitMode="DynamicHeight"
    SelectionMode="Single"
    ItemsSource="{Binding MemberDetails}">
    <syncfusion:SfListView.GroupHeaderTemplate>
        <DataTemplate>
            <StackLayout BackgroundColor="LightGray">
                <Image x:Name="ScrumMasterImage" Source="{Binding 
                    Converter={StaticResource Converter}, 
                    ConverterParameter=ScrumMasterImage}"
                    VerticalOptions="Center"
                    HeightRequest="80" WidthRequest="80"
                    HorizontalOptions="CenterAndExpand"
                    Margin="10" />
                <Label x:Name="ScrumMasterRole" Text="{Binding 
                    Converter={StaticResource Converter}, 
                    ConverterParameter=ScrumMasterRole}" 
                    FontSize="20"   
                    HorizontalOptions="CenterAndExpand" 
                    VerticalOptions="Center"/>
                <Label x:Name="ScrumMasterName" Text="{Binding 
                    Converter={StaticResource Converter}, 
                    ConverterParameter=ScrumMasterName}" 
                    FontSize="18"
                    Padding="0,0,0,10"
                    HorizontalOptions="CenterAndExpand" 
                    VerticalOptions="Center"/>
            </StackLayout>
        </DataTemplate>
    </syncfusion:SfListView.GroupHeaderTemplate>
</syncfusion:SfListView>
```
Converter, which designs the group header.
``` c#
class Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        string id = parameter as string;

        var groupResult = value as GroupResult;
        foreach (var item in groupResult.Items)
        {
            if(id == "ScrumMasterImage")
            {
                return (item as TeamInfo).MemberImage;
            }
            else if(id == "ScrumMasterRole")
            {
                return (item as TeamInfo).Role;
            }
            else if (id == "ScrumMasterName")
            {
                return (item as TeamInfo).MemberName;
            }
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```
The [GridLayout](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GridLayout.html) defined in the [LayoutManager](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~LayoutManager.html) with column count as 2 using the [SpanCount](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GridLayout~SpanCount.html) property.
``` xml
<syncfusion:SfListView x:Name="listView">
    <syncfusion:SfListView.LayoutManager>
        <syncfusion:GridLayout SpanCount="2"/>
    </syncfusion:SfListView.LayoutManager>
</syncfusion:SfListView>
```
The GroupDescriptor defined with comparer to sort the specific group always to be at the top of the rows.
``` c#
public class Behavior : Behavior<SfListView>
{
    protected override void OnAttachedTo(SfListView bindable)
    {
        bindable.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "IsScrumMaster",
            Comparer = new GroupComparer()
        });
        base.OnAttachedTo(bindable);
    }
}

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
```
[QueryItemSize](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~QueryItemSize_EV.html) to reset the Height of the groups that are not needed to show in the view to zero. 
``` c#
public class Behavior : Behavior<SfListView>
{
    protected override void OnAttachedTo(SfListView bindable)
    {
        bindable.QueryItemSize += Bindable_QueryItemSize;
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
```
**Output**

![CustomGroupHeader](https://github.com/SyncfusionExamples/custom-group-header-gridlayout-listview-xamarin/blob/master/ScreenShot/CustomGroupHeader.jpg)

