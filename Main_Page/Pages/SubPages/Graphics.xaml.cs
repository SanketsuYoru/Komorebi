using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// bug：显示重复
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Graphics : Page
    {
        private static Items Item_;
        //private static int currentIndex = 0;
       // bool find = false;
       // private ObservableCollection<Items> Item_list = new ObservableCollection<Items>();
        //Image insideimg = new Image();
        public Graphics()
        {
            this.InitializeComponent();
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
            
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation animation =
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimationMedia", img_dispaly);
                // Use the recommended configuration for back animation.
                animation.Configuration = new DirectConnectedAnimationConfiguration();
/*                ConnectedAnimation animationinfo =
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimationmc_info", img_dispaly);
                // Use the recommended configuration for back animation.
                animationinfo.Configuration = new DirectConnectedAnimationConfiguration();*/
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            
            Item_ = (Items)e.Parameter;
            img_dispaly.Visibility = Visibility.Collapsed;
            img_dispaly.Source = await BitmapProcess(Item_.StorageFile_);
            
            ConnectedAnimation animation =
    ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null )
            {
                animation.TryStart(img_dispaly );
                
            }
            img_dispaly.Visibility = Visibility.Visible;
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


        private async void Preview_ClickAsync(object sender, RoutedEventArgs e)
        {
            var StorageFile_ = await ItemAccess.GetnextFileAsync(((FrameworkElement)sender as Button).Name, Item_);
            //img_dispaly.Source =await BitmapProcess(Item_.StorageFile_);
            //img_dispaly.Source = await BitmapProcess(await ItemAccess.GetnextFileAsync(((FrameworkElement)sender as Button).Name, Item_));
            img_dispaly.Source = await BitmapProcess(StorageFile_);
            Item_= new Items { AccessSource = await ThumbnailProcess(StorageFile_), Date = StorageFile_.DateCreated.ToString(), Type = StorageFile_.DisplayType, Name = StorageFile_.Name, Path = StorageFile_.Path, StorageFile_ = StorageFile_, Size = await SizeOfFileAsync(StorageFile_) };

        }

        private async void Next_ClickAsync(object sender, RoutedEventArgs e)
        {
            var StorageFile_ = await ItemAccess.GetnextFileAsync(((FrameworkElement)sender as Button).Name, Item_);
            //img_dispaly.Source =await BitmapProcess(Item_.StorageFile_);
            //img_dispaly.Source = await BitmapProcess(await ItemAccess.GetnextFileAsync(((FrameworkElement)sender as Button).Name, Item_));
            img_dispaly.Source = await BitmapProcess(StorageFile_);
            Item_ = new Items { AccessSource = await ThumbnailProcess(StorageFile_), Date = StorageFile_.DateCreated.ToString(), Type = StorageFile_.DisplayType, Name = StorageFile_.Name, Path = StorageFile_.Path, StorageFile_ = StorageFile_, Size = await SizeOfFileAsync(StorageFile_) };

        }

    }
    }

