using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_Entrega2
{
    public class Profile : User
    {
        private string profileName;
        private string profilePic;
        private string profileType;
        private string profileMail;
        private List<Song> playlistEnColaSongs;
        private List<Song> playlistFavoritosSongs;
        private List<Video> playlistEnColaVideos;
        private List<Video> playlistFavoritosVideos;
        private string gender;
        private int age;


        public string ProfileName { get => profileName; set => profileName = value; }
        public string ProfileType { get => profileType; set => profileType = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Age { get => age; set => age = value; }

        public Profile(string pn, string pp, string pt, string pm, string pg, int pa)
        {
            profileName = pn;
            profilePic = pp;
            profileType = pt;
            profileMail = pm;
            gender = pg;
            age = pa;
        }

        public void ChangeName(string NewName)
        {
            profileName.Replace(profileName, NewName);
        }
        public void ChangeProfilePic()
        {

        }
        public void ChangeProfileType()
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
        public void Follow()
        {

        }
        public List<string> InfoProfile()
        {
            List<string> InfoPro = new List<string>() { profileName, profileType, gender, age.ToString() };
            return InfoPro;
        }

        public int RankingMultimedia()
        {

        }
        public void AddColaSongs(Song song)
        {
            playlistEnColaSongs.Add(song);
        }
        public void AddColaVideos(Video video)
        {
            playlistEnColaVideos.Add(video);
        }
        public void AddFavSongs(Song song)
        {
            playlistFavoritosSongs.Add(song);
        }
        public void AddFavVideos(Video video)
        {
            playlistFavoritosVideos.Add(video);
        }
        public void AddImage()
        {

        }
        public void DownloadSong()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Se ha descargado la cancion");
        }
        public void LikeSong(Song song)
        {
            // Conexion con multimedia / Likes +1
            song.Likes();

        }
        public void LikeVideo(Video video)
        {
            // Conexion con multimedia / Likes +1
            video.Likes();

        }

        // En videos o canciones (seria mejor en multimedia) deberia haber un metodo
        // que permita sumarle uno al contador de likes de cancion o video, de lo
        // contrario no se le podria sumar algo al atributo desde esta clase











    }
}
