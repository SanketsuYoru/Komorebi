﻿#pragma checksum "D:\Code\20190926\Main_Page\Pages\ItemPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E1EAF76D82C7529D5607F34B8BB4C88A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main_Page.Pages
{
    partial class ItemPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\ItemPage.xaml line 15
                {
                    this.Top_Bar = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 3: // Pages\ItemPage.xaml line 68
                {
                    global::Windows.UI.Xaml.Controls.Grid element3 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    ((global::Windows.UI.Xaml.Controls.Grid)element3).RightTapped += this.RelativePanel_RightTapped;
                }
                break;
            case 4: // Pages\ItemPage.xaml line 71
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element4 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element4).Click += this.MenuFlyoutItem_Click_Copy;
                }
                break;
            case 5: // Pages\ItemPage.xaml line 79
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element5 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element5).Click += this.MenuFlyoutItem_Click_Delete;
                }
                break;
            case 6: // Pages\ItemPage.xaml line 87
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element6 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element6).Click += this.MenuFlyoutItem_Click_Share;
                }
                break;
            case 7: // Pages\ItemPage.xaml line 95
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element7 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element7).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 8: // Pages\ItemPage.xaml line 106
                {
                    this.DeleteContentDialog = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.DeleteContentDialog).PrimaryButtonClick += this.DeleteContentDialog_PrimaryButtonClick;
                }
                break;
            case 9: // Pages\ItemPage.xaml line 128
                {
                    this.item_inside = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 10: // Pages\ItemPage.xaml line 129
                {
                    this.Name = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // Pages\ItemPage.xaml line 130
                {
                    this.Type = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Pages\ItemPage.xaml line 131
                {
                    this.Date = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Pages\ItemPage.xaml line 132
                {
                    this.Path = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // Pages\ItemPage.xaml line 133
                {
                    this.Size = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16: // Pages\ItemPage.xaml line 119
                {
                    this.img = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 17: // Pages\ItemPage.xaml line 120
                {
                    this.dialogText1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18: // Pages\ItemPage.xaml line 122
                {
                    this.dialogText2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 19: // Pages\ItemPage.xaml line 123
                {
                    this.dialogText3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 20: // Pages\ItemPage.xaml line 59
                {
                    this.OpenFolder = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.OpenFolder).Click += this.OpenFolder_Click;
                }
                break;
            case 21: // Pages\ItemPage.xaml line 62
                {
                    this.textopen = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 22: // Pages\ItemPage.xaml line 19
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element22 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element22).Click += this.MenuFlyoutItem_Click_Copy;
                }
                break;
            case 23: // Pages\ItemPage.xaml line 27
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element23 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element23).Click += this.MenuFlyoutItem_Click_Delete;
                }
                break;
            case 24: // Pages\ItemPage.xaml line 35
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element24 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element24).Click += this.MenuFlyoutItem_Click_Share;
                }
                break;
            case 25: // Pages\ItemPage.xaml line 44
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element25 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element25).Click += this.MenuFlyoutItem_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

