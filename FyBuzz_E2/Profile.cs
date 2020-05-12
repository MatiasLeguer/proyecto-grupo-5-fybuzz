using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FyBuzz_E2
{
    [Serializable]
    public class Profile:User
    {
        protected string profileName;
        protected string profilePic;

        protected string profileType; //creador o viewer
        protected string profileMail;
        protected List<Song> playlistEnColaSongs;
        protected List<Song> playlistFavoritosSongs;
        protected List<Video> playlistEnColaVideos;
        protected List<Video> playlistFavoritosVideos;
        protected List<PlayList> followedPlayList;
        protected string gender;
        protected int age;


        public string ProfileName { get => profileName; set => profileName = value; }
        public string ProfileType { get => profileType; set => profileType = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Age { get => age; set => age = value; }
        public List<Song> PlaylistFavoritosSongs { get => playlistFavoritosSongs; set => playlistFavoritosSongs = value; }
        public List<Song> PlaylistEnColaSongs { get => playlistEnColaSongs; set => playlistEnColaSongs = value; }
        public List<Video> PlaylistFavoritosVideos { get => playlistFavoritosVideos; set => playlistFavoritosVideos = value; }
        public List<Video> PlaylistEnColaVideos { get => playlistEnColaVideos; set => playlistEnColaVideos = value; }
        public List<PlayList> FollowedPlayList { get => followedPlayList; set => followedPlayList = value; }

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
        public string SearchedInfoProfile()
        {
            return "Name: " + profileName + " " + "Gender:" + gender + " " + "Age:" + age;
        }
        
        public void RankingMultimedia()
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
        public List<string> ProfileSettings()
        {
            List<string> Settings = new List<string>() { profileName, profileType, profilePic, gender, age.ToString()};
            return Settings;
        }
        
        // En videos o canciones (seria mejor en multimedia) deberia haber un metodo
        // que permita sumarle uno al contador de likes de cancion o video, de lo
        // contrario no se le podria sumar algo al atributo desde esta clase


    }
}
