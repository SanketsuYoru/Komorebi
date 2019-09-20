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
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using static Main_Page.Models.ItemAccess;
using Windows.Storage.Streams;
using static Main_Page.Models.UserSettings;
using Windows.UI.Xaml.Media.Animation;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchResult : Page
    {
        ObservableCollection<Items> result=new ObservableCollection<Items>();
        ObservableCollection<Items> result_media = new ObservableCollection<Items>();
        ObservableCollection<Items> result_doc = new ObservableCollection<Items>();
        ObservableCollection<Items> result_file = new ObservableCollection<Items>();
        bool search_break =false;
        Items item_;
        string text_in = "";


        public SearchResult()
        {
            this.InitializeComponent();
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           var TitleofResult= e.Parameter.ToString();
            text_in = TitleofResult.ToLower();
            result = new ObservableCollection<Items>();
            string temp = "";
            int flag = 0;
           // int i=0;
           
            pivot_result.Title = text_in+" 的搜索结果 " ;
            foreach (var inputFile in ItemAccess.Cache)
            {
                if (search_break)
                {
                    result.Clear(); 
                    result_media.Clear();
                    result_doc.Clear();
                    result_file.Clear();
                    break;
                }
                var type = FileType_check(inputFile);
                var source = inputFile.Name.ToLower().ToArray();
                for (int i = 0;i < source.Count(); i++)
                {
                    temp = "";
                    try {
                        for (int j = i; j <( i+text_in.Count()); j++)
                        {
                            temp += source[j];
                        }
                    }
                    catch (System.IndexOutOfRangeException) {
                        break;
                    }

                    if (temp == text_in)
                    {
                        pivot_result.Title = TitleofResult + " 的搜索结果 (" +( result.Count()+1)+")" ;
                        flag++;
                    }
                       
                }

                if (flag >= 1)
                {

                    result.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile });

                    if (type == "Music" || type == "Picture" || type == "Video")
                    {
                        result_media.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile });
                    }
                    else if (type == "Doc")
                        result_doc.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile });
                    else
                        result_file.Add(new Items { AccessSource = await ItemAccess.ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile });
                }
                 
                flag = 0;
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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            search_break = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush myBrush = GetBGColor();
            pivot_result.Background = myBrush;
        }
    }
 }
