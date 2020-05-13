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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Music_Library
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> Songs;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(Songs);

            var song = Songs[0];
            MyMediaElement.Source = new Uri(BaseUri, song.AudioFile);

            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            MyMediaElement.Play();            
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Collapsed;
            MyMediaElement.Pause();
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MyMediaElement.Volume = (double)VolumeSlider.Value;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
