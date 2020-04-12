using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Main_Page.Pages;
using Main_Page;
using Windows.Storage.AccessCache;
using Windows.Graphics.Imaging;
using DataAccessLibrary;
using System.Collections.ObjectModel;
using System.Threading;
using Windows.Storage.Search;
using Windows.Storage.FileProperties;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Windows.Storage.Pickers;
using Windows.Media.Core;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
using Windows.Globalization;

namespace Main_Page.Models
{


    public class Items
    {
        public SoftwareBitmapSource AccessSource { set; get; }
        public StorageFile StorageFile_ { set; get; }
        public string Date { set; get; }
        public string Type { set; get; }
        public string Name { set; get; }
        public string Path { set; get; }
        public string  Size { set; get; }
        public int Count = 0;
    }


    //添加程序未来可访问列表许可
    public class GetUserPermissions
    {
        public static string MruToken { set; get; }
        public static string FaToken { set; get; }
        public static async Task<string> GetAccessPermissions()
        {
            bool Folderflag=false;
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            foreach (var data in FaTokenDataAccess.GetData())
            {
                if (data.Folder == folder.Path)
                {
                    Folderflag = true;
                    break;
                }
            }

                if (folder != null && !Folderflag)
            {
                // Add to MRU with metadata (For example, a string that represents the date)
                MruToken = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(folder, currentTime.ToShortDateString());
                // Add to FA without metadata
                FaToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder, currentTime.ToShortDateString());
            }
            else
            {
                return "Operation cancelled.";
            }
            return folder.Path;
        }
    }


    public static class UserSettings {
        public static string PrimaryLanguage;
       public static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public static double GetVolume() {
            double Volume = 0;
            try
            {
                Volume = Convert.ToDouble((UserSettings.localSettings.Values["Volume"]).ToString());
            }
            catch (System.NullReferenceException)
            {
                UserSettings.localSettings.Values["Volume"] = "0.5";
            }

            return Volume;
        }


        public static int GetMaxNumber()
        {
            var MaxNumber = 0;
            try
            {
                MaxNumber = Convert.ToInt32((UserSettings.localSettings.Values["MaxNum"]).ToString());
            }
            catch (System.NullReferenceException)
            {
                UserSettings.localSettings.Values["MaxNum"] = "1000";
                UserSettings.localSettings.Values["Unlimited"] = false;
            }

            return MaxNumber;
        }
            public static SolidColorBrush GetBGColor()
        {
            var Apha_ = new byte();
            var Red_ = new byte();
            var Green_ = new byte();
            var Blue_ = new byte();
            try
            {
                Apha_ = Convert.ToByte(localSettings.Values["Apha_BG"].ToString());
                Red_ = Convert.ToByte(localSettings.Values["Red_BG"].ToString());
                Green_ = Convert.ToByte(localSettings.Values["Green_BG"].ToString());
                Blue_ = Convert.ToByte(localSettings.Values["Blue_BG"].ToString());
            }
            catch (System.NullReferenceException)
            {
                //提示用户可以自定义背景
                Apha_ = Convert.ToByte(255);
                Red_ = Convert.ToByte(255);
                Green_ = Convert.ToByte(237);
                Blue_ = Convert.ToByte(75);
            }
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(Apha_, Red_, Green_, Blue_));
            return myBrush;
        }
        public static SolidColorBrush GetBGColor(byte Apha)
        {
            var Red_ = new byte();
            var Green_ = new byte();
            var Blue_ = new byte();
            try
            {
                Red_ = Convert.ToByte(localSettings.Values["Red_BG"].ToString());
                Green_ = Convert.ToByte(localSettings.Values["Green_BG"].ToString());
                Blue_ = Convert.ToByte(localSettings.Values["Blue_BG"].ToString());
            }
            catch (System.NullReferenceException)
            {
                //提示用户可以自定义背景
                Red_ = Convert.ToByte(255);
                Green_ = Convert.ToByte(237);
                Blue_ = Convert.ToByte(75);
            }
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(Apha, Red_, Green_, Blue_));
            return myBrush;
        }



    }

    public static class Notifications_
    {


        public static  void  Notifications_Tocast()
        {
            string title = "查看今天的新文件";
            string content = "发现新文件";
            string view = "查看";
            string cancel = "取消";
            switch (UserSettings.PrimaryLanguage)
            {
                case "en-US":
                    {
                        title = "Today's New Files";
                        content = "Find New Files";
                        view = "View";
                        cancel = "Cancel";
                        break;
                    }
                case "ja":
                    {
                        title = "今日の新しいファイル";
                        content = "新しいファイルを発見";
                        view = "確認";
                        cancel = "キャッセル";
                        break;
                    }
                case "zh-Hans-CN":
                    {
                        title = "查看今天的新文件";
                        content = "发现新文件";
                        view = "查看";
                        cancel = "取消";
                        break;
                    }
                default:
                    break;

            }




            //string image = "https://picsum.photos/360/202?image=883";
            //string image = "Resources/BF942982C59CEBF2640D14CD6F9420CB.png";
            var image = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE3sKm8?ver=08d4&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true";
            string logo = "Resources/BF942982C59CEBF2640D14CD6F9420CB.png";

            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
        {
            new AdaptiveText()
            {
                Text = title
            },

            new AdaptiveText()
            {
                Text = content
            },

            new AdaptiveImage()
            {
                Source = image
            }
        },

                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = logo,
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    }
                }
            };
            ////

            // In a real app, these would be initialized with actual data
            int conversationId = 384928;

            // Construct the actions for the toast (inputs and buttons)
            ToastActionsCustom actions = new ToastActionsCustom(){Inputs ={},Buttons ={ new ToastButton(view, new QueryString(){{ "action", "viewImage" },{ "imageUrl", image }}.ToString())         ,new ToastButton(cancel, new QueryString(){{ "action", "cancel" },{ "conversationId", conversationId.ToString() }}.ToString())
        {
            ActivationType = ToastActivationType.Background
        }
                }
            };
            ////
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions,

                // Arguments when the user taps body of toast
                Launch = new QueryString()
    {
        { "action", "viewConversation" },
        { "conversationId", conversationId.ToString() }

    }.ToString()
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddDays(1);
            //
            toast.Tag = "18365";
            toast.Group = "wallPosts";
            //
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }


        public static class ItemAccess
    {
        
        public static string Exception_ ="";
        public static bool NeedNav = false;
        public static bool SettingChanged = false;
        public static bool Noticed = false;
        public static bool Refreshing = false;
        public static bool Cache_flag = false;
        public static bool End_sort = false;
        public static ObservableCollection<StorageFile> Cache= new ObservableCollection<StorageFile>();
        public static ObservableCollection<Items> Cache_Processed =new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_Media = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_Doc = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_File = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_Today = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Recent_Media = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Recent_Music = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Recent_Doc = new ObservableCollection<Items>();


        public static async Task Refresh() {
            Noticed = false;
            Refreshing = true;
            End_sort = true;
            ItemAccess.Cache_flag = true;
            ItemAccess.Cache.Clear();
            Cache_Processed.Clear();
            Cache_Processed_Media.Clear();
            Cache_Processed_Doc.Clear();
            Cache_Processed_File.Clear();
            Cache_Recent_Media.Clear();
            Cache_Recent_Music.Clear();
            Cache_Recent_Doc.Clear();
            Cache_Processed_Today.Clear();
            await ItemAccess.RecentShowItemAsync();
            await ItemAccess.SortAsync();
            Refreshing = false;
        }

        public static async Task<StorageFile> GetnextFileAsync(string PorN, Items source)
        {
            var temp = source;
            var filetype = FileType_check(source.StorageFile_);
            int currentIndex = 0;
            for (int i = 0; i < ItemAccess.Cache.Count; i++)
            {
                    if (source.StorageFile_.Name== ItemAccess.Cache[i].Name)
                    {
                        currentIndex = i;
                    break;
                    }
            }

            if (PorN == "Preview")
            {
                try
                {
                    for (int i = currentIndex-1; i >=0; i--)
                    {
                        if (FileType_check(ItemAccess.Cache[i]) == filetype)
                        {
                            if (source.StorageFile_.Name == ItemAccess.Cache[i].Name)
                            {
                                i++;
                                continue;
                            }
                            Debug.WriteLine("currentIndex" + currentIndex);
                            Debug.WriteLine("Next i" + i);
                            Debug.WriteLine("Pre" + ItemAccess.Cache[i].Name);
                            return ItemAccess.Cache[i];
                        }
                    }
                }
                catch (Exception e1)
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
            }
            else
            {
                try
                {
                    for (int i = currentIndex+1; i < ItemAccess.Cache.Count; i++)
                    {
                        if (FileType_check(ItemAccess.Cache[i]) == filetype)
                        {
                            if (source.StorageFile_.Name == ItemAccess.Cache[i].Name)
                            {
                                i++;
                                continue;
                            }
                            Debug.WriteLine("currentIndex" + currentIndex);
                            Debug.WriteLine("Next i" + i);
                            Debug.WriteLine("Next item" + ItemAccess.Cache[i].Name);
                            return ItemAccess.Cache[i];
                        }
                    }
                }
                catch (Exception e1)
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
            }
            return source.StorageFile_;
        }

        public static string FileType_check(StorageFile inputFile)
        {
            var type = inputFile.FileType.ToLower();
            string type_out = "";
            if (type == ".doc" || type == ".docx" || type == ".ppt" || type == ".pptx" || type == ".xlsx" || type == ".xls" || type == ".pdf" || type == ".txt")
                type_out = "Doc";
            else if (type == ".jpg" || type == ".bmp" || type == ".png" || type == ".gif" || type == ".mp4" || type == ".mp3" || type == ".mkv" || type == ".avi" || type == ".mov" || type == ".wma")
            {
                if (type == ".mp3"|| type == ".wma")
                    type_out = "Music";
                else if(type == ".jpg" || type == ".bmp" || type == ".png" || type == ".gif")
                    type_out = "Picture";
                else
                    type_out = "Video";
            }
                
            else
                type_out = "File";
            return type_out;
        }

        public static bool Today_check(DateTime  inputFile)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            if (inputFile.Day== currentTime.Day&& inputFile.Month == currentTime.Month&& inputFile.Year == currentTime.Year)
            {
                return true;
            }
            else
                return false;
        }


            public static async Task<string> SizeOfFileAsync(StorageFile inputFile) {
            string size="";
            var Origin = (await inputFile.GetBasicPropertiesAsync()).Size;
            if (Origin >= 1024 * 1024 * 1024)
                size = (Origin / 1024.0 / 1024.0/1024.0).ToString("0.00") + "GB"; 
            else if (Origin >= 1024 * 1024)
                size = (Origin / 1024.0 / 1024.0).ToString("0.00") + "MB";
            else if (Origin >= 1024)
                size = (Origin / 1024.0).ToString("0.00") + "KB";
            else
                size= Origin.ToString("0") + "B";
            return size;
        }


        public static async Task ExplorePath(StorageFolder folder, StorageFile Flie)
        {
            //System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            var t = new FolderLauncherOptions();
            t.ItemsToSelect.Add(Flie);
            await Launcher.LaunchFolderAsync(folder, t);
        }




        public static async Task SortAsync()
        {
            int MaxNumber;
            End_sort = false;
            Cache_Processed.Clear();
            Cache_Processed_Media.Clear();
            Cache_Processed_Doc.Clear();
            Cache_Processed_File.Clear();
            Cache_Recent_Media.Clear();
            Cache_Recent_Music.Clear();
            Cache_Processed_Today.Clear();
/*            Debug.WriteLine(" Sort_in");
            Debug.WriteLine(ItemAccess.Cache.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);*/
            MaxNumber = UserSettings.GetMaxNumber();
            try {
                for (int i = 0; i < ItemAccess.Cache.Count; i++)
                {
                    for (int j = i + 1; j < ItemAccess.Cache.Count; j++)
                    {
                        if (ItemAccess.Cache[i].DateCreated < ItemAccess.Cache[j].DateCreated)
                        {
                            var temp = ItemAccess.Cache[i];
                            ItemAccess.Cache[i] = ItemAccess.Cache[j];
                            ItemAccess.Cache[j] = temp;
                        }
                    }
                }
                foreach (var inputFile in ItemAccess.Cache)
                {
                    if (ItemAccess.End_sort)
                    {
                        Cache_Processed.Clear();
                        Cache_Processed_Media.Clear();
                        Cache_Processed_Doc.Clear();
                        Cache_Processed_File.Clear();
                    }

                    var type = FileType_check(inputFile);
                    var date = inputFile.DateCreated;
                    //source = await BitmapProcess(inputFile);//图像编码
                    if (Today_check(date.DateTime))
                    {
                        if (!Noticed)
                        {
                            Noticed = true;
                            Notifications_.Notifications_Tocast();
                        }
                        Cache_Processed_Today.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }
                    if (Cache_Processed.Count <= MaxNumber)
                    {
                        Cache_Processed.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        if (type == "Music" || type == "Picture" || type == "Video")
                        {
                            if (type == "Music" && Cache_Recent_Music.Count <= 15)
                                Cache_Recent_Music.Add(new Items { AccessSource = await ThumbnailProcess_HQ(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                            else if (Cache_Recent_Media.Count <= 15 && type != "Music")
                                Cache_Recent_Media.Add(new Items { AccessSource = await ThumbnailProcess_HQ(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                            Cache_Processed_Media.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        }
                        else if (type == "Doc")
                        {
                            if (Cache_Recent_Doc.Count <= 15)
                                Cache_Recent_Doc.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                            Cache_Processed_Doc.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        }
                        else
                            Cache_Processed_File.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }
                    // if (Cache_Processed_Today.Count > 0) ;
                    //Notifications_.Notifications_Tocast(Cache_Processed_Today[0].StorageFile_);
/*                    Debug.WriteLine(" Sort_out");
                    Debug.WriteLine(ItemAccess.Cache.Count);
                    Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
                    Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
                    Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);*/
                }
            }
            catch {
                
            }
            
              

        }


        public static async Task SortAsyncLowSpeed()
        {
            int MaxNumber;
            End_sort = false;
            Cache_Processed.Clear();
            Cache_Processed_Media.Clear();
            Cache_Processed_Doc.Clear();
            Cache_Processed_File.Clear();
            Cache_Recent_Media.Clear();
            Cache_Recent_Music.Clear();
            Cache_Processed_Today.Clear();
/*            Debug.WriteLine(" Sort_in");
            Debug.WriteLine(ItemAccess.Cache.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);*/
            MaxNumber = UserSettings.GetMaxNumber();
             var inputFiles = new ObservableCollection<StorageFile>();
            foreach (var input in ItemAccess.Cache) {
                Debug.WriteLine(" Foreach");
                Debug.WriteLine(ItemAccess.Cache.Count);
                inputFiles.Add(input);
            }
            while (inputFiles.Count != 0 && !End_sort)
            {
/*                Debug.WriteLine(" Sorting");
                Debug.WriteLine(ItemAccess.Cache.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);*/
                StorageFile inputFile = inputFiles.LastOrDefault();
                for (int i = inputFiles.Count - 1; i >= 0; i--)
                {
                    if (inputFiles[i].DateCreated > inputFile.DateCreated)
                    {
                        inputFile = inputFiles[i];
                    }
                }
                inputFiles.Remove(inputFile);
                if (ItemAccess.End_sort)
                {
                    Cache_Processed.Clear();
                    Cache_Processed_Media.Clear();
                    Cache_Processed_Doc.Clear();
                    Cache_Processed_File.Clear();
                }
               
                var type = FileType_check(inputFile);
                var date = inputFile.DateCreated;
                //source = await BitmapProcess(inputFile);//图像编码
                if (Today_check(date.DateTime))
                {
                    if (!Noticed)
                    {
                        Noticed = true;
                        Notifications_.Notifications_Tocast();
                    }
                    Cache_Processed_Today.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                }
                if (Cache_Processed.Count <=MaxNumber)
                {
                    Cache_Processed.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile ,Size = await  SizeOfFileAsync(inputFile)});
                    if (type=="Music" || type == "Picture" || type == "Video")
                    {
                        if (type == "Music" && Cache_Recent_Music.Count <= 15)
                            Cache_Recent_Music.Add(new Items { AccessSource = await ThumbnailProcess_HQ(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        else if (Cache_Recent_Media.Count <= 15 && type != "Music")
                            Cache_Recent_Media.Add(new Items { AccessSource = await ThumbnailProcess_HQ(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        Cache_Processed_Media.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }
                    else if (type =="Doc")
                    {
                        if (Cache_Recent_Doc.Count <= 15)
                        Cache_Recent_Doc.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                        Cache_Processed_Doc.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }
                    else
                        Cache_Processed_File.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.DisplayType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                }
            }
            inputFiles.Clear();
           // if (Cache_Processed_Today.Count > 0) ;
            //Notifications_.Notifications_Tocast(Cache_Processed_Today[0].StorageFile_);
/*            Debug.WriteLine(" Sort_out");
            Debug.WriteLine(ItemAccess.Cache.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);*/

        }

        public static MediaSource MediaProcess(StorageFile inputFile)
        {
            MediaSource _mediaSource;
            _mediaSource = MediaSource.CreateFromStorageFile(inputFile);
            return _mediaSource;
        }

        //BitmapProcess比特图处理（异步）
        public static async Task<SoftwareBitmapSource> BitmapProcess(StorageFile inputFile)
        {
            var source = new SoftwareBitmapSource();
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                var decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                decoder = null;
            }
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
    softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            await source.SetBitmapAsync(softwareBitmap);
           // softwareBitmap.Dispose();//如果有机器慢的话就删除这句
            return source;
        }

        //设置缩略图选项

        public static async Task<SoftwareBitmapSource> ThumbnailProcess_HQ(StorageFile inputFile)
        {
            const uint requestedSize = 1200;
            const ThumbnailMode thumbnailMode = ThumbnailMode.SingleItem;
            const ThumbnailOptions thumbnailOptions = ThumbnailOptions.UseCurrentScale;
            var thumbnail = await inputFile.GetThumbnailAsync(thumbnailMode, requestedSize, thumbnailOptions);
            var source = new SoftwareBitmapSource();
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = thumbnail)
            {
                // Create the decoder from the stream
                var decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                decoder = null;
                stream.Dispose();
            }
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
    softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            await source.SetBitmapAsync(softwareBitmap);
            thumbnail.Dispose();//如果有机器慢的话就删除这句
                                // softwareBitmap.Dispose();//造成不能实现UI虚拟化的元凶，代价是增加内存
            return source;
        }



        public static async Task<SoftwareBitmapSource> ThumbnailProcess(StorageFile inputFile)
        {
            const uint requestedSize = 300;
            const ThumbnailMode thumbnailMode = ThumbnailMode.SingleItem;
            const ThumbnailOptions thumbnailOptions = ThumbnailOptions.ResizeThumbnail;
            var thumbnail = await inputFile.GetThumbnailAsync(thumbnailMode, requestedSize, thumbnailOptions);
            var source = new SoftwareBitmapSource();
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = thumbnail)
            {
                // Create the decoder from the stream
                var decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                decoder = null;
                stream.Dispose();
            }
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
    softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            await source.SetBitmapAsync(softwareBitmap);
            thumbnail.Dispose();//如果有机器慢的话就删除这句
           // softwareBitmap.Dispose();//造成不能实现UI虚拟化的元凶，代价是增加内存
            return source;
        }



        public static async Task<ObservableCollection<StorageFile>> GetitemsAsyncNotIndexed(string Token)
        {
            //ItemList.Dispose();
            StorageFolder inputFloder = null;
            ObservableCollection<StorageFile> StorageFilelist = new ObservableCollection<StorageFile>();
            try
            {
                inputFloder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(Token);
                // var inputFlies=await  inputFloder.GetFilesAsync();

            }
            catch (System.ArgumentException)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Remove(Token);
                FaTokenDataAccess.DeleteData(Token);
                MruTokenDataAccess.DeleteData(Token);
                if (inputFloder != null)
                    StorageFilelist = await Retrieve_(inputFloder, StorageFilelist);
                return StorageFilelist;
            }
           if(inputFloder!=null)
            StorageFilelist = await Retrieve_(inputFloder, StorageFilelist);

            return StorageFilelist;
        }

        public static async Task<ObservableCollection<StorageFile>> Retrieve_(StorageFolder inputFloder, ObservableCollection<StorageFile> storageFiles)
        {
            //Debug.WriteLine(flag);
            foreach (var inputfile in await inputFloder.GetFilesAsync())
            {
                var type = inputfile.FileType;
              /*  if (type != ".jpg" && type != ".bmp" && type != ".png" && type != ".mp4" && type != ".mp3" && type != ".mkv")
                {
                    continue;//以后处理
                }
                else
                {*/
                    storageFiles.Add(inputfile);
              //  }
            }
                foreach (var folder in await inputFloder.GetFoldersAsync())
                {
                    await Retrieve_(folder, storageFiles);
                }
            return storageFiles;
        }
        //GetitemsAsync异步获取文件
        public static List<Database> GetTokensAsync()
        {
            var Tokens = new List<Database>();
            Tokens = FaTokenDataAccess.GetData();//返回数据库里面所有文件访问令牌faToken : List<string>
            return Tokens;
        }




        public static async Task RecentShowItemAsync()
        {
            // ItemList.Dispose();
            //Noticed = false;
            Cache = new ObservableCollection<StorageFile>();
            Cache.Clear();
            var Tokens = new List<Database>();
            Tokens = GetTokensAsync();
            foreach (var Token in Tokens)
            {
                try
                {
                    Exception_ = Token.Folder;
                    var file = await GetitemsAsyncNotIndexed(Token.Token);//没建立索引
                    foreach (var i in file)
                    {
                        Cache.Add(i);
                    }

                }
                catch (System.IO.FileNotFoundException)
                {
                    FaTokenDataAccess.DeleteData(Token.Folder);
                    MruTokenDataAccess.DeleteData(Token.Folder);
                    continue;
                }

            }
            // await SortAsync(Cache, ItemList);
          

        }

    }






}
