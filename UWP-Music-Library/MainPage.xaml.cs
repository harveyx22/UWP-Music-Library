using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Music_Library.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.ApplicationModel;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Music_Library
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> Songs;
        private List<Song> SongsList;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(Songs);

            SongsList = Songs.ToList();
        }


        private void MusicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            MyMediaElement.Source = new Uri(BaseUri, song.AudioFile);
        }

        private void AppFunctionalitiesBottom_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YourLibrary_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LikedSongs_Click(object sender, RoutedEventArgs e)
        {

        }
  
        #region Test saving to/loading from user's local files
        private async void onStartup()
        {
            await loadSongs(Songs);            
        }

        private async Task loadSongs(ObservableCollection<Song> songs)
        {
           /* This is a path to my local computer, so pressing test button 4 will cause an error unless this
            filepath is changed to your local path with a songstorage textfile. The textfile should have the 
            format "songname;artistname;albumname;songname;artistname;albumname...". This stores the 
            info for all the songs.*/
            var file = await StorageFile.GetFileFromPathAsync(@"C:\Users\peter\Music\UWP_Music_Library\SongStorage.txt");

            if (file != null)
            {
                // Reads all contents from storage text file
                var allTextContents = await FileIO.ReadTextAsync(file);

                // Splits contents into an array based on the ";" character
                string[] allTextContentsArray = allTextContents.Split(';');

                // Steps through array and instantiates new song class with the info.
                int i = 0;
                songs.Clear();
                while (i < allTextContentsArray.Count())
                {
                    songs.Add(new Song(allTextContentsArray[i], allTextContentsArray[i + 1], allTextContentsArray[i + 2]));
                    i += 3;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private async void saveSongAwait(string name, string artist, string album)
        {
             await saveSong(name, artist, album);
        }
           
        private async Task saveSong(string name, string artist, string album)
        {
            /* This is a path to my local computer, so pressing test button 3 will cause an error unless this
            filepath is changed to your local path with a songstorage textfile. The textfile should have the 
            format "songname;artistname;albumname;songname;artistname;albumname...". This stores the 
            info for all the songs.*/
            var file = await StorageFile.GetFileFromPathAsync(@"C:\Users\peter\Music\UWP_Music_Library\SongStorage.txt");

            if (file != null)
            {
                //Adds all info from inputs to text file, splitting the info by the ";" character
                string textOutput = $";{name};{artist};{album}";
                await FileIO.AppendTextAsync(file, textOutput);
            }
            else
            {
                throw new Exception();
            }
        }

        private async void Test3_Click(object sender, RoutedEventArgs e)
        {
            // This is a test song addition. It will need to have files of the same name in the Assets folders to work
            saveSongAwait("Your Hand in Mine", "Explosions in the Sky", "The Earth Is Not a Cold Dead Place");
            await loadSongs(Songs);
        }

        private void Test4_Click(object sender, RoutedEventArgs e)
        {
            onStartup();
        }
        #endregion

    }
}
