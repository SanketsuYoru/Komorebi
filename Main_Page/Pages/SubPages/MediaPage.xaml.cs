using Main_Page.Models;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;
using static Main_Page.Models.ItemAccess;
using static Main_Page.Models.UserSettings;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MediaPage : Page
    {
        private static Items Item_;
        public MediaPage()
        {
            this.InitializeComponent();
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
            Media_in.MediaPlayer.VolumeChanged += MediaPlayer_VolumeChangedAsync;
        }

        private async void MediaPlayer_VolumeChangedAsync(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            try
            {
                UserSettings.localSettings.Values["Volume"] = sender.Volume.ToString();
            }
            catch (Exception e)
            {
                await new ContentDialog
                {
                    Title = "Error Occored",
                    Content = e.Message,
                    CloseButtonText = "Closed",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }
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

        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {

            var Items_in = (Items)e.Parameter;
            Item_ = Items_in;
            Media_in.Source =  ItemAccess.MediaProcess(Items_in.StorageFile_);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
           Media_in.MediaPlayer.IsMuted = true;
           // Media_in.MediaPlayer.Dispose();
        }

        private void RelativePanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ((MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender)).ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }




        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            await ItemAccess.ExplorePath(await Item_.StorageFile_.GetParentAsync(), Item_.StorageFile_);
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
                if (Item_.Name == item.Name && item.Path == Item_.Path)
                {
                    Cache_Processed_File.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_File)
                if (Item_.Name == item.Name && item.Path == Item_.Path)
                {
                    Cache_Processed_File.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_Doc)
                if (Item_.Name == item.Name && item.Path == Item_.Path)
                {
                    Cache_Processed_Doc.Remove(item);
                    break;
                }
            foreach (var item in Cache_Processed_Media)
                if (Item_.Name == item.Name && item.Path == Item_.Path)
                {
                    Cache_Processed_Media.Remove(item);
                    break;
                }


            await Item_.StorageFile_.DeleteAsync();
        }

        private async void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            await ItemAccess.ExplorePath(await Item_.StorageFile_.GetParentAsync(), Item_.StorageFile_);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Media_in.MediaPlayer.Volume = GetVolume();
        }
    }
}
