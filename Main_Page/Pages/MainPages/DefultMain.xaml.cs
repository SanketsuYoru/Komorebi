using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
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
    public sealed partial class MAIN_Page1 : Page
    {
        DispatcherTimer _timer = new DispatcherTimer();//定义定时器
        DispatcherTimer ProgressBar_timer = new DispatcherTimer();//定义定时器
        Items item_;
        public MAIN_Page1()
        {

            this.InitializeComponent();
            //Timer
            comlpeteEvents += refreshComplete;
            _timer.Interval = TimeSpan.FromSeconds(4.5);
            _timer.Tick += ChangeImage_MainFlip;
            _timer.Tick += ChangeImage_Music;
            ProgressBar_timer.Interval = TimeSpan.FromSeconds(0.5);
            ProgressBar_timer.Tick += ProgressCheck;
            _timer.Start();
            ProgressBar_timer.Start();
            //BackGround
            SolidColorBrush myBrush = GetBGColor();
            var ButtonBrush = GetBGColor(200);
            this.Background = myBrush;
            REFLASH.Background = ButtonBrush;
            MB_1.Background = ButtonBrush;
            MB_2.Background = ButtonBrush;
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

            //MUSIC.PrepareConnectedAnimation("portrait", item, "PortraitEllipse");
            //ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation_mc", MUSIC);
            // ContactsListView.PrepareConnectedAnimation("portrait", item, "PortraitEllipse");
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            REFLASH.IsEnabled = true;

            //  UserSettings.localSettings.Values["firstOpen998"] = "opened";
        }



        private void ChangeImage_MainFlip(object sender, object e)
        {
            if (povit_main.SelectedIndex == 0)
            {
                //MainFlip.
                try
                {
                    if (MainFlip.Items != null && MainFlip.Items.Count > 1 && MainFlip.SelectedIndex < MainFlip.Items.Count - 1)
                    {
                        MainFlip.SelectedIndex++;
                        progressBar_Main.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        var temp_ = MainFlip.SelectedIndex;
                        for (int i = 0; i < temp_; i++)
                            MainFlip.SelectedIndex--;
                    }
                }
                catch (Exception e1)
                {
                    Debug.WriteLine("主页，图片切换异常" + e1.ToString());
                }

            }
        }

        private void ChangeImage_Music(object sender, object e)
        {
            if (povit_main.SelectedIndex == 0)
            {
                try
                {

                    if (MUSIC.Items != null && MUSIC.Items.Count > 1 && MUSIC.SelectedIndex < MUSIC.Items.Count - 1)
                    {
                        MUSIC.SelectedIndex++;
                        if (progressBar_Main.Visibility == Visibility.Visible && progressBar_Main != null)
                            progressBar_Main.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MUSIC.SelectedIndex = 0;
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("主页，图片切换异常");
                }
            }
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


        private void ProgressCheck(object sender, object e)
        {
            var Value_ = ((Cache.Count / (double)UserSettings.GetMaxNumber()) * 100);
            ScanPbar.Value = Value_;
            Progress.Text = ScanPbar.Value.ToString() + "%";
        }

        private void refreshComplete()
        {
            ProgressBar_timer.Tick -= ProgressCheck;
            REFLASH.IsEnabled = true;
            Progress.Text = 100 + "%";
            ScanPbar.Value = 100;
            progressBar_Main.Visibility = Visibility.Collapsed;
            if (ItemAccess.Cache.Count == 0)
                Frame.Navigate(typeof(Pages.GUIDE_page));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressBar_timer.Start();
            _timer.Start();
            //progressBar_Main.Value = ItemAccess.Cache.Count/ItemAccess.Cache_Processed.Count;
            SolidColorBrush myBrush = GetBGColor();
            this.Background = myBrush;
            progressBar_Main.Visibility = Visibility.Visible;
        }


        private void Source__ItemClick(object sender, ItemClickEventArgs e)
        {
            // ItemAccess.NeedNav = "All_New";
            item_ = (Items)e.ClickedItem;
            var type_in = ItemAccess.FileType_check(item_.StorageFile_);
            switch (type_in)
            {

                case "Picture":
                    {
                        var contatiner = Source_.ContainerFromItem(e.ClickedItem) as GridViewItem;
                        if (contatiner != null)
                        {
                            var temp = contatiner.Content as Items;
                            Source_.PrepareConnectedAnimation("forwardAnimation", temp, "CoverIMG");
                            // MUSIC.PrepareConnectedAnimation
                            //("forwardAnimation", temp, "MusicSourceImg");
                        }
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.Graphics), item_, new SuppressNavigationTransitionInfo());
                        break;
                    }
                case "Video":
                    {
                        var contatiner = Source_.ContainerFromItem(e.ClickedItem) as GridViewItem;
                        if (contatiner != null)
                        {
                            var temp = contatiner.Content as Items;
                            Source_.PrepareConnectedAnimation("forwardAnimation", temp, "CoverIMG");
                            // MUSIC.PrepareConnectedAnimation
                            //("forwardAnimation", temp, "MusicSourceImg");
                        }
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.MediaPage), item_, new SuppressNavigationTransitionInfo());
                        break;
                    }
                case "Music":
                    {
                        var contatiner = Source_.ContainerFromItem(e.ClickedItem) as GridViewItem;
                        if (contatiner != null)
                        {
                            var temp = contatiner.Content as Items;
                            Source_.PrepareConnectedAnimation("forwardAnimation_mc", temp, "CoverIMG");
                            Source_.PrepareConnectedAnimation("forwardAnimation_mcinfo", temp, "infoStackPanel");
                            // MUSIC.PrepareConnectedAnimation
                            //("forwardAnimation", temp, "MusicSourceImg");
                        }



                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.AudioPage), item_, new SuppressNavigationTransitionInfo());
                        //Frame.Navigate(typeof(Pages.AudioPage), item_in, new SuppressNavigationTransitionInfo());
                        break;
                    }
                default:
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.ItemPage), item_, new SuppressNavigationTransitionInfo());
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

            // FlyoutBase.GetAttachedFlyout((FrameworkElement)sender).Placement = FlyoutPlacementMode.Full;
            //  FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

            //location_.Locations.Remove(item_);
            // await item_.StorageFile_.DeleteAsync();
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
                    Cache_Processed.Remove(item_D);
                    Cache.Remove(item_D.StorageFile_);
                    //Cache_Processed_File.Remove(item_);
                    //Cache_Processed_Doc.Remove(item_);
                    foreach (var item in Cache_Processed_File)
                        if (item_D.Name == item.Name && item.Path == item_D.Path)
                        {
                            Cache_Processed_File.Remove(item);
                            break;
                        }
                    foreach (var item in Cache_Processed_Doc)
                        if (item_D.Name == item.Name && item.Path == item_D.Path)
                        {
                            Cache_Processed_Doc.Remove(item);
                            break;
                        }
                    foreach (var item in Cache_Processed_Media)
                        if (item_D.Name == item.Name && item.Path == item_D.Path)
                        {
                            Cache_Processed_Media.Remove(item);
                            break;
                        }
                    await item_D.StorageFile_.DeleteAsync();
                }
            }
            else
            {
                Cache_Processed.Remove(item_);
                Cache.Remove(item_.StorageFile_);
                //Cache_Processed_File.Remove(item_);
                //Cache_Processed_Doc.Remove(item_);
                foreach (var item in Cache_Processed_File)
                    if (item_.Name == item.Name && item.Path == item_.Path)
                    {
                        Cache_Processed_File.Remove(item);
                        break;
                    }
                foreach (var item in Cache_Processed_Doc)
                    if (item_.Name == item.Name && item.Path == item_.Path)
                    {
                        Cache_Processed_Doc.Remove(item);
                        break;
                    }
                foreach (var item in Cache_Processed_Media)
                    if (item_.Name == item.Name && item.Path == item_.Path)
                    {
                        Cache_Processed_Media.Remove(item);
                        break;
                    }
                await item_.StorageFile_.DeleteAsync();
            }
        }


        private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            progressBar_Main.Visibility = Visibility.Visible;
            await Refresh();
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
            REFLASH.IsEnabled = false;
            progressBar_Main.Visibility = Visibility.Visible;
            await Refresh();
        }

        private void MainFlip_Tapped(object sender, TappedRoutedEventArgs e)
        {
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            var type_in = ItemAccess.FileType_check(item_.StorageFile_);
            switch (type_in)
            {

                case "Picture":
                    {
                        ConnectedAnimation animation =
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation", MainFlip);
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.Graphics), item_, new SuppressNavigationTransitionInfo());
                        break;
                    }
                case "Video":
                    {
                        ConnectedAnimation animation =
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation", MainFlip);
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.MediaPage), item_, new SuppressNavigationTransitionInfo());
                        break;
                    }
                case "Music":
                    {
                        ConnectedAnimation animation =
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation_mc", MUSIC);
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.AudioPage), item_, new SuppressNavigationTransitionInfo());
                        /*Frame.Navigate(typeof(Pages.AudioPage), item_in, new SuppressNavigationTransitionInfo());*/
                        break;
                    }
                default:
                    {
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(Pages.ItemPage), item_, new SuppressNavigationTransitionInfo());
                        break;
                    }
            }
        }

        private void MB_1_Click(object sender, RoutedEventArgs e)
        {
            var frame_now = Window.Current.Content as Frame;
            frame_now.Navigate(typeof(Today_NewFile));
        }

        private void MB_2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search_Page_));
        }

        private void Source__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Item_index = Source_.SelectedIndex;
            //selected_item= Source_.SelectedItem;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            MainFlip.Background = new SolidColorBrush(Windows.UI.Colors.White);//爽到
            progressBar_Main.Visibility = Visibility.Collapsed;
        }

        private void Music_stack_Loaded(object sender, RoutedEventArgs e)
        {
            MUSIC.Background = new SolidColorBrush(Windows.UI.Colors.White);
            progressBar_Main.Visibility = Visibility.Collapsed;
        }

        private void Doc_stack_Loaded(object sender, RoutedEventArgs e)
        {
            DOC.Background = new SolidColorBrush(Windows.UI.Colors.White);
            progressBar_Main.Visibility = Visibility.Collapsed;
        }

        private void HideTipsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UserSettings.localSettings.Values["firstOpen998"] = "hide";
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            var Frame_page = this.Frame as Frame;
            Frame_page.Navigate(typeof(Pages.FileAdd_Page));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ProgressBar_timer.Stop();
            _timer.Stop();
        }

        private async void Source__LoadedAsync(object sender, RoutedEventArgs e)
        {
            var Animationkey = "other";
            //ContactsItem item = GetPersistedItem(); // Get persisted item
            if (item_ != null)
            {
                Source_.ScrollIntoView(item_);
                switch (FileType_check(item_.StorageFile_))
                {
                    case "Music":
                        Animationkey = "backAnimationmc";
                        break;
                    case "Picture":
                        Animationkey = "backAnimationMedia";
                        break;
                    case "Video":
                        Animationkey = "backAnimationMedia";
                        break;
                    case "Doc":
                        Animationkey = "other";
                        break;
                    default:
                        Animationkey = "other";
                        break;
                }


                ConnectedAnimation animation =
                    ConnectedAnimationService.GetForCurrentView().GetAnimation(Animationkey);
                ConnectedAnimation animation_info =
    ConnectedAnimationService.GetForCurrentView().GetAnimation(Animationkey + "_info");
                if (animation != null && povit_main.SelectedIndex == 1)
                {
                    await Source_.TryStartConnectedAnimationAsync(
                        animation, item_, "CoverIMG");
                }

                if (animation_info != null && povit_main.SelectedIndex == 1)
                {
                    await Source_.TryStartConnectedAnimationAsync(
     animation_info, item_, "infoStackPanel");
                }
            }
        }

        private async void MUSIC_LoadedAsync(object sender, RoutedEventArgs e)
        {
            if (item_ != null)
            {
                MUSIC.SelectedItem = item_;
                if (FileType_check(item_.StorageFile_) == "Music")
                {
                    ConnectedAnimation animation =
                        ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimationmc");
                    ConnectedAnimation animation_info =
                        ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimationmc_info");
                    if (animation != null && povit_main.SelectedIndex == 0)
                    {
                        animation.TryStart(MUSIC);

                    }
                    if (animation_info != null && povit_main.SelectedIndex == 0)
                    {
                        animation_info.TryStart(MUSIC);
                    }
                }
            }
        }

        private void MainFlip_Loaded(object sender, RoutedEventArgs e)
        {
            if (item_ != null)
            {
                var filetype = FileType_check(item_.StorageFile_);
                if (filetype == "Picture" || filetype == "Video")
                {
                    ConnectedAnimation animation =
                        ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimationMedia");
                    if (animation != null && povit_main.SelectedIndex == 0)
                    {
                        animation.TryStart(MainFlip);

                    }
                }
            }
        }
    }
}
