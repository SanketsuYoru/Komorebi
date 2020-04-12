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
using Windows.Media.Playback;
using Windows.Security.ExchangeActiveSyncProvisioning;
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
    public sealed partial class AudioPage : Page
    {
        DispatcherTimer _timer = new DispatcherTimer();//定义定时器
        private static bool needPlayNext=false;
        private static Items Item_;
        private static int currentIndex=0;
        private ObservableCollection< Items > Item_list = new ObservableCollection<Items>();
        public AudioPage()
        {
            this.InitializeComponent();
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
            //Media_in.MediaPlayer.Volume = UserSettings.GetVolume();

            Media_in.MediaPlayer.VolumeChanged += MediaPlayer_VolumeChangedAsync; ;
            Media_in.MediaPlayer.MediaEnded += next;
           _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += CurrentTime;
            _timer.Start();
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
           if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation animation =
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimationmc", CoverIMG);
                ConnectedAnimation animationinfo =
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimationmc_info", Info);
                // Use the recommended configuration for back animation.
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animationinfo.Configuration = new DirectConnectedAnimationConfiguration();
            }
        }

        private async void MediaPlayer_VolumeChangedAsync(MediaPlayer sender, object args)
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


        private async void PlayNext()
        {
            if (Item_list.Count != 0)
            {
                currentIndex = Math.Abs((currentIndex + 1) % Item_list.Count);
                PlayBackList.SelectedIndex = currentIndex;
                PlayBackList.ScrollIntoView(Item_list[currentIndex]);
                await setNewMediaSource(Item_list[currentIndex]);
            }
            

        }

        private void next(object sender, object e) {
            needPlayNext = true;
        }

      private async void CurrentTime(object sender, object e)
        {

            //Timerofnow.Text = Media_in.MediaPlayer.PlaybackSession.Position.ToString(@"hh\:mm\:ss");
           // Timerofthis.Text = "/" + Media_in.MediaPlayer.PlaybackSession.NaturalDuration.ToString(@"hh\:mm\:ss");
            if (needPlayNext == true)
            {
               PlayNext();
                needPlayNext = false;
            }
        }
            private async Task  setNewMediaSource(Items Nextsource) {
            Item_ = Nextsource;
            Media_in.Source = ItemAccess.MediaProcess(Item_.StorageFile_);
            CoverIMG.Source = await ItemAccess.ThumbnailProcess_HQ(Item_.StorageFile_);
            Type_.Text = Item_.StorageFile_.DisplayType;
            Date_.Text = Item_.Date;
            NameBolck.Text = Item_.StorageFile_.DisplayName;
            Size_.Text = await SizeOfFileAsync(Item_.StorageFile_);
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

            
            var Items_in = (Items)e.Parameter;
            Item_ = Items_in;
            CoverIMG.Visibility = Visibility.Collapsed;
            PlayArea.Visibility= Visibility.Collapsed;
            await setNewMediaSource(Items_in);
            
            base.OnNavigatedTo(e);

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation_mc");
            ConnectedAnimation animation_info =
               ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation_mcinfo");
            if (animation != null)
                animation.TryStart(CoverIMG);
            if (animation_info != null)
                animation_info.TryStart(LogoAndTitle, new UIElement[] {  PlayArea, Info, PlayBackList });
               
            CoverIMG.Visibility = Visibility.Visible;
            PlayArea.Visibility = Visibility.Visible;
            int count_ = 0;
            try
            {
                for (int i = 0; i < ItemAccess.Cache.Count; i++)
                {
                    if (FileType_check(ItemAccess.Cache[i]) == "Music")
                    {
                        Item_list.Add(new Items { AccessSource = null, Date = ItemAccess.Cache[i].DateCreated.ToString(), Type = ItemAccess.Cache[i].DisplayType, Name = ItemAccess.Cache[i].Name, Path = ItemAccess.Cache[i].Path, StorageFile_ = ItemAccess.Cache[i], Size = await SizeOfFileAsync(ItemAccess.Cache[i]), Count = count_ });
                        if (Items_in.Name == ItemAccess.Cache[i].Name)
                        {
                            PlayBackList.SelectedIndex = count_;
                            PlayBackList.ScrollIntoView(Item_list[count_]);
                            currentIndex = count_;
                        }
                        count_++;
                    }
                }
            }
            catch(Exception e1)
            {
                //TODO: 保存用户数据
                await new ContentDialog
                {
                    Title = "Error Occored",
                    Content = e1.Message,
                    CloseButtonText = "Closed",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }
            finally {
               //毁灭性错误
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
         Media_in.MediaPlayer.IsMuted = true;
            Item_list.Clear();
          //  Media_in.MediaPlayer.Dispose();
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
            Media_in.MediaPlayer.Pause();
            _timer.Stop();
            try
            {
                await Item_.StorageFile_.DeleteAsync();
                foreach (var item in Item_list)
                    if (Item_.Name == item.Name && item.Path == Item_.Path)
                    {
                        Item_list.Remove(item);
                        break;
                    }
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
                foreach (var item in Item_list)
                    if (Item_.Name == item.Name && item.Path == Item_.Path)
                    {
                        Item_list.Remove(item);
                        break;
                    }
                PlayNext();
            }
            catch (Exception e)
            {
                tip.Text = e.ToString();
                recentTip.IsOpen = true;
            }
            Media_in.MediaPlayer.Pause();
            _timer.Start();
        }

        private async void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            await ItemAccess.ExplorePath(await Item_.StorageFile_.GetParentAsync(), Item_.StorageFile_);
        }

        private void Share_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private async void PlayBackList_ItemClick(object sender, ItemClickEventArgs e)
        {
            _timer.Stop();
            var ClickedItem = (e.ClickedItem as Items);
            await setNewMediaSource(ClickedItem);
            currentIndex = ClickedItem.Count;
            _timer.Start();
        }

        private void More_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //NameBolck.Text
            int count=0;
            foreach (var temp in Item_list)
            {
                if (NameBolck.Text == temp.Name)
                {
                    Item_ = temp;
                    currentIndex = count-1;
                    break;
                }
                count++;
            }

           
            ((MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender)).ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }

        private void PlayBackList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            int count = 0;
            foreach (var temp in Item_list)
            {
                if (Item_.Name== temp.Name)
                {
                    currentIndex = count - 1;
                    break;
                }
                count++;
            }
           
            ((MenuFlyout)FlyoutBase.GetAttachedFlyout((FrameworkElement)sender)).ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.System.Profile.AnalyticsVersionInfo analyticsVersion = Windows.System.Profile.AnalyticsInfo.VersionInfo;
            var body = "";
            body += "平台" + analyticsVersion.DeviceFamily;
            body += System.Environment.NewLine;
            ulong v = ulong.Parse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);
            body += "    版本" + $"{v1}.{v2}.{v3}.{v4}";
            body += System.Environment.NewLine;
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            body += "   应用平台 " + package.Id.Architecture.ToString();
            body += System.Environment.NewLine;
            body += "   程序名称 " + "Komorenobi_UWP";
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            body += System.Environment.NewLine;
            body += "   机器制造商" + eas.SystemManufacturer;
            var address = "KomorenobiProject_2019@outlook.com";
            var subject = "Error：";
             body += tip.Text;
            var mailto = new Uri($"mailto:{address}?subject={subject}&body={body}");
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Media_in.MediaPlayer.Volume = UserSettings.GetVolume();
        }

    }
}
