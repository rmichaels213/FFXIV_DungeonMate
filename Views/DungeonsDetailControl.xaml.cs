/*----------------------------------------
 * Filename:        DungeonsDetailControl.xaml.cs
 * Created By:      Ryan C. Michaels
 * Created Date:    05/10/2018
 * Updated Date:    05/10/2018
----------------------------------------*/

using System;
using FFXIV_DungeonMate.Models;
using FFXIV_DungeonMate.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FFXIV_DungeonMate.Views
{
    /// <summary>
    /// Dungeon detail page.
    /// </summary>
    public sealed partial class DungeonsDetailControl : UserControl
    {
        private static Dungeon CurrentDungeon { get; set; }
        private static Dungeon LastDungeon { get; set; }

        /// <summary>
        /// The master menu item, Dungeon
        /// </summary>
        public Dungeon MasterMenuItem
        {
            get { return GetValue( MasterMenuItemProperty ) as Dungeon; }
            set { SetValue( MasterMenuItemProperty, value ); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register( "MasterMenuItem", typeof( Dungeon ), typeof( DungeonsDetailControl ), new PropertyMetadata( null, OnMasterMenuItemPropertyChanged ) );

        /// <summary>
        /// Constructor.
        /// </summary>
        public DungeonsDetailControl()
        {
            InitializeComponent();
            DisableAllContentForEditing();
        }

        /// <summary>
        /// Event handler for menu item change
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnMasterMenuItemPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var control = d as DungeonsDetailControl;
            control.ForegroundElement.ChangeView( 0, 0, 1 );

            LastDungeon = CurrentDungeon;
            CurrentDungeon = control.DataContext as Dungeon;

            control.HideEmptyTextBoxes();
        }

        /// <summary>
        /// Clicked Save button.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event arguments.</param>
        private async void ButtonSave_Click( object sender, RoutedEventArgs e )
        {
            Console.Write( "We have changed something!" );

            DungeonDataService oService = new DungeonDataService();
            //oService.SaveDungeon( CurrentDungeon );
            await oService.SaveDataAsync( App.DungeonData );
        }

        /// <summary>
        /// Iterate through all textboxes and hide those that are empty.
        /// </summary>
        private void HideEmptyTextBoxes()
        {
            foreach ( object child in PanelDungeonDetail.Children )
            {
                if ( child is StackPanel )
                {
                    StackPanel stackPanel = child as StackPanel;

                    foreach ( object innerChild in stackPanel.Children )
                    {
                        if ( innerChild is TextBox )
                        {
                            TextBox innerChildTB = innerChild as TextBox;
                            if ( innerChildTB.Text == string.Empty )
                            {
                                innerChildTB.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                innerChildTB.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Disable all content for editing.
        /// </summary>
        private void DisableAllContentForEditing()
        {
            ButtonNote1Save.Visibility = Visibility.Collapsed;
            ButtonNote2Save.Visibility = Visibility.Collapsed;
            ButtonNote3Save.Visibility = Visibility.Collapsed;
            ButtonNote4Save.Visibility = Visibility.Collapsed;
            ButtonNote5Save.Visibility = Visibility.Collapsed;
            ButtonNote6Save.Visibility = Visibility.Collapsed;
            ButtonNote7Save.Visibility = Visibility.Collapsed;
            ButtonNote8Save.Visibility = Visibility.Collapsed;
            ButtonNote9Save.Visibility = Visibility.Collapsed;
            ButtonNote10Save.Visibility = Visibility.Collapsed;

            foreach ( object child in PanelDungeonDetail.Children )
            {
                if ( child.GetType() == typeof( Grid ) )
                {
                    Grid parent = child as Grid;
                    foreach ( object innerChild in parent.Children )
                    {
                        if ( innerChild is TextBox )
                        {
                            TextBox innerChildTB = innerChild as TextBox;

                            if ( innerChildTB.Name == "NameTextBox" )
                            {
                                // We don't want to edit the Dungeon Name
                                continue;
                            }

                            innerChildTB.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    StackPanel parent = child as StackPanel;
                    foreach ( object innerChild in parent.Children )
                    {
                        if ( innerChild is TextBox )
                        {
                            TextBox innerChildTB = innerChild as TextBox;

                            if ( innerChildTB.Name == "NameTextBox" )
                            {
                                // We don't want to edit the Dungeon Name
                                continue;
                            }

                            innerChildTB.IsEnabled = false;
                        }
                    }
                }
            }

            HideEmptyTextBoxes();
        }

        /// <summary>
        /// Enable all content for editing.
        /// </summary>
        private void EnableAllContentForEditing()
        {
            ButtonNote1Save.Visibility = Visibility.Visible;
            ButtonNote2Save.Visibility = Visibility.Visible;
            ButtonNote3Save.Visibility = Visibility.Visible;
            ButtonNote4Save.Visibility = Visibility.Visible;
            ButtonNote5Save.Visibility = Visibility.Visible;
            ButtonNote6Save.Visibility = Visibility.Visible;
            ButtonNote7Save.Visibility = Visibility.Visible;
            ButtonNote8Save.Visibility = Visibility.Visible;
            ButtonNote9Save.Visibility = Visibility.Visible;
            ButtonNote10Save.Visibility = Visibility.Visible;

            foreach ( object child in PanelDungeonDetail.Children )
            {
                if ( child.GetType() == typeof( Grid ) )
                {
                    Grid parent = child as Grid;
                    foreach ( object innerChild in parent.Children )
                    {
                        if ( innerChild is TextBox )
                        {
                            TextBox innerChildTB = innerChild as TextBox;

                            if ( innerChildTB.Name == "NameTextBox" )
                            {
                                // We don't want to edit the Dungeon Name
                                continue;
                            }

                            innerChildTB.IsEnabled = true;
                            innerChildTB.Visibility = Visibility.Visible;
                        }
                    }
                }
                else
                {
                    StackPanel parent = child as StackPanel;
                    foreach ( object innerChild in parent.Children )
                    {
                        if ( innerChild is TextBox )
                        {
                            TextBox innerChildTB = innerChild as TextBox;

                            if ( innerChildTB.Name == "NameTextBox" )
                            {
                                // We don't want to edit the Dungeon Name
                                continue;
                            }

                            innerChildTB.IsEnabled = true;
                            innerChildTB.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Click edit button.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event arguments.</param>
        private async void ButtonDetailDungeonEdit_Click( object sender, RoutedEventArgs e )
        {
            var button = sender as Button;

            // Toggle the edit
            SetEdit( (string)button.Content == "Edit" );

            // We should probably save content here too...
            DungeonDataService oService = new DungeonDataService();
            //oService.SaveDungeon( CurrentDungeon );
            await oService.SaveDataAsync( App.DungeonData );
        }

        /// <summary>
        /// Explicitly set edit.
        /// </summary>
        /// <param name="shouldEdit">Flag if we should edit or not.</param>
        private void SetEdit( bool shouldEdit )
        {
            if ( shouldEdit )
            {
                // Toggle editing on
                ButtonDetailDungeonEdit.Content = "Save";
                EnableAllContentForEditing();
            }
            else
            {
                // Toggle editing off
                ButtonDetailDungeonEdit.Content = "Edit";
                DisableAllContentForEditing();   
            }
        }

        /// <summary>
        /// Catch data update and reasses hiding empty textboxes.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event arguments.</param>
        private async void BestTimeTextBox_DataContextChanged( FrameworkElement sender, DataContextChangedEventArgs args )
        {
            // If we were editing, toggle it off.
            if ( (string)ButtonDetailDungeonEdit.Content == "Save" )
            {
                SetEdit( false );

                // We should probably save content here too...
                DungeonDataService oService = new DungeonDataService();
                //oService.SaveDungeon( LastDungeon );
                await oService.SaveDataAsync( App.DungeonData );
            }

            // Reassess the empty textboxes
            HideEmptyTextBoxes();
        }

        /// <summary>
        /// Event when panel is loaded.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event arguments.</param>
        private void PanelDungeonDetail_Loaded( object sender, RoutedEventArgs e )
        {
            Console.Write( "We have been loaded!" );
        }
    }
}
