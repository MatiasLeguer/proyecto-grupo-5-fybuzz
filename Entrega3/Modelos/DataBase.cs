using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Modelos
{
    public class DataBase
    {
        protected List<string> gender;

        private List<User> userDataBase = new List<User>();

        protected List<Song> listSongsGlobal = new List<Song>();
        protected List<Video> listVideosGlobal = new List<Video>();
        protected List<PlayList> listPLsGlobal = new List<PlayList>();

        public List<Song> ListSongsGlobal
        {
            get
            {
                return listSongsGlobal;
            }
            set
            {
                if (File.Exists("AllSongs.bin") == true) listSongsGlobal = Load_Songs();
                else listSongsGlobal = new List<Song>();

            }

        }
        public List<Video> ListVideosGlobal { get => listVideosGlobal; }
        public List<PlayList> ListPLsGlobal { get => listPLsGlobal; }
        public List<User> UserDataBase { get => userDataBase; }


        //Guarda usuarios en archivos, pero necesito el diccionario.
        public void Save_Users(List<User> user)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Create, FileAccess.Write, FileShare.None); //Puse append para abrir o crear el archivo y ponerle cosas.
            formatter.Serialize(stream, user);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }

        //Muestra el archivo de todos los usuarios existentes.
        public List<User> Load_Users()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<User> userdatabase = (List<User>)formatter.Deserialize(stream);
            stream.Close();
            return userdatabase;
        }

        // Metodo para agregar un nuevo usuario, verificando ademas que no exista
        public string AddUser(User userdata, List<User> userDataBase)
        {
            string description = null;
            foreach (User value in userDataBase)
            {
                if (userdata.Username == value.Username)
                {
                    description = "El nombre de usuario especificado ya existe";
                }
                else if (userdata.Email == value.Email)
                {
                    description = "El correo ingresado ya existe";
                }
            }
            if (description == null)
            {
                userDataBase.Add(userdata);
            }
            return description;
        }

        // Metodo para obtener los datos de usr
        public List<string> GetData(string usr, List<User> userdatabase)
        {
            foreach (User user in userdatabase)
            {
                if (user.Username == usr)
                {
                    return user.AccountSettings();
                }
            }

            return new List<string>();
        }
        // Metodo para realizar el LogIn
        public User LogIn(string usrname, string password, List<User> userlist)
        {
            foreach (User user in userlist)
            {
                if (user.Username == usrname && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
        //Metodo que viene de evento que verifica y agrega elementos a las listas especificas.
        /*
        public void OnSongSent(object source, Song s)
        {
            foreach (Song song in listSongsGlobal)
            {
                if (song != s) listSongsGlobal.Add(s);
            }
        }
        public void OnVideoSent(object source, Video v)
        {
            foreach (Video vid in listVideosGlobal)
            {
                if (vid != v) listVideosGlobal.Add(v);
            }
        }
        public void OnPlaylistSent(object source, PlayList p)
        {
            foreach (PlayList pl in listPLsGlobal)
            {
                if (pl != p) listPLsGlobal.Add(p);
            }
        }
        */

        //Guarda canciones en archivos, pero necesito la lista de canciones.

        public void Save_Songs(List<Song> SongGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, SongGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<Song> Load_Songs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Song> listSongsGlobal = (List<Song>)formatter.Deserialize(stream);
            stream.Close();
            return listSongsGlobal;
        }
        public void Save_DSongs(List<Song> DSongs)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DownloadSongs.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, DSongs);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<Song> Load_DSongs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("DownloadSongs.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Song> DSongs = (List<Song>)formatter.Deserialize(stream);
            stream.Close();
            return DSongs;
        }

        public void Save_Videos(List<Video> VideosGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, VideosGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<Video> Load_Videos()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Video> listVideosGlobal = (List<Video>)formatter.Deserialize(stream);
            stream.Close();
            return listVideosGlobal;
        }
        public void Save_PLs(List<PlayList> listPLsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listPLsGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<PlayList> Load_PLs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<PlayList> listPLsGlobal = (List<PlayList>)formatter.Deserialize(stream);
            stream.Close();
            return listPLsGlobal;
        }
        public void Save_PLs_Priv(List<PlayList> listPLsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("PrivatePlayLists.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listPLsGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<PlayList> Load_PLs_Priv()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("PrivatePlayLists.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<PlayList> listPLsGlobal = (List<PlayList>)formatter.Deserialize(stream);
            stream.Close();
            return listPLsGlobal;
        }

        //Metodo para cambiar contraseña por newpsswds

        public void ChangePassword(string usr, string newpsswd)
        {
            foreach (User user in this.userDataBase)
            {
                if (user.Username == usr)
                {
                    user.Password = newpsswd;
                }
            }
        }

        public string AddMult(int typeMult, List<string> multInfo, List<Song> listSongsGlobal, List<PlayList> listPLsGlobal, List<Video> listVideosGlobal, string username, string profileUsername, string privacy, List<PlayList> listPLsPriv)
        {
            string description = null;
            List<string> infoCompare;

            switch (typeMult)
            {
                case 0: // Case 0 es cuando quiere agregar cancion
                    foreach (Song s in listSongsGlobal)
                    {
                        infoCompare = s.InfoSong();
                        if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]) && (infoCompare[2] == multInfo[2]))
                        {
                            description = "This Song is already in the system.";
                            break;
                        }
                        else if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]) && (infoCompare[2] != multInfo[2]))
                        {
                            string confirm;
                            do
                            {
                                Console.Write("This song is in other album, do you want to add it anyway? (y/n): ");
                                confirm = Console.ReadLine();
                            } while (((confirm != "y") && (confirm != "n")) || ((confirm != "Y") && (confirm != "N")));

                            if ((confirm == "n") || (confirm == "N"))
                            {
                                description = "The song was in another album and you didn't add it anyway.";
                                break;
                            }
                        }
                    }
                    if (description == null)
                    {
                        Song cancion = new Song(multInfo[0], multInfo[1], multInfo[2], multInfo[3], multInfo[4], multInfo[5], multInfo[6], double.Parse(multInfo[7]), multInfo[8], multInfo[9],multInfo[10]);
                        listSongsGlobal.Add(cancion);
                    }
                    break;


                case 1: // Case 1 es cuando quiere agregar video
                    foreach (Video v in listVideosGlobal)
                    {
                        infoCompare = v.InfoVideo();
                        if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]) && (infoCompare[2] == multInfo[2]))
                        {
                            description = "This Video is already in the system.";
                            break;
                        }

                    }

                    if (description == null)
                    {
                        Video video = new Video(multInfo[0], multInfo[1], multInfo[2], multInfo[3], multInfo[4], multInfo[5], multInfo[6], multInfo[7], Convert.ToBoolean(multInfo[8]), double.Parse(multInfo[9]), multInfo[10], multInfo[11]);
                        listVideosGlobal.Add(video);

                        //Escribir de alguna forma de que se ha agregado al sistema.
                    }
                    break;

                case 2: // Case 2 es cuando quiere agregar Playlist
                    foreach (PlayList p in listPLsGlobal)
                    {
                        infoCompare = p.InfoPlayList();
                        if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]))
                        {
                            description = "This Playlist is already in the system.";
                            break;
                        }

                    }

                    if (description == null)
                    {
                        PlayList playlist = new PlayList(multInfo[0], multInfo[1], username, profileUsername);
                        if (privacy == "n") listPLsGlobal.Add(playlist);
                        else if (privacy == "y") listPLsPriv.Add(playlist);
                    }
                    break;

                default: // Cuando escribe algo incorrecto(?)
                    description = "ERROR[!] Invalid command.";
                    break;
            }

            return description;
        }
    }
}
