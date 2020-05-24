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
            songs.Add(new Song("First Breath After Coma", "Explosions in the Sky", "The Earth Is Not a Cold Dead Place"));
            songs.Add(new Song("Yasmin the Light", "Explosions in the Sky", "Those Who Tell the Truth"));
            songs.Add(new Song("Californication", "Red Hot Chili Peppers", "Californication"));
            songs.Add(new Song("Ants Marching", "Dave Matthews Band", "Remember Two Things"));
            songs.Add(new Song("Everlong", "Foo Fighters", "The Colour and the Shape"));
            songs.Add(new Song("Alive", "Pearl Jam", "Ten"));
            songs.Add(new Song("Push", "Matchbox Twenty", "Yourself or Someone Like You"));
            songs.Add(new Song("Say it Ain't So", "Weezer", "Blue Album"));
            songs.Add(new Song("Semi-Charmed Life", "Third Eye Blind", "Third Eye Blind"));
            return songs;
        }
    }
}
