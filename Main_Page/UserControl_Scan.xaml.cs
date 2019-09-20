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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Main_Page
{
    public sealed partial class UserControl_Scan : UserControl
    {
        public UserControl_Scan()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = Textbox_Search.Text;




            //SomeThing to do
            Frame searchframe = Window.Current.Content as Frame;
            searchframe.Navigate(typeof(SearchResultPage), Textbox_Search.Text);

        }
    }
}
