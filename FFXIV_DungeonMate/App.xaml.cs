﻿using System;
using System.Collections.ObjectModel;
using FFXIV_DungeonMate.Models;
using FFXIV_DungeonMate.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace FFXIV_DungeonMate
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private DungeonDataService _dungeonDataService { get; set; }
        public static ObservableCollection<Dungeon> DungeonData { get; set; } = new ObservableCollection<Dungeon>();

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public DungeonDataService DungeonDataService
        {
            get { return _dungeonDataService; }
            set { _dungeonDataService = value; }
        }

		public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>( CreateActivationService );
            CreateDungeonDataService();

            LoadData();
        }

        public async void LoadData()
        {
            if ( DungeonData.Count == 0 )
            {
                DungeonData?.Clear();
                DungeonData = await DungeonDataService.LoadDataAsync<ObservableCollection<Dungeon>>();
            }
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService( this, typeof( Views.MainPage ), new Lazy<UIElement>( CreateShell ) );
        }

        private void CreateDungeonDataService()
        {
            if ( DungeonDataService == null )
            {
                DungeonDataService = new DungeonDataService();
            }
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
