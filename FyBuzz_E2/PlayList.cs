using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    [Serializable]
    public class PlayList
    {
        private List<Song> Songs;
        private List<Video> Videos;
        private string NamePlayList;
        private string Format;

        private Dictionary<string, List<Song>> DicCanciones;
        private Dictionary<string, List<Video>> DicVideos;

        public PlayList(string Formato, string Nombre)
        {
            Format = Formato;
            NamePlayList = Nombre;

        }


        public void AddToPlayList()
        {
            if (Format == ".mp3" || Format == ".wav")
            {
                DicCanciones.Add(NamePlayList, Songs);
            }
            if (Format == ".mp4" || Format == ".mov")
            {
                DicVideos.Add(NamePlayList, Videos);
            }
        }


        public string InfoPlayList()
        {
            string str = "";
            str += "PlayList Name: " + NamePlayList + "\n";
            str += "Play List Format: " + Format + "\n";

            return str;
        }
    }
}
