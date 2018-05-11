/*----------------------------------------
 * Filename:        DungeonDataService.cs
 * Created By:      Ryan C. Michaels
 * Created Date:    05/10/2018
 * Updated Date:    05/11/2018
----------------------------------------*/

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FFXIV_DungeonMate.Models;
using Windows.Storage;

namespace FFXIV_DungeonMate.Services
{
    public class DungeonDataService
    {
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// Load default Dungeon data.
        /// </summary>
        /// <returns>Returns the default dungeon data.</returns>
        public ObservableCollection<Dungeon> LoadDefaultData()
        {
            ObservableCollection<Dungeon> result = new ObservableCollection<Dungeon>();

            try
            {
                string[] res = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                
                using ( Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream( res[0] ) )
                {
                    // If there is data in the file, load it!
                    if ( stream.Length > 0 )
                    {
                        var serializer = new XmlSerializer( typeof( ObservableCollection<Dungeon> ) );
                        result = (ObservableCollection<Dungeon>)serializer.Deserialize( stream );
                    }
                }
            }
            catch ( Exception e )
            {
                Console.Write( e.Message );
            }

            return result;
        }

        /// <summary>
        /// Load default Dungeon data.
        /// </summary>
        public void SetDefaultData()
        {
            ObservableCollection<Dungeon> result = LoadDefaultData();
            App.DungeonData = result;
        }

        /// <summary>
        /// Asynchronous Load.
        /// </summary>
        /// <typeparam name="T">Type of data to save.</typeparam>
        /// <returns>Returns completed task with loaded data.</returns>
        // TODO: Move filenames to a new resource and make it type agnostic
        public async Task<T> LoadDataAsync<T>()
        {
            T result = default( T );

            StorageFile dataFile;
            IStorageItem dataItem = await localFolder.TryGetItemAsync( "dungeonDataFile.xml" );

            // If the file doesn't exist, create it!
            if ( dataItem == null )
            {
                dataFile = await localFolder.CreateFileAsync( "dungeonDataFile.xml" );
            }
            else
            {
                dataFile = dataItem as StorageFile;
            }

            // Try to load the data file
            if ( dataFile != null )
            {
                using ( Stream stream = await dataFile.OpenStreamForReadAsync() )
                {
                    // If there is data in the file, load it!
                    if ( stream.Length > 0 )
                    {
                        var serializer = new XmlSerializer( typeof( T ) );
                        result = (T)serializer.Deserialize( stream );
                    }
                    // If no data, just go grab the default data
                    else
                    {
                        ObservableCollection<Dungeon> defaultData = LoadDefaultData();
                        result = (T)Convert.ChangeType( defaultData, typeof( T ) );
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronous save.
        /// </summary>
        /// <typeparam name="T">Type of data to save.</typeparam>
        /// <param name="data">The data to save.</param>
        /// <returns>Returns a comleted task.</returns>
        public async Task SaveDataAsync<T>( T data )
        {
            // If the file doesn't exist, create it!
            StorageFile dataFile = await localFolder.CreateFileAsync( "dungeonDataFile.xml", CreationCollisionOption.ReplaceExisting );

            var serializer = new XmlSerializer( typeof( T ) );

            using ( Stream stream = await dataFile.OpenStreamForWriteAsync() )
            {
                serializer.Serialize( stream, data );
            }
        }

        /// <summary>
        /// Save a specific dungeon.
        /// </summary>
        /// <param name="dungeon"></param>
        public void SaveDungeon( Dungeon dungeon )
        {
            // Find the specific element and update it
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load( "MyXml.xml" );
                string xmlString = xmlDocument.OuterXml;

                ObservableCollection<Dungeon> localDungeons;

                using ( StringReader read = new StringReader( xmlString ) )
                {
                    Type outType = typeof( ObservableCollection<Dungeon> );

                    XmlSerializer serializerOut = new XmlSerializer( outType );
                    using ( XmlReader reader = new XmlTextReader( read ) )
                    {
                        localDungeons = (ObservableCollection<Dungeon>)serializerOut.Deserialize( reader );
                        reader.Close();
                    }

                    read.Close();
                }

                foreach ( Dungeon d in localDungeons )
                {
                    if ( d.Id == dungeon.Id )
                    {
                        d.Name = dungeon.Name;
                        d.Description = dungeon.Description;

                        d.BestTime = dungeon.BestTime;

                        d.Note1 = dungeon.Note1;
                        d.Note2 = dungeon.Note2;
                        d.Note3 = dungeon.Note3;
                        d.Note4 = dungeon.Note4;
                        d.Note5 = dungeon.Note5;
                        d.Note6 = dungeon.Note6;
                        d.Note7 = dungeon.Note7;
                        d.Note8 = dungeon.Note8;
                        d.Note9 = dungeon.Note9;
                        d.Note10 = dungeon.Note10;

                        d.Note1Name = dungeon.Note1Name;
                        d.Note2Name = dungeon.Note2Name;
                        d.Note3Name = dungeon.Note3Name;
                        d.Note4Name = dungeon.Note4Name;
                        d.Note5Name = dungeon.Note5Name;
                        d.Note6Name = dungeon.Note6Name;
                        d.Note7Name = dungeon.Note7Name;
                        d.Note8Name = dungeon.Note8Name;
                        d.Note9Name = dungeon.Note9Name;
                        d.Note10Name = dungeon.Note10Name;
                    }
                }

                using ( TextWriter writer = new StreamWriter( "MyXml.xml", false ) )
                {
                    var serializerIn = new XmlSerializer( typeof( ObservableCollection<Dungeon> ) );
                    serializerIn.Serialize( writer, localDungeons );
                }

            }
            catch ( Exception e )
            {
                Console.Write( e.Message );
            }
        }
    }
}
