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
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Main_Page.Models;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using DataAccessLibrary;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using System.Collections.ObjectModel;
using static Main_Page.Models.UserSettings;
using Windows.System;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Globalization;
using System.Diagnostics;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FileAdd_Page : Page
    {

        private Windows.UI.Color Usercolor;
        private SolidColorBrush myBrush;
        List<string> itemsource = new List<string>();
        List<string> selectedtokens = new List<string>();
        public FileAdd_Page()
        {
            this.InitializeComponent();


            //Background
            myBrush = GetBGColor();
            myPivot.Background = myBrush;
            Button_add.Background = myBrush;
            Delete.Background = myBrush;
            Confirm.Background = myBrush;

            //Switch
            MaxNum.PlaceholderText = UserSettings.GetMaxNumber().ToString();
            Language.PlaceholderText = ApplicationLanguages.PrimaryLanguageOverride;
            combo.PlaceholderText = GetBGColor().Color.ToString();
            try {

                if (localSettings.Values["ToggleSwitch_Menu"].ToString() == "Open")
                {
                    Toggle_Menu.IsOn = true;
                }
                else
                {
                    Toggle_Menu.IsOn = false;
                }
            }
            catch (System.NullReferenceException)
            {
                Toggle_Menu.IsOn = true;
                localSettings.Values["ToggleSwitch_Menu"]= "Open";
            }

         


            //Binding Data
            itemsource = new List<string>();
            foreach (var data in FaTokenDataAccess.GetData())
            {
                itemsource.Add(data.Folder);
            }
            Output.ItemsSource = itemsource;
        }

        //保存许可路径

        private async void Button_add_Click(object sender, RoutedEventArgs e)
        {
            ItemAccess.SettingChanged = true;
            Database data_in = new Database();

            var Floder_in = await GetUserPermissions.GetAccessPermissions();

            if (Floder_in == "Operation cancelled.")
                return;
            else
                try
                {
                    ItemAccess.End_sort = true;
                    ItemAccess.Cache_flag = false;
                    ItemAccess.Cache.Clear();
                    data_in.Folder = Floder_in;
                    data_in.Token = GetUserPermissions.FaToken;
                    FaTokenDataAccess.AddData(data_in);
                    // mruTokenDataAccess.AddData(GetUserPermissions.mruToken);
                    itemsource = new List<string>();
                    foreach (var data in FaTokenDataAccess.GetData())
                    {
                        itemsource.Add(data.Folder);
                    }
                    Output.ItemsSource = itemsource;
                }
                catch (IOException File_E)
                {
                    Debug.WriteLine(File_E);
                    return;

                }


        }





        private async void Combo_DropDownClosed(object sender, object e)
        {
            ItemAccess.SettingChanged = true;
            if (combo.SelectedItem != null)
            {
                if (combo.SelectedItem ==Yellow)
                {
                    localSettings.Values["Apha_BG"] = 255;
                    localSettings.Values["Red_BG"] = 255;
                    localSettings.Values["Green_BG"] =249;
                    localSettings.Values["Blue_BG"] = 92;
                    myBrush = GetBGColor();
                    myPivot.Background = myBrush;
                    Button_add.Background = myBrush;
                    Delete.Background = myBrush;
                    Confirm.Background = myBrush;

                }
                else if (combo.SelectedItem ==White)
                {
                    localSettings.Values["Apha_BG"] = 255;
                    localSettings.Values["Red_BG"] = 255;
                    localSettings.Values["Green_BG"] =255;
                    localSettings.Values["Blue_BG"] =255;
                    myBrush = GetBGColor();
                    myPivot.Background = myBrush;
                    Button_add.Background = myBrush;
                    Delete.Background = myBrush;
                    Confirm.Background = myBrush;

                }
                else
                {
                    localSettings.Values["Apha_BG"] = 255;
                    localSettings.Values["Red_BG"] = 46;
                    localSettings.Values["Green_BG"] =46;
                    localSettings.Values["Blue_BG"] =46;
                    myBrush = GetBGColor();
                    myPivot.Background = myBrush;
                    Button_add.Background = myBrush;
                    Delete.Background = myBrush;
                    Confirm.Background = myBrush;


                }
                await Confirm_Dia.ShowAsync();

            }
        }




        private async void ConfirmColor_Click(object sender, RoutedEventArgs e)
        {
            ItemAccess.SettingChanged = true;
            Usercolor = myColorPicker.Color;
            // Save a setting locally on the device
          
            localSettings.Values["Apha_BG"] = Usercolor.A;
            localSettings.Values["Red_BG"] = Usercolor.R;
            localSettings.Values["Green_BG"] = Usercolor.G;
            localSettings.Values["Blue_BG"] = Usercolor.B;
            myBrush = GetBGColor();
            myPivot.Background = myBrush;
            Button_add.Background = myBrush;
            Delete.Background = myBrush;
            Confirm.Background = myBrush;
            await Confirm_Dia.ShowAsync();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ItemAccess.SettingChanged = true;
            ItemAccess.End_sort = true;
            foreach (var token in selectedtokens)
            FaTokenDataAccess.DeleteData(token);
            ItemAccess.Cache.Clear();
            ItemAccess.Cache_flag = false;
            itemsource = new List<string>();
            foreach (var data in FaTokenDataAccess.GetData())
            {
                itemsource.Add(data.Folder);
            }
            Output.ItemsSource = itemsource;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox cb = sender as CheckBox;
            selectedtokens.Add(cb.Content.ToString());
            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            selectedtokens.Remove(cb.Content.ToString());
        }

        private async void Email_B_Click(object sender, RoutedEventArgs e)
        {
            Windows.System.Profile.AnalyticsVersionInfo analyticsVersion = Windows.System.Profile.AnalyticsInfo.VersionInfo;
            var body = "";
            body+= "平台"+ analyticsVersion.DeviceFamily;
            body += System.Environment.NewLine;
           ulong v = ulong.Parse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);
            body+="    版本"+$"{v1}.{v2}.{v3}.{v4}";
            body += System.Environment.NewLine;
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            body += "   应用平台 " + package.Id.Architecture.ToString();
            body += System.Environment.NewLine;
            body += "   程序名称 " + "Komorenobi_UWP";
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            body += System.Environment.NewLine;
            body += "   机器制造商" + eas.SystemManufacturer;
            var address = "KomorenobiProject_2019@outlook.com";
            var subject = "反馈：";
           

           var mailto = new Uri($"mailto:{address}?subject={subject}&body={body}");
            await Launcher.LaunchUriAsync(mailto);
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ItemAccess.SettingChanged = true;
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    localSettings.Values["ToggleSwitch_Menu"] = "Open";
                }
                else
                {
                    localSettings.Values["ToggleSwitch_Menu"] = "Close";
                }
            }
           

        }

        private async void ComboBox_DropDownClosed(object sender, object e)
        {
            if (MaxNum.SelectedItem != null)
            {
                ItemAccess.SettingChanged = true;
                switch ((MaxNum.SelectedItem).ToString())
                {
                    case "30":
                        {
                            UserSettings.localSettings.Values["MaxNum"] = "30";
                            UserSettings.localSettings.Values["Unlimited"] = false;
                            break;
                        }
                    case "50":
                        {
                            UserSettings.localSettings.Values["MaxNum"] = "50";
                            UserSettings.localSettings.Values["Unlimited"] = false;
                            break;
                        }
                    case "200":
                        {
                            UserSettings.localSettings.Values["MaxNum"] = "200";
                            UserSettings.localSettings.Values["Unlimited"] = false;
                            break;
                        }
                    case "500":
                        {
                            UserSettings.localSettings.Values["MaxNum"] = "500";
                            UserSettings.localSettings.Values["Unlimited"] = false;
                            break;
                        }
                    case "1000":
                        {
                            UserSettings.localSettings.Values["MaxNum"] = "1000";
                            UserSettings.localSettings.Values["Unlimited"] = false;
                            break;
                        }
                }
                await Confirm_Dia.ShowAsync();
                ItemAccess.End_sort = true;
                ItemAccess.Cache_flag = false;
                ItemAccess.Cache.Clear();
            }

        }

        private async void Language_DropDownClosed(object sender, object e)
        {
            if (Language.SelectedItem!=null)
            {
                ItemAccess.SettingChanged = true;
                var temp_string = Language.SelectedItem.ToString();
                localSettings.Values["Language"] = temp_string;

                switch (temp_string)
                {
                    case "en-US English":
                        {
                            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
                            break;
                        }
                    case "ja-JP 日本語":
                        {
                            ApplicationLanguages.PrimaryLanguageOverride = "ja-JP";
                            break;
                        }
                    case "zh-CN 简体中文":
                        {
                            ApplicationLanguages.PrimaryLanguageOverride = "zh-CN";
                            break;
                        }
                    default:
                        break;

                }
                await Confirm_Dia.ShowAsync();
            }
        }

        private void Thispage_Loaded(object sender, RoutedEventArgs e)
        {
            ItemAccess.SettingChanged = true;
        }
    }
}
