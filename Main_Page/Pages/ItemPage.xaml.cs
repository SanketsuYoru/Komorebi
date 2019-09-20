using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Main_Page.Models;
using static Main_Page.Models.ItemAccess;
using static Main_Page.Models.UserSettings;
using Main_Page.Pages;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ItemPage : Page
    {
        private static  Items Item_;

        public ItemPage()
        {

            
            this.InitializeComponent();
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
            //BackGround
            SolidColorBrush myBrush = GetBGColor();
           // Top_Bar.Background = myBrush;
            OpenFolder.Background = myBrush;

        }


        private void ShareRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            List<StorageFile> files = new List<StorageFile>();
            files.Add(Item_.StorageFile_);
            var deferral = args.Request.GetDeferral();
            DataRequest request = args.Request;
            request.Data.Properties.Title = "主题：Master向貴様共享了一个文件";
            request.Data.SetText("Noblesse oblege,今后も救世主たらんことを");
            request.Data.SetStorageItems(files);
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(Item_.StorageFile_));
            request.Data.RequestedOperation = DataPackageOperation.Link;
            deferral.Complete();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Item_ =(Items)e.Parameter;
            item_inside.Source = await ItemAccess.ThumbnailProcess(Item_.StorageFile_);
            Size.Text = await ItemAccess.SizeOfFileAsync(Item_.StorageFile_);
            Name.Text = Item_.StorageFile_.Name;
            Date.Text = Item_.StorageFile_.DateCreated.ToString();
            Type.Text = Item_.StorageFile_.DisplayType;
            Path.Text = Item_.StorageFile_.Path;
        }

        private void RelativePanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ((MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender)).ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }



        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            await ItemAccess.ExplorePath(await Item_.StorageFile_ .GetParentAsync(), Item_.StorageFile_);
        }

        private void MenuFlyoutItem_Click_Copy(object sender, RoutedEventArgs e)
        {
            List<StorageFile> files = new List<StorageFile>();
            DataPackage dataPackage = new DataPackage();
            files.Add(Item_.StorageFile_);
            dataPackage.SetStorageItems(files);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }

        private async void MenuFlyoutItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            img.Source = await ThumbnailProcess(Item_.StorageFile_);
            dialogText1.Text = Item_.StorageFile_.Name;
            dialogText3.Text = Item_.StorageFile_.Path;
            dialogText2.Text = Item_.StorageFile_.DateCreated.ToString();
            await DeleteContentDialog.ShowAsync();

            // FlyoutBase.GetAttachedFlyout((FrameworkElement)sender).Placement = FlyoutPlacementMode.Full;
            //  FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

            //location_.Locations.Remove(item_);
            // await item_.StorageFile_.DeleteAsync();
        }

        private void MenuFlyoutItem_Click_Share(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }


        private async void DeleteContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Cache.Remove(Item_.StorageFile_);
            //Cache_Processed_File.Remove(item_);
            //Cache_Processed_Doc.Remove(item_);

            foreach (var item in Cache_Processed)
                if (Item_.Name == item.Name && item.Path ==Item_.Path)
                {
                    Cache_Processed_File.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_File)
                if (Item_.Name == item.Name && item.Path ==Item_.Path)
                {
                    Cache_Processed_File.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_Doc)
                if (Item_.Name == item.Name && item.Path ==Item_.Path)
                {
                    Cache_Processed_Doc.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_Media)
                if (Item_.Name == item.Name && item.Path ==Item_.Path)
                {
                    Cache_Processed_Media.Remove(item);
                    break;
                }


            await Item_.StorageFile_. DeleteAsync();
        }

        private async void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            await ItemAccess.ExplorePath(await Item_.StorageFile_.GetParentAsync(), Item_.StorageFile_);
        }
    }
}
