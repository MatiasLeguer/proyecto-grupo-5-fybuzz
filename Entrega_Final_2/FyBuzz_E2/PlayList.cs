﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    [Serializable]
    public class PlayList
    {
        private List<Song> songs;
        private List<Video> videos;
        private string namePlayList;
        private string format;
        private int followers;

        private Dictionary<string, List<Song>> dicCanciones;
        private Dictionary<string, List<Video>> dicVideos;

        public Dictionary<string, List<Song>> DicCanciones { get => dicCanciones; }
        public Dictionary<string, List<Video>> DicVideos { get => dicVideos; }
        public string Format { get => format; }
        public string NamePlayList { get => namePlayList; }

        public PlayList(string Nombre, string Formato)
        {
            format = Formato;
            namePlayList = Nombre;

        }


        public void AddToPlayList()
        {
            if (format == ".mp3" || format == ".wav")
            {
                dicCanciones.Add(namePlayList, songs);
            }
            if (format == ".mp4" || format == ".mov")
            {
                dicVideos.Add(namePlayList, videos);
            }
        }


        public List<string> InfoPlayList()
        {
            return new List<string>() {NamePlayList, Format};
            /*string str = "";
            str += "PlayList Name: " + namePlayList + "\n";
            str += "Play List Format: " + format + "\n";

            return str;*/
        }
        public string DisplayInfoPlayList()
        {
            return "PlayList Name: " + namePlayList + "\t Formato: " + format;
        }
    }
}
