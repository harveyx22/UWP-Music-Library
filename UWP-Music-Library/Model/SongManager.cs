using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Music_Library.Model;

namespace UWP_Music_Library.Model
{
    public static class SongManager
    {
        public static void GetAllSongs(ObservableCollection<Song> songs)
        {
            var allSongs = getSongs();
            songs.Clear();
            allSongs.ForEach(song => songs.Add(song));
        }


        private static List<Song> getSongs()
        {
            var songs = new List<Song>();
            songs.Add(new Song("Remember Me As A Time Of Day", "Explosions in the Sky", "How Strange, Innocence"));
            songs.Add(new Song("So Long, Lonesome", "Explosions in the Sky", "All of a Sudden I Miss Everyone"));
            songs.Add(new Song("Your Hand in Mine", "Explosions in the Sky", "The Earth Is Not a Cold Dead Place"));
            return songs;
        }
    }
}
