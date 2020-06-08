using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class PlaylistEventArgs : EventArgs
    {
        public bool PrivacyText { get; set; }
        public string NameText { get; set; }
        public string FormatText { get; set; }
        public User CreatorText { get; set; }
        public Profile ProfileCreatorText { get; set; }
        public List<Song> SongDataBaseText { get; set; }
        public List<Video> videoDataBaseText { get; set; }
        public string RestultText { get; set; }
        public int ChoosenIndex { get; set; }
        public string SearchedPlaylistNameText { get; set; }
        public Song SongText { get; set; }
        public Video VideoText { get; set; }
    }
}