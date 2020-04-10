using Main_Page.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Main_Page.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Search_Page_ : Page
    {
        ObservableCollection<Items> location_;
        private List<string> selectionItems = new List<string>();
        private string Need ="";
        public Search_Page_()
        {
            this.InitializeComponent();
           
        }

        private void SearchSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            var autoSuggestBox = (AutoSuggestBox)sender;
            var filtered = selectionItems.Where(p => p.StartsWith(autoSuggestBox.Text));
            autoSuggestBox.ItemsSource = filtered;
        }


        protected override  void OnNavigatedTo(NavigationEventArgs e) {
            // ItemAccess.flag = 0;
            location_ = new ObservableCollection<Items>();
            // await ItemAccess.SuggestShowItemAsync(location_);
            foreach (var inputFile in ItemAccess.Cache)
                location_.Add(new Items { Date = inputFile.DateCreated.ToString(), Type = inputFile.FileType, Name = inputFile.Name, Path = inputFile.Path, StorageFile_ = inputFile });
            foreach (var item in location_)
            {
                selectionItems.Add(item.Name);
            }
        }




        private void SearchSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var autoSuggestBox = (AutoSuggestBox)sender;
            Frame frame = Window.Current.Content as Frame;
            if (Need != "")
            {
                frame.Navigate(typeof(Pages.SearchResult), Need, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                Need = "";
               // ItemAccess.NeedNav = "Search";
            }
            else if (autoSuggestBox.Text != "")
            {
                frame.Navigate(typeof(Pages.SearchResult), autoSuggestBox.Text, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
            else
            {
                //tip
            }
           
        }

        private void SearchSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args != null)
            {
                Need = args.SelectedItem.ToString();
            }
        }
    }
}
