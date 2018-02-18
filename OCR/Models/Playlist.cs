using OCR.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Models
{
    public class Playlist
    {
        public Playlist()
        {
            this.Songs = new ObservableCollection<Song>();
        }

        public string Name { get; set; }
        public ObservableCollection<Song> Songs { get; set; }

        //public static List<Playlist> Playlists { get; set; } = new List<Playlist>();
        public static ObservableCollection<Playlist> Playlists { get; set; } = new ObservableCollection<Playlist>();
    }
}
