using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    [Serializable]
    public class PlayList
    {
        //ATRIBUTOS:

        private List<Song> songs = new List<Song>();
        private List<Video> videos = new List<Video>();
        private string namePlayList;
        private string format;
        private int followers;
        private string creator;
        private string profileCreator;
        private Dictionary<string, List<Song>> dicCanciones;
        private Dictionary<string, List<Video>> dicVideos;
        //--------------------------------------------------------------------------------------------------

        //GETTERS Y SETTERS:
        //--------------------------------------------------------------------------------------------------
        public Dictionary<string, List<Song>> DicCanciones { get => dicCanciones; }
        public Dictionary<string, List<Video>> DicVideos { get => dicVideos; }
        public string Format { get => format; }
        public string NamePlayList { get => namePlayList; }
        public string Creator { get => creator; set => creator = value; }
        public string ProfileCreator { get => profileCreator; set => profileCreator = value; }
        public List<Song> Songs { get => songs; set => songs = value; }
        public List<Video> Videos { get => videos; set => videos = value; }
        //--------------------------------------------------------------------------------------------------

        //CONSTRUCTOR
        //--------------------------------------------------------------------------------------------------
        public PlayList(string Nombre, string Formato, string Creator, string ProfileCreator)
        {
            format = Formato;
            namePlayList = Nombre;
            this.creator = Creator;
            this.profileCreator = ProfileCreator;

        }
        //--------------------------------------------------------------------------------------------------

        //MÉTODOS:

        //MÉTODO ADD
        //--------------------------------------------------------------------------------------------------
        public void AddToPlayList()                             //Agrega las canciones y videos a un diccionario de canciones o videos.
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
        //--------------------------------------------------------------------------------------------------

        //MÉTODOs INFORMACIÓN
        //--------------------------------------------------------------------------------------------------
        public List<string> InfoPlayList()                      //Entrega una lista de string con la información de la playlist
        {
            return new List<string>() { NamePlayList, Format };
            /*string str = "";
            str += "PlayList Name: " + namePlayList + "\n";
            str += "Play List Format: " + format + "\n";

            return str;*/
        }
        public string DisplayInfoPlayList()                     //Entrega un string con la información de la playlist
        {
            return "PlayList Name: " + namePlayList + "\t Formato: " + format + "\t Creator: " + creator;
        }
        //--------------------------------------------------------------------------------------------------
    }
}
