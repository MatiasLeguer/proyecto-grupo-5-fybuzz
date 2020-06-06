using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Modelos
{
    [Serializable]
    public class Profile : User
    {
        //ATRIBUTOS:

        protected string profileName;
        protected string profilePic;

        protected string profileType; //creador o viewer
        protected string profileMail;
        protected List<Song> playlistEnColaSongs;
        protected List<Song> playlistFavoritosSongs = new List<Song>();
        protected List<Video> playlistEnColaVideos;
        protected List<Video> playlistFavoritosVideos = new List<Video>();
        protected List<PlayList> followedPlayList = new List<PlayList>();
        protected List<PlayList> createdPlaylist = new List<PlayList>();
        protected string gender;
        protected int age;

        private List<string> persVideoPlaylist = new List<string>();
        private List<string> persSongPlaylist = new List<string>();
        //--------------------------------------------------------------------------------------------------

        //GETTERS Y SETTERS
        //--------------------------------------------------------------------------------------------------
        public string ProfileName { get => profileName; set => profileName = value; }
        public string ProfileType { get => profileType; set => profileType = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Age { get => age; set => age = value; }
        public List<Song> PlaylistFavoritosSongs { get => playlistFavoritosSongs; set => playlistFavoritosSongs = value; }
        public List<Video> PlaylistFavoritosVideos { get => playlistFavoritosVideos; set => playlistFavoritosVideos = value; }
        public List<Song> PlaylistEnColaSongs { get => playlistEnColaSongs; set => playlistEnColaSongs = value; }
        public List<Video> PlaylistEnColaVideos { get => playlistEnColaVideos; set => playlistEnColaVideos = value; }
        public List<PlayList> FollowedPlayList { get => followedPlayList; set => followedPlayList = value; }
        public List<PlayList> CreatedPlaylist { get => createdPlaylist; set => createdPlaylist = value; }
        public List<string> PersVideoPlaylist { get => persVideoPlaylist; set => persVideoPlaylist = value; }
        public List<string> PersSongPlaylist { get => persSongPlaylist; set => persSongPlaylist = value; }

        //--------------------------------------------------------------------------------------------------

        //CONSTRUCTOR
        //--------------------------------------------------------------------------------------------------
        public Profile(string pn, string pp, string pt, string pm, string pg, int pa)
        {
            profileName = pn;
            profilePic = pp;
            profileType = pt;
            profileMail = pm;
            gender = pg;
            age = pa;
        }
        //--------------------------------------------------------------------------------------------------


        //MÉTODOS

        //MÉTODOS CHANGE
        //--------------------------------------------------------------------------------------------------
        public void ChangeName(string NewName)                            //Reemplaza el nombre de perfil por un nombre nuevo.
        {
            profileName.Replace(profileName, NewName);
        }

        public void ChangeProfilePic()                                    //Reemplaza la imagen anterior por una nueva.
        {

        }

        public void ChangeProfileType()                                   //Reemplaza el tipo de perfil por uno nuevo.
        {
            if (profileType == "public")
            {
                profileType.Replace(profileType, "private");
            }
            if (profileType == "private")
            {
                profileType.Replace(profileType, "public");
            }
        }

        public void Follow()                                              //Sigue a una cuenta.
        {

        }


        //--------------------------------------------------------------------------------------------------

        //MÉTODOS DE INFORMACIÓN
        //--------------------------------------------------------------------------------------------------

        public List<string> InfoProfile()                                 //Entrega una lista de string con la información total del perfil.
        {
            List<string> InfoPro = new List<string>() { profileName, profileType, gender, age.ToString() };
            return InfoPro;
        }

        public string SearchedInfoProfile()                               //Entrega un string con la informacion básica del perfil.
        {
            return "Name: " + profileName + " " + "Gender:" + gender + " " + "Age:" + age;
        }

        public void RankingMultimedia()                                   //Entrega la información del ranking que tiene la canción.
        {

        }
        //--------------------------------------------------------------------------------------------------


        //MÉTODOS ADD/DOWNLOAD
        //--------------------------------------------------------------------------------------------------

        public void AddColaSongs(Song song)                               //Agrega la canción seleccionada a la playlist "En Cola Song"
        {
            playlistEnColaSongs.Add(song);
        }

        public void AddColaVideos(Video video)                            //Agrega el video seleccionado a la playlist "En Cola Videos"
        {
            playlistEnColaVideos.Add(video);
        }

        public void AddFavSongs(Song song)                                //Agrega la canción seleccionada a la playlist favorita de canciones.
        {
            playlistFavoritosSongs.Add(song);
        }

        public void AddFavVideos(Video video)                             //Agrega el video seleccionado a la playlist favorita de videos.
        {
            playlistFavoritosVideos.Add(video);
        }

        public void AddImage()                                            //Agrega una imagen al perfil.
        {

        }

        public void DownloadSong()                                        //Función que permite descargar la canción.
        {
            Thread.Sleep(5000);
            Console.WriteLine("Se ha descargado la cancion");
        }
        //--------------------------------------------------------------------------------------------------

        //MÉTODOS SETTINGS
        //--------------------------------------------------------------------------------------------------
        public List<string> ProfileSettings()                             //Entrega una lista ocn la información de profile para hacer display en los settings.
        {
            List<string> Settings = new List<string>() { profileName, profileType, profilePic, gender, age.ToString() };
            return Settings;
        }
        //--------------------------------------------------------------------------------------------------

        /* En videos o canciones (seria mejor en multimedia) deberia haber un metodo
           que permita sumarle uno al contador de likes de cancion o video, de lo
           contrario no se le podria sumar algo al atributo desde esta clase */


    }
}
