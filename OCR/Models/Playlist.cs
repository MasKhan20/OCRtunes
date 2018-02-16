using OCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Models
{
    public class Playlist
    {
        public string Title { get; set; }
        public List<Song> Songs { get; set; }

        public static List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
