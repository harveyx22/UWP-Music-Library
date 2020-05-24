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
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

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
        private List<Song> SongQueue;

        // private MediaPlayer mediaPlayer;
        private MediaPlaybackList mediaPlaybackList;       

        public MainPage()
        {
            this.InitializeComponent();

            Songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(Songs);
            
            SongsList = Songs.ToList();

            mediaPlaybackList = new MediaPlaybackList();

            // Initialize mediaplayer with playbacklist of all songs.
            initializeMediaPlayer();
            mediaPlaybackList.CurrentItemChanged += MediaPlaybackList_CurrentItemChanged;
        }

        private void initializeMediaPlayer()
        {
            // Iterate through songlist, adding each to playbacklist.
            foreach (Song music in SongsList)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(BaseUri, music.AudioFile)));
                mediaPlaybackList.Items.Add(mediaPlaybackItem);
            }

            mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;

            MyMediaPlayer.Source = mediaPlaybackList;

            SongQueue = new List<Song>();

            foreach (Song s in SongsList)
            {
                SongQueue.Add(s);
            }

        }

        private void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            var task = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                int index = (int)sender.CurrentItemIndex;
                UpdateUI();
            });
        }

        private void UpdateUI()
        {
            var newSongIndex = (int)mediaPlaybackList.CurrentItemIndex;

            if (newSongIndex > 0)
            {
                Song newSong = SongQueue[newSongIndex];
                CurrentAlbumCover.Source = new BitmapImage(new Uri(BaseUri, newSong.AlbumCoverFile));
                CurrentSong.Text = newSong.Name;
                CurrentArtist.Text = newSong.Artist;
            }           
        }

        private void MusicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Obtain song object chosen from list.
            var song = (Song)e.ClickedItem;

            // Find index of song in song list.
            int clickedIndex = SongsList.FindIndex(s => s.Name == song.Name);

            // Get spliced list of chosen song to end of all songs list, and add it to songqueue along with beginning of all songs list up to the spliced list. 
            var filteredSongs = SongsList.Skip(clickedIndex);
            var allSongs = filteredSongs.ToList();

            SongQueue.Clear();

            foreach (Song s in allSongs)
            {
                SongQueue.Add(s);
            }

            foreach (Song s in SongsList.GetRange(0, clickedIndex))
            {
                SongQueue.Add(s);
            }        

            mediaPlaybackList.Items.Clear();

            // Add each song in new list to playbacklist
            foreach (Song music in SongQueue)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(BaseUri, music.AudioFile)));
                mediaPlaybackList.Items.Add(mediaPlaybackItem);
            }

            mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;

            MyMediaPlayer.Source = mediaPlaybackList;
            MyMediaPlayer.AutoPlay = true;

            CurrentAlbumCover.Source = new BitmapImage(new Uri(BaseUri, song.AlbumCoverFile));
            CurrentSong.Text = song.Name;
            CurrentArtist.Text = song.Artist;
        }

        private void SortBySong_Click(object sender, RoutedEventArgs e)
        {
            MusicListView.ItemsSource = Songs.OrderBy(song => song.Name).ToList();
            SongsList = SongsList.OrderBy(song => song.Name).ToList();
        }

        private void SortByAlbum_Click(object sender, RoutedEventArgs e)
        {
            MusicListView.ItemsSource = Songs.OrderBy(song => song.Album).ToList();
            SongsList = SongsList.OrderBy(song => song.Album).ToList();
        }

        private void SortByArtist_Click(object sender, RoutedEventArgs e)
        {
            MusicListView.ItemsSource = Songs.OrderBy(song => song.Artist).ToList();
            SongsList = SongsList.OrderBy(song => song.Artist).ToList();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.MovePrevious();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlaybackList.MoveNext();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MusicListView.ItemsSource = Songs.OrderBy(song => song.Name).ToList();
            SongsList = SongsList.OrderBy(song => song.Name).ToList();
        }

        #region Not Implemented 

        private void AppFunctionalitiesBottom_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void YourLibrary_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LikedSongs_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void PlayPlaylist_ButtonClick(object sender, RoutedEventArgs e)
        {

            foreach (Song music in SongsList)
            {
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(BaseUri, music.AudioFile)));
                mediaPlaybackList.Items.Add(mediaPlaybackItem);
            }

            mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;

            MyMediaPlayer.Source = mediaPlaybackList;
            MyMediaPlayer.AutoPlay = true;
        }
        #endregion 
    }
}
