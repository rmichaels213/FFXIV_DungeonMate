using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using FFXIV_DungeonMate.Models;
using FFXIV_DungeonMate.Services;

using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace FFXIV_DungeonMate.Views
{
    public sealed partial class DungeonsPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Dungeon> MyDungeonData { get => App.DungeonData; set => MyDungeonData = value; }

        private Dungeon _selected;

        public Dungeon Selected
        {
            get { return _selected; }
            set { Set( ref _selected, value ); }
        }

        public DungeonsPage()
        {
            InitializeComponent();

            //LoadData();

            // Load default data
            //DungeonData = oService.LoadDefaultData() as ObservableCollection<Dungeon>;

            // Make a copy of the default data right now
            //oService.SaveData( DungeonData );

            // Load back to make sure everything works correctly
            //DungeonData = oService.LoadData() as ObservableCollection<Dungeon>;

            Loaded += DungeonsPage_Loaded;
        }

        public async void LoadData()
        {
            if ( App.DungeonData.Count == 0 )
            {
                DungeonDataService oService = new DungeonDataService();

                App.DungeonData?.Clear();
                App.DungeonData = await oService.LoadDataAsync<ObservableCollection<Dungeon>>();
            }           
        }

        private void DungeonsPage_Loaded( object sender, RoutedEventArgs e )
        {
            if ( App.DungeonData != null )
            {
                if ( App.DungeonData.Count > 0 && MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both )
                {
                    Selected = App.DungeonData.First();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>( ref T storage, T value, [CallerMemberName]string propertyName = null )
        {
            if ( Equals( storage, value ) )
            {
                return;
            }

            storage = value;
            OnPropertyChanged( propertyName );
        }

        private void OnPropertyChanged( string propertyName ) => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }
}
