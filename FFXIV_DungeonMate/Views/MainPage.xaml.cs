using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIV_DungeonMate.Services;

using Windows.UI.Xaml.Controls;

namespace FFXIV_DungeonMate.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ButtonLoadDefaultData_Click( object sender, Windows.UI.Xaml.RoutedEventArgs e )
        {
            // Load default data
            DungeonDataService oService =  new DungeonDataService();
            oService.SetDefaultData();
        }
    }
}
