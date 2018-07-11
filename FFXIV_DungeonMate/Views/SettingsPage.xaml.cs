using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using FFXIV_DungeonMate.Helpers;
using FFXIV_DungeonMate.Services;

using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FFXIV_DungeonMate.Views
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings-codebehind.md
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page, INotifyPropertyChanged
    {
        public string DefaultDataDescription = "Revert to default data?";

        public string UserDataLocation = ApplicationData.Current.LocalFolder.ToString();

        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private bool _hideDescription;

        public bool HideDescription
        {
            get { return _hideDescription; }

            set { _hideDescription = value; }
        }

        private void AppSettings_HideDescription( object sender, RoutedEventArgs e )
        {
            RadioButton radioButton = sender as RadioButton;
            HideDescription = (string)radioButton.Content == "Show" ? true : false;
        }

        private void ButtonLoadDefaultData_Click( object sender, Windows.UI.Xaml.RoutedEventArgs e )
        {
            // Load default data
            DungeonDataService oService = new DungeonDataService();
            oService.SetDefaultData();
        }

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private async void ThemeChanged_CheckedAsync(object sender, RoutedEventArgs e)
        {
            var param = (sender as RadioButton)?.CommandParameter;

            if (param != null)
            {
                await ThemeSelectorService.SetThemeAsync((ElementTheme)param);
            }
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
    }
}
