using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class SongEventArgs : EventArgs
    {
        public string NameText { get; set; }
        public string ArtistText { get; set; }
        public string AlbumText { get; set; }
        public string DiscographyText { get; set; }
        public string GenderText { get; set; }
        public DateTime DateText { get; set; }
        public string StudioText { get; set; }
        public double DurationText { get; set; }
        public string FormatText { get; set; }
        public string LyricsText { get; set; }
        public string FileNameText { get; set; }
        public string FileDestName { get; set; }
    }
}
