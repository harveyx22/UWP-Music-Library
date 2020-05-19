using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Music_Library.Model
{
    public class Song
    {
        private static int songCount = 0;

        public static int GetSongCount()
        {
            return songCount;
        }


        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AudioFile { get; set; }
        public string AlbumCoverFile { get; set; }
        public int songIDNumber { get; set; }

        public Song(string name, string artist, string album)
        {
            this.Name = name;
            this.Artist = artist;
            this.Album = album;
            this.AudioFile = $"/Assets/AudioFiles/{this.Name}.mp3";
            this.AlbumCoverFile = $"/Assets/AlbumCovers/{this.Album}.jpg";
            songCount++;
            songIDNumber = songCount;
        }                 
    }
}
