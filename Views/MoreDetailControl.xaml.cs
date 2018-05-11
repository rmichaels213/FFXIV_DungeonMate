using System;

using FFXIV_DungeonMate.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FFXIV_DungeonMate.Views
{
    public sealed partial class MoreDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(MoreDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public MoreDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MoreDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
