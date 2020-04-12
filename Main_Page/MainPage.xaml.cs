using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLibrary;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Composition;
using Main_Page.Pages;
using Windows.Storage;
using static Main_Page.Models.ItemAccess;
using static Main_Page.Models.UserSettings;
using Windows.UI.Xaml.Media.Animation;
using Windows.Globalization;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Main_Page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool Flag_Choose =false;
        public MainPage()
        {
          
            this.InitializeComponent();
           this.NavigationCacheMode = NavigationCacheMode.Required;
            //主导航处
            if (Flag_Choose == false)
            {
                contentFrame.Navigate(typeof(Pages.MAIN_Page1));
            }

            //Panel

        }


        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {




            if (NeedNav)
            {
                Frame current = Window.Current.Content as Frame;
                current.Navigate(typeof(Today_NewFile));
                NeedNav = false;
            }

            if (ApplicationLanguages.PrimaryLanguageOverride == "")
                ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            UserSettings.PrimaryLanguage = ApplicationLanguages.PrimaryLanguageOverride;
            try
            {

                if (localSettings.Values["ToggleSwitch_Menu"].ToString() == "Open")
                {
                    nvSample.PaneDisplayMode = NavigationViewPaneDisplayMode.Auto;
                }
                else
                {
                    nvSample.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftCompact;
                }
            }
            catch (Exception)
            {
               nvSample.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftCompact;
                localSettings.Values["ToggleSwitch_Menu"] = "Close";
            }

            //Background
            SolidColorBrush myBrush = GetBGColor();
            nvSample.Background = myBrush;
            if (SettingChanged) {
                contentFrame.Navigate(typeof(Pages.MAIN_Page1), new SuppressNavigationTransitionInfo());
                ItemAccess.SettingChanged = false;
            }
            if (ItemAccess.Cache_flag == false)
            {
                ItemAccess.Cache_flag = true;
                ItemAccess.Cache.Clear();
                Cache_Processed.Clear();
                Cache_Processed_Media.Clear();
                Cache_Processed_Doc.Clear();
                Cache_Processed_File.Clear();
                Cache_Recent_Media.Clear();
                Cache_Recent_Music.Clear();
                Cache_Recent_Doc.Clear();
                try
                {
                    await Refresh();
                }
                catch (Exception Excep)
                {
                    await new ContentDialog
                    {
                        Title = "发生错误",
                        Content = Excep.Message,
                        CloseButtonText = "关闭",
                        DefaultButton = ContentDialogButton.Close
                    }.ShowAsync();
                }
            }
            /*if (!Refreshing && ItemAccess.Cache.Count == 0)
            {

                contentFrame.Navigate(typeof(Pages.GUIDE_page));
            }*/


        }








        private void NvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //ItemAccess.NeedNav = "ItemInvoked";
            Flag_Choose = true;
            if (args.InvokedItem == null)
                return;

            if (args.IsSettingsInvoked)
            {
                var Frame_page = this.Frame as Frame;
                Frame_page.Navigate(typeof(Pages.FileAdd_Page));
            }

            else
            {
                // Getting the Tag from Content (args.InvokedItem is the content of NavigationViewItem)
                var navItemTag = nvSample.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(i => args.InvokedItem.Equals(i.Content))
                    .Tag.ToString();

                switch (navItemTag)
                {
                    case "Home":
                        contentFrame.Navigate(typeof(Pages.MAIN_Page1));
                        break;

                    case "Media":
                        contentFrame.Navigate(typeof(Pages.MEDIA_MAIN));
                        break;


                    case "Document":
                        contentFrame.Navigate(typeof(Pages.DOC_MAIN));
                        break;

                    case "File":
                        contentFrame.Navigate(typeof(Pages.MAIN_FILE));
                        break;

                    case "Find":
                        contentFrame.Navigate(typeof(Pages.Search_Page_));
                        break;

                }
            }


        }
    }
}
