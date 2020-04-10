using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static Main_Page.Models.ItemAccess;
using static Main_Page.Models.UserSettings;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MEDIA_MAIN : Page
    {
        //DispatcherTimer _timer = new DispatcherTimer();//定义一个定时器
        Items item_;
        public MEDIA_MAIN()
        {
            this.InitializeComponent();

            //Background
            SolidColorBrush myBrush = GetBGColor();
            this.Background = myBrush;

            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
        }

        private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            await Refresh();
        }

        private void ShareRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            List<StorageFile> files = new List<StorageFile>();

            if (Source_.SelectionMode == ListViewSelectionMode.Multiple)
            {
                foreach (var item_M in Source_.SelectedItems)
                {
                    files.Add((item_M as Items).StorageFile_);
                }
            }
            else
            {
                files.Add(item_.StorageFile_);
            }
            var deferral = args.Request.GetDeferral();
            DataRequest request = args.Request;
            request.Data.Properties.Title = $"主题：Master向貴様共享了{files.Count}个文件";
            request.Data.SetText("Noblesse oblege,今后も救世主たらんことを");
            request.Data.SetStorageItems(files);
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(item_.StorageFile_));
            request.Data.RequestedOperation = DataPackageOperation.Link;
            deferral.Complete();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           // if (ItemAccess.NeedNav != "ItemInvoked")
               // Source_.SelectedIndex = Convert.ToInt32(e.Parameter);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Background
            SolidColorBrush myBrush = GetBGColor();
            this.Background = myBrush;
        }


        private void Source__ItemClick(object sender, ItemClickEventArgs e)
        {
          //  ItemAccess.NeedNav = "Media";
            var item_in = (Items)e.ClickedItem;
            var type_in = ItemAccess.FileType_check(item_in.StorageFile_);
            switch (type_in)
            {

                case "Picture":
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.Graphics), item_in, new DrillInNavigationTransitionInfo());
                        break;
                    }
                case "Video":
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.MediaPage), item_in, new DrillInNavigationTransitionInfo());
                        break;
                    }
                case "Music":
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.AudioPage), item_in, new DrillInNavigationTransitionInfo());
                        break;
                    }
                default:
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.ItemPage), item_in, new DrillInNavigationTransitionInfo());
                        break;
                    }
            }
        }

        private void Source__RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ((MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender)).ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }


        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            await ItemAccess.ExplorePath(await item_.StorageFile_.GetParentAsync(), item_.StorageFile_);
        }

        private void MenuFlyoutItem_Click_Copy(object sender, RoutedEventArgs e)
        {
            List<StorageFile> files = new List<StorageFile>();
            DataPackage dataPackage = new DataPackage();
            if (Source_.SelectionMode == ListViewSelectionMode.Multiple)
            {
                foreach (var item_M in Source_.SelectedItems)
                {
                    files.Add((item_M as Items).StorageFile_);
                }
            }
            else
            {
                item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
                files.Add(item_.StorageFile_);
            }
            dataPackage.SetStorageItems(files);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }

        private async void MenuFlyoutItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            img.Source = await ThumbnailProcess(item_.StorageFile_);
            if (Source_.SelectionMode == ListViewSelectionMode.Multiple)
            {
                dialogText1.Text = $"{item_.Name}等，共{Source_.SelectedItems.Count}个文件";
                dialogText3.Text = "：被删除的文件会进入回收站";
                dialogText2.Text = "";
            }
            else
            {
                dialogText1.Text = item_.Name;
                dialogText3.Text = item_.Path;
                dialogText2.Text = item_.Date;
            }

            await DeleteContentDialog.ShowAsync();
        }

            private void MenuFlyoutItem_Click_Share(object sender, RoutedEventArgs e)
        {
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            DataTransferManager.ShowShareUI();
        }


        private async void DeleteContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (Source_.SelectionMode == ListViewSelectionMode.Multiple)
            {
                List<Items> Temp_list = new List<Items>();
                foreach (var item_M in Source_.SelectedItems)
                {
                    Temp_list.Add(item_M as Items);
                }

                foreach (var item_D in Temp_list)
                {
                    Cache_Processed_Media.Remove(item_D);
                    Cache.Remove(item_D.StorageFile_);
                    //Cache_Processed_File.Remove(item_);
                    //Cache_Processed_Doc.Remove(item_);
                    foreach (var item in Cache_Processed)
                        if (item_D.Name == item.Name && item.Path == item_D.Path)
                        {
                            Cache_Processed.Remove(item);
                            break;
                        }
                    await item_D.StorageFile_.DeleteAsync();
                }
            }
            else
            {
                Cache_Processed_Media.Remove(item_);
                Cache.Remove(item_.StorageFile_);
                //Cache_Processed_File.Remove(item_);
                //Cache_Processed_Doc.Remove(item_);
                foreach (var item in Cache_Processed)
                    if (item_.Name == item.Name && item.Path == item_.Path)
                    {
                        Cache_Processed.Remove(item);
                        break;
                    }
                await item_.StorageFile_.DeleteAsync();
            }
        }

        private void Adap_stackpanel_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar_Main.Visibility = Visibility.Collapsed;
        }

        private void MultipleSelect_Click(object sender, RoutedEventArgs e)
        {
            if (Source_.SelectionMode == ListViewSelectionMode.Single)
            {
                (sender as MenuFlyoutItem).Text = "单选";
                Source_.SelectionMode = ListViewSelectionMode.Multiple;
                Source_.IsItemClickEnabled = false;
            }

            else
            {
                Source_.SelectionMode = ListViewSelectionMode.Single;
                (sender as MenuFlyoutItem).Text = "多选";
                Source_.IsItemClickEnabled = true;
            }
        }


        private void MenuFlyout_Opened(object sender, object e)
        {
            if (Source_.SelectionMode == ListViewSelectionMode.Single)
            {
                foreach (var Flyout_ in ((sender as MenuFlyout).Items))
                {
                    if (Flyout_.Name == "Multiple")
                        (Flyout_ as MenuFlyoutItem).Text = "多选";
                }
            }

            else
            {
                foreach (var Flyout_ in ((sender as MenuFlyout).Items))
                {
                    if (Flyout_.Name == "Multiple")
                        (Flyout_ as MenuFlyoutItem).Text = "单选";
                }
            }
        }

        private async void Reflash_Click(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        private void Source__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Item_index = Source_.SelectedIndex;
        }
    }
}
