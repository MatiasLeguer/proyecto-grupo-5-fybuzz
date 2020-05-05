using System;
using System.Collections.Generic;

namespace E2_JacoboG
{
    public class PlayList
    {
        private List<Song> Songs;
        private List<Video> Videos;
        private string NamePlayList;
        private string Format;
        
        
        

        public PlayList(string Formato, string Nombre)
        {
            Format = Formato;
            NamePlayList = Nombre;
            
        }


        public void AddToPlayList()
        {
            if (Format == "mp3")
            {
                .Add(Songs)
            }
            if (Format == "mp4")
            {
                .Add(Videos)
            }
        }


        public string InfoPlayList()
        {
            string str = "";
            str += "PlayList Name: " + NamePlayList+ "\n";
            str += "Play List Format: " + Format + "\n";
            return str;


        }


















    }
}
