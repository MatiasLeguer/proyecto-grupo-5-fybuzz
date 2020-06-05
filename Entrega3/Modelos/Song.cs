using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Modelos
{
    [Serializable]
    public class Song : Multimedia
    {

        //ATRIBUTOS:
        /* Ver si nos faltan atributos por poner */

        SoundPlayer player = new SoundPlayer();

        //Ver si nos faltan atributos por poner

        protected string album;
        protected string artist;
        protected string discography;
        protected string gender;
        protected string studio;
        protected string lyrics;

        private string songFile;
        public string Lyrics { get => lyrics; }
        public string SongFile { get => songFile; set => songFile = value; }
        public string Format { get => format; }
        public string Album { get; set; }
        public string Artist { get => artist; set => artist = value; }
        public string Discography { get; set; }
        public string Gender { get; set; }
        public string Studio { get; set; }

        //Ver si el constructor está adecuado, o si hay que sacar algo.
        public Song(string name, string artist, string album, string discography, string gender, string date, string studio, double duration, string lyrics, string format, string songFile)

        {
            this.name = name;
            this.artist = artist;
            this.album = album;
            this.discography = discography;
            this.gender = gender;
            this.date = date;
            this.studio = studio;
            this.ranking = 0;
            this.duration = duration;
            this.lyrics = lyrics;
            this.format = format;
            this.SongFile = songFile;
        }
        //--------------------------------------------------------------------------------------------------

        //MÉTODOS:

        //MÉTODOS DE INFORMACIÓN
        //--------------------------------------------------------------------------------------------------
        public List<string> InfoSong()                     //Entrega una lista de strings con la información de la canción
        {
            return new List<string>() { name, artist, album, discography, studio, gender, ranking.ToString(), songFile };
        }

        public string DisplayInfoSong()                    //Entrega un string con la información de la canción
        {
            return "Name: " + name + "\tArtist: " + artist + "\nAlbum: " + album + "\tDiscography: " + discography + "\nStudio: " + studio + "\tGender: " + gender + "\nRanking: " + ranking;
        }

        public List<int> InfoRep()                         //Entrega una lista de int con la reproducción general y del perfil.
        {
            return new List<int>() { generalRep, profileRep };
        }
        public string SearchedInfoSong()                   //Entrega un string con la información de la canción buscada.
        {
            return "Song: " + name + ": Artist: " + artist;
        }
        //--------------------------------------------------------------------------------------------------


        /* Necesitaremos agragar más métodos?? */
    }
}


//ESTABA EN LA LINEA 48
/*
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
*/
