﻿#pragma checksum "D:\Code\20190930\Main_Page\Pages\DOC_MAIN.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B19A1856B7D1432C36226F906A2954F5"
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
    partial class DOC_MAIN : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Image_Source(global::Windows.UI.Xaml.Controls.Image obj, global::Windows.UI.Xaml.Media.ImageSource value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), targetNullValue);
                }
                obj.Source = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class DOC_MAIN_obj8_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IDOC_MAIN_Bindings
        {
            private global::Main_Page.Models.Items dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj8;
            private global::Windows.UI.Xaml.Controls.Image obj16;
            private global::Windows.UI.Xaml.Controls.TextBlock obj17;
            private global::Windows.UI.Xaml.Controls.TextBlock obj18;
            private global::Windows.UI.Xaml.Controls.TextBlock obj19;
            private global::Windows.UI.Xaml.Controls.TextBlock obj20;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj16SourceDisabled = false;
            private static bool isobj17TextDisabled = false;
            private static bool isobj18TextDisabled = false;
            private static bool isobj19TextDisabled = false;
            private static bool isobj20TextDisabled = false;

            public DOC_MAIN_obj8_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 95 && columnNumber == 78)
                {
                    isobj16SourceDisabled = true;
                }
                else if (lineNumber == 96 && columnNumber == 72)
                {
                    isobj17TextDisabled = true;
                }
                else if (lineNumber == 97 && columnNumber == 67)
                {
                    isobj18TextDisabled = true;
                }
                else if (lineNumber == 98 && columnNumber == 67)
                {
                    isobj19TextDisabled = true;
                }
                else if (lineNumber == 99 && columnNumber == 67)
                {
                    isobj20TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 8: // Pages\DOC_MAIN.xaml line 47
                        this.obj8 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.StackPanel)target);
                        break;
                    case 16: // Pages\DOC_MAIN.xaml line 95
                        this.obj16 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    case 17: // Pages\DOC_MAIN.xaml line 96
                        this.obj17 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 18: // Pages\DOC_MAIN.xaml line 97
                        this.obj18 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 19: // Pages\DOC_MAIN.xaml line 98
                        this.obj19 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 20: // Pages\DOC_MAIN.xaml line 99
                        this.obj20 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = 1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj8.Target as global::Windows.UI.Xaml.Controls.StackPanel).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                    case 1:
                        global::Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj17);
                        global::Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj18);
                        global::Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj19);
                        global::Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj20);
                        nextPhase = 2;
                        break;
                    case 2:
                        global::Windows.UI.Xaml.Markup.XamlBindingHelper.ResumeRendering(this.obj16);
                        nextPhase = -1;
                        break;
                }
                this.Update_((global::Main_Page.Models.Items) item, 1 << phase);
            }

            public void Recycle()
            {
                global::Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj17);
                global::Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj18);
                global::Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj19);
                global::Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj20);
                global::Windows.UI.Xaml.Markup.XamlBindingHelper.SuspendRendering(this.obj16);
            }

            // IDOC_MAIN_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Main_Page.Models.Items)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Main_Page.Models.Items obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0) | (1 << 1))) != 0)
                    {
                        this.Update_Name(obj.Name, phase);
                        this.Update_Date(obj.Date, phase);
                        this.Update_Type(obj.Type, phase);
                        this.Update_Size(obj.Size, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0) | (1 << 2))) != 0)
                    {
                        this.Update_AccessSource(obj.AccessSource, phase);
                    }
                }
            }
            private void Update_AccessSource(global::Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource obj, int phase)
            {
                if ((phase & ((1 << 2) | NOT_PHASED )) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 95
                    if (!isobj16SourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Image_Source(this.obj16, obj, null);
                    }
                }
            }
            private void Update_Name(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 1) | NOT_PHASED )) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 96
                    if (!isobj17TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj17, obj, null);
                    }
                }
            }
            private void Update_Date(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 1) | NOT_PHASED )) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 97
                    if (!isobj18TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj18, obj, null);
                    }
                }
            }
            private void Update_Type(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 1) | NOT_PHASED )) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 98
                    if (!isobj19TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj19, obj, null);
                    }
                }
            }
            private void Update_Size(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 1) | NOT_PHASED )) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 99
                    if (!isobj20TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj20, obj, null);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class DOC_MAIN_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IDOC_MAIN_Bindings
        {
            private global::Main_Page.Pages.DOC_MAIN dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.GridView obj6;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj6ItemsSourceDisabled = false;

            private DOC_MAIN_obj1_BindingsTracking bindingsTracking;

            public DOC_MAIN_obj1_Bindings()
            {
                this.bindingsTracking = new DOC_MAIN_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 39 && columnNumber == 19)
                {
                    isobj6ItemsSourceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 6: // Pages\DOC_MAIN.xaml line 37
                        this.obj6 = (global::Windows.UI.Xaml.Controls.GridView)target;
                        this.bindingsTracking.RegisterTwoWayListener_6(this.obj6);
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IDOC_MAIN_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Main_Page.Pages.DOC_MAIN)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Main_Page.Pages.DOC_MAIN obj, int phase)
            {
                this.Update_Main_Page_Models_ItemAccess_Cache_Processed_Doc(global::Main_Page.Models.ItemAccess.Cache_Processed_Doc, phase);
            }
            private void Update_Main_Page_Models_ItemAccess_Cache_Processed_Doc(global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items> obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_Main_Page_Models_ItemAccess_Cache_Processed_Doc(obj);
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Pages\DOC_MAIN.xaml line 37
                    if (!isobj6ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj6, obj, null);
                    }
                }
            }
            private void UpdateTwoWay_6_ItemsSource()
            {
                if (this.initialized)
                {
                    global::Main_Page.Models.ItemAccess.Cache_Processed_Doc = (global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items>)this.obj6.ItemsSource;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class DOC_MAIN_obj1_BindingsTracking
            {
                private global::System.WeakReference<DOC_MAIN_obj1_Bindings> weakRefToBindingObj; 

                public DOC_MAIN_obj1_BindingsTracking(DOC_MAIN_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<DOC_MAIN_obj1_Bindings>(obj);
                }

                public DOC_MAIN_obj1_Bindings TryGetBindingObject()
                {
                    DOC_MAIN_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_Main_Page_Models_ItemAccess_Cache_Processed_Doc(null);
                }

                public void PropertyChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    DOC_MAIN_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    DOC_MAIN_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items>;
                    }
                }
                private global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items> cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc = null;
                public void UpdateChildListeners_Main_Page_Models_ItemAccess_Cache_Processed_Doc(global::System.Collections.ObjectModel.ObservableCollection<global::Main_Page.Models.Items> obj)
                {
                    if (obj != cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc)
                    {
                        if (cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc).PropertyChanged -= PropertyChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc).CollectionChanged -= CollectionChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc;
                            cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc = null;
                        }
                        if (obj != null)
                        {
                            cache_Main_Page_Models_ItemAccess_Cache_Processed_Doc = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)obj).CollectionChanged += CollectionChanged_Main_Page_Models_ItemAccess_Cache_Processed_Doc;
                        }
                    }
                }
                public void RegisterTwoWayListener_6(global::Windows.UI.Xaml.Controls.GridView sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.ItemsControl.ItemsSourceProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_6_ItemsSource();
                        }
                    });
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Pages\DOC_MAIN.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Pages\DOC_MAIN.xaml line 13
                {
                    this.DeleteContentDialog = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)this.DeleteContentDialog).PrimaryButtonClick += this.DeleteContentDialog_PrimaryButtonClick;
                }
                break;
            case 3: // Pages\DOC_MAIN.xaml line 34
                {
                    this.progressBar_Main = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 4: // Pages\DOC_MAIN.xaml line 35
                {
                    this.hold_text = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Pages\DOC_MAIN.xaml line 36
                {
                    this.RefreshContainer = (global::Windows.UI.Xaml.Controls.RefreshContainer)(target);
                    ((global::Windows.UI.Xaml.Controls.RefreshContainer)this.RefreshContainer).RefreshRequested += this.RefreshContainer_RefreshRequested;
                }
                break;
            case 6: // Pages\DOC_MAIN.xaml line 37
                {
                    this.Source_ = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.Source_).ItemClick += this.Source__ItemClick;
                    ((global::Windows.UI.Xaml.Controls.GridView)this.Source_).SelectionChanged += this.Source__SelectionChanged;
                }
                break;
            case 8: // Pages\DOC_MAIN.xaml line 47
                {
                    global::Windows.UI.Xaml.Controls.StackPanel element8 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                    ((global::Windows.UI.Xaml.Controls.StackPanel)element8).RightTapped += this.Source__RightTapped;
                    ((global::Windows.UI.Xaml.Controls.StackPanel)element8).Loaded += this.Adap_stackpanel_Loaded;
                }
                break;
            case 9: // Pages\DOC_MAIN.xaml line 49
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyout element9 = (global::Windows.UI.Xaml.Controls.MenuFlyout)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyout)element9).Opened += this.MenuFlyout_Opened;
                }
                break;
            case 10: // Pages\DOC_MAIN.xaml line 50
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element10 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element10).Click += this.MenuFlyoutItem_Click_Copy;
                }
                break;
            case 11: // Pages\DOC_MAIN.xaml line 58
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element11 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element11).Click += this.MenuFlyoutItem_Click_Delete;
                }
                break;
            case 12: // Pages\DOC_MAIN.xaml line 66
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element12 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element12).Click += this.MenuFlyoutItem_Click_Share;
                }
                break;
            case 13: // Pages\DOC_MAIN.xaml line 74
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element13 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element13).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 14: // Pages\DOC_MAIN.xaml line 83
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element14 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element14).Click += this.Reflash_Click;
                }
                break;
            case 15: // Pages\DOC_MAIN.xaml line 88
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element15 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element15).Click += this.MultipleSelect_Click;
                }
                break;
            case 22: // Pages\DOC_MAIN.xaml line 26
                {
                    this.img = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 23: // Pages\DOC_MAIN.xaml line 27
                {
                    this.dialogText1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 24: // Pages\DOC_MAIN.xaml line 29
                {
                    this.dialogText2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 25: // Pages\DOC_MAIN.xaml line 30
                {
                    this.dialogText3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
            switch(connectionId)
            {
            case 1: // Pages\DOC_MAIN.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    DOC_MAIN_obj1_Bindings bindings = new DOC_MAIN_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            case 8: // Pages\DOC_MAIN.xaml line 47
                {                    
                    global::Windows.UI.Xaml.Controls.StackPanel element8 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    DOC_MAIN_obj8_Bindings bindings = new DOC_MAIN_obj8_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element8.DataContext);
                    element8.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element8, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element8, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

