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
    }


    //添加程序未来可访问列表许可
    public class GetUserPermissions
    {
        public static string mruToken { set; get; }
        public static string faToken { set; get; }
        public static async Task<string> GetAccessPermissions()
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Add to MRU with metadata (For example, a string that represents the date)
                mruToken = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(folder, currentTime.ToShortDateString());
                // Add to FA without metadata
                faToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder, currentTime.ToShortDateString());
            }
            else
            {
                return "Operation cancelled.";
            }
            return folder.Path;
        }
    }


    public static class UserSettings {
        public static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

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

    }


    public static class ItemAccess
    {
        public static bool Refreshing = false;
        public static bool Cache_flag = false;
        public static bool End_sort = false;
        public static ObservableCollection<StorageFile> Cache= new ObservableCollection<StorageFile>();
        public static ObservableCollection<Items> Cache_Processed =new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_Media = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_Doc = new ObservableCollection<Items>();
        public static ObservableCollection<Items> Cache_Processed_File = new ObservableCollection<Items>();


        public static async Task<string> SizeOfFileAsync(StorageFile inputFile) {
            string size="";
            var Origin = (await inputFile.GetBasicPropertiesAsync()).Size;
            if (Origin >= 1024 * 1024 * 1024)
                size = (Origin / 1024.0 / 1024.0/1024.0).ToString("0.00") + "GB"; 
            else if (Origin >= 1024 * 1024)
                size = (Origin / 1024.0 / 1024.0).ToString("0.00") + "MB";
            else
                size = (Origin / 1024.0).ToString("0.00") + "KB";
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

            End_sort = false;
            Cache_Processed.Clear();
            Cache_Processed_Media.Clear();
            Cache_Processed_Doc.Clear();
            Cache_Processed_File.Clear();
            Debug.WriteLine(" Sort_in");
           Debug.WriteLine(ItemAccess.Cache.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);


            var inputFiles = new ObservableCollection<StorageFile>();
            foreach (var input in ItemAccess.Cache) {
                Debug.WriteLine(" Foreach");
                Debug.WriteLine(ItemAccess.Cache.Count);
                inputFiles.Add(input);
            }
            while (inputFiles.Count != 0 && !End_sort)
            {
                Debug.WriteLine(" Sorting");
                Debug.WriteLine(ItemAccess.Cache.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
                Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);
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
                var type = inputFile.FileType.ToLower(); ;
                //source = await BitmapProcess(inputFile);//图像编码
                if (Cache_Processed.Count <= 500)
                {
                    Cache_Processed.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile ,Size = await  SizeOfFileAsync(inputFile)});
                    if (type == ".jpg" || type == ".bmp" || type == ".png" || type == ".mp4" || type == ".mp3" || type == ".mkv" || type == ".avi")
                    {
                        Cache_Processed_Media.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    }
                    else if(type == ".doc" || type == ".docx" || type == ".ppt" || type == ".pptx" || type == ".xlsx" || type == ".xls")
                        Cache_Processed_Doc.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });
                    else
                        Cache_Processed_File.Add(new Items { AccessSource = await ThumbnailProcess(inputFile), Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile, Size = await SizeOfFileAsync(inputFile) });

                }
                else
                    break;
            }
            inputFiles.Clear();
            Debug.WriteLine(" Sort_out");
            Debug.WriteLine(ItemAccess.Cache.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Media.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_Doc.Count);
            Debug.WriteLine(ItemAccess.Cache_Processed_File.Count);

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
            softwareBitmap.Dispose();//如果有机器慢的话就删除这句
            return source;
        }

        //设置缩略图选项

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
            StorageFolder inputFloder;
            ObservableCollection<StorageFile> StorageFilelist = new ObservableCollection<StorageFile>();
            inputFloder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(Token);
            // var inputFlies=await  inputFloder.GetFilesAsync();
            StorageFilelist = await retrieve_(inputFloder, StorageFilelist);
            return StorageFilelist;
        }

        public static async Task<ObservableCollection<StorageFile>> retrieve_(StorageFolder inputFloder, ObservableCollection<StorageFile> storageFiles)
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
                    await retrieve_(folder, storageFiles);
                }
            return storageFiles;
        }
        //GetitemsAsync异步获取文件
        public static List<Database> GetTokensAsync()
        {

            var ObservableAccessSource = new ObservableCollection<Items>();
            var ListofBitmap = new ObservableCollection<SoftwareBitmapSource>();
            var Tokens = new List<Database>();
            Tokens = faTokenDataAccess.GetData();//返回数据库里面所有文件访问令牌faToken : List<string>
            return Tokens;
        }




        public static async Task RecentShowItemAsync()
        {
           // ItemList.Dispose();
             Cache = new ObservableCollection<StorageFile>();
            Cache.Clear();
            var Tokens = new List<Database>();
            Tokens = GetTokensAsync();
            foreach (var Token in Tokens)
            {
                try
                {
                    var file = await GetitemsAsyncNotIndexed(Token.token);//没建立索引
                    foreach (var i in file)
                    {
                        Cache.Add(i);
                    }

                }
                catch (System.IO.FileNotFoundException)
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Remove(Token.token);
                    faTokenDataAccess.DeleteData(Token.token);
                    mruTokenDataAccess.DeleteData(Token.token);
                    continue;
                }

            }
           // await SortAsync(Cache, ItemList);

        }

    }






}
