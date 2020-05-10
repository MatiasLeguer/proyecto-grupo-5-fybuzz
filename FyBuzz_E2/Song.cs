using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Song:Multimedia
    {
        //Ver si nos faltan atributos por poner
        protected string album;
        protected string artist;
        protected string discography;
        protected string gender;
        protected string studio;
        protected bool lyrics;//

        //Ver si el constructor está adecuado, o si hay que sacar algo.
        public Song(string name, string artist, string album, string discography, string gender, string date, string studio, string ranking, double duration, bool lyrics, string format)
        {
            this.name = name;
            this.artist = artist;
            this.album = album;
            this.discography = discography;
            this.gender = gender;
            this.date = date;
            this.studio = studio;
            this.ranking = ranking;
            this.duration = duration;
            this.lyrics = lyrics;
            this.format = format;
        }

        public delegate void SendSongEventHandler(object source, Song s);
        public event SendSongEventHandler SongSent;

        //Verificar si es que funciona este evento, debido a que colocque en vez de EventArgs args, puse Song s y no se si sirve
        protected virtual void OnSongSent(Song s)
        {
            if (SongSent != null)
            {
                SongSent(this, s);
            }
        }

        public List<string> InfoSong()
        {
            return new List<string>() { name, artist, album, discography, studio, gender, ranking };
        }

        public List<int> InfoRep()
        {
            return new List<int>() { generalRep, profileRep };
        }
        public string SearchedInfoSong()
        {
            return "Cancion: " + name + "\tArtista: " + artist;
        }

        //Necesitaremos agragar más métodos??
    }
}
