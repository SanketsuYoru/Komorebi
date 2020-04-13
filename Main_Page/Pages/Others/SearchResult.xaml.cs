using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class SearchResult : Page
    {
        ObservableCollection<Items> result;
        ObservableCollection<Items> result_media;
        ObservableCollection<Items> result_doc;
        ObservableCollection<Items> result_file;
        Items item_;


        public SearchResult()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            SolidColorBrush myBrush = GetBGColor();
            pivot_result.Background = myBrush;

            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;
        }


        private void ShareRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            List<StorageFile> files = new List<StorageFile>();
            files.Add(item_.StorageFile_);
            var deferral = args.Request.GetDeferral();
            DataRequest request = args.Request;
            request.Data.Properties.Title = "主题：Master向貴様共享了一个文件";
            request.Data.SetText("Noblesse oblege,今后も救世主たらんことを");
            request.Data.SetStorageItems(files);
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(item_.StorageFile_));
            request.Data.RequestedOperation = DataPackageOperation.Link;
            deferral.Complete();
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
                    default:
                        Animationkey = "other";
                        break;
                }


                ConnectedAnimation animation =
                    ConnectedAnimationService.GetForCurrentView().GetAnimation(Animationkey);
                ConnectedAnimation animation_info =
    ConnectedAnimationService.GetForCurrentView().GetAnimation(Animationkey + "_info");
                if (animation != null)
                {
                    await Source_.TryStartConnectedAnimationAsync(
                        animation, item_, "CoverIMG");
                }

                if (animation_info != null)
                {
                    await Source_.TryStartConnectedAnimationAsync(
     animation_info, item_, "infoStackPanel");
                }
            }
        }

        private async Task searchByAsync(string text_in)
        {
            try
            {
                result = new ObservableCollection<Items>();
                result_media = new ObservableCollection<Items>();
                result_doc = new ObservableCollection<Items>();
                result_file = new ObservableCollection<Items>();
                var TitleofResult = text_in;
                text_in = text_in.ToLower();
                result = new ObservableCollection<Items>();
                string temp = "";
                int flag = 0;
                // int i=0;
                result.Clear();
                result_media.Clear();
                result_doc.Clear();
                result_file.Clear();

                pivot_result.Title = text_in + " 的搜索结果 ";
                foreach (var inputFile in ItemAccess.Cache)
                {
                    var type = FileType_check(inputFile);
                    var source = inputFile.Name.ToLower().ToArray();
                    for (int i = 0; i < source.Count(); i++)
                    {
                        temp = "";
                        try
                        {
                            for (int j = i; j < (i + text_in.Count()); j++)
                            {
                                temp += source[j];
                            }
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            break;
                        }

                        if (temp == text_in)
                        {
                            pivot_result.Title = TitleofResult + " 的搜索结果 (" + (result.Count() + 1) + ")";
                            flag++;
                        }

                    }

                    if (flag >= 1)
                    {

                        result.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });

                        if (type == "Music" || type == "Picture" || type == "Video")
                        {
                            result_media.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        }
                        else if (type == "Doc")
                            result_doc.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        else
                            result_file.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }

                    flag = 0;
                }
            }
            catch (Exception e)
            {
                //TODO: 保存用户数据
                await new ContentDialog
                {
                    Title = "Error Occored",
                    Content = e.Message,
                    CloseButtonText = "Closed",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
                //NavigationMode.Back
            }
            //MUSIC.PrepareConnectedAnimation("portrait", item, "PortraitEllipse");
            //ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("forwardAnimation_mc", MUSIC);
            // ContactsListView.PrepareConnectedAnimation("portrait", item, "PortraitEllipse");
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                //NavigationMode.Back
            }
            else
            {
                var TitleofResult = e.Parameter.ToString();
                await searchByAsync(TitleofResult);
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
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            files.Add(item_.StorageFile_);
            dataPackage.SetStorageItems(files);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }

        private async void MenuFlyoutItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            item_ = (e.OriginalSource as FrameworkElement)?.DataContext as Items;
            img.Source = await ItemAccess.ThumbnailProcess(item_.StorageFile_);
            dialogText1.Text = item_.Name;
            dialogText3.Text = item_.Path;
            dialogText2.Text = item_.Date;
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
            End_sort = true;
            result.Remove(item_);
            foreach (var item in result_media)
                if (item_.Name == item.Name && item.Path == item_.Path)
                {
                    result_media.Remove(item);
                    break;
                }
            foreach (var item in result_doc)
                if (item_.Name == item.Name && item.Path == item_.Path)
                {
                    result_doc.Remove(item);
                    break;
                }
            foreach (var item in result_file)
                if (item_.Name == item.Name && item.Path == item_.Path)
                {
                    result_file.Remove(item);
                    break;
                }
            //ItemAccess.Cache.Remove(item_.StorageFile_);
            ItemAccess.Cache_flag = false;
            await item_.StorageFile_.DeleteAsync();
        }

        private void Source__ItemClick(object sender, ItemClickEventArgs e)
        {
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush myBrush = GetBGColor();
            pivot_result.Background = myBrush;
        }
    }
}
