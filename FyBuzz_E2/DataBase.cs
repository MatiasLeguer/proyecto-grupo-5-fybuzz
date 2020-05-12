using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;

namespace FyBuzz_E2
{
    public class DataBase
    {
        protected List<string> gender;

        private Dictionary<int, User> userDataBase=  new Dictionary<int, User>();

        protected List<Song> listSongsGlobal = new List<Song>();
        protected List<Video> listVideosGlobal = new List<Video>();
        protected List<PlayList> listPLsGlobal = new List<PlayList>();

        public List<Song> ListSongsGlobal{ get => listSongsGlobal; }
        public List<Video> ListVideosGlobal { get => listVideosGlobal; }
        public List<PlayList> ListPLsGlobal { get => listPLsGlobal; }


        //Guarda usuarios en archivos, pero necesito el diccionario.
        public void Save_Users(User user)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Append, FileAccess.Write, FileShare.None); //Puse append para abrir o crear el archivo y ponerle cosas.
            formatter.Serialize(stream, user);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }

        //Muestra el archivo de todos los usuarios existentes.
        public Dictionary<int, User> Load_Users()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Dictionary<int, User> userdatabase = new Dictionary<int, User>();
            int lines = File.ReadAllLines("AllUsers.bin").Length;
            string sep;
            for (int i = 0; i < (lines- 1)/19; i++)
            {
                userdatabase.Add(i, (User)formatter.Deserialize(stream));
                sep = (string)formatter.Deserialize(stream);
            }
            stream.Close();
            return userdatabase;
        }

        // Metodo para agregar un nuevo usuario, verificando ademas que no exista
        public string AddUser(User userdata)
        {
            string description = null;
            // No funciona revisar los usuarios del archivo AllUsers.bin
            if (File.Exists("AllUsers.bin") == true)
            {
                Dictionary<int, User> userdic = Load_Users();
                foreach (User value in userdic.Values)
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
                    this.userDataBase.Add(userDataBase.Count + 1, userdata);
                    Save_Users(userdata);
                }
            }
            else
            {
                foreach (User value in userDataBase.Values)
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
                    this.userDataBase.Add(userDataBase.Count + 1, userdata);
                    Save_Users(userdata);
                }
            }
            return description;
        }

        // Metodo para obtener los datos de usr
        public List<string> GetData(User usr)
        {
            foreach (User user in this.userDataBase.Values)
            {
                if (user.Username == usr.Username)
                {
                    return user.AccountSettings();
                }
            }

            return new List<string>();
        }
        // Metodo para realizar el LogIn
        public User LogIn(string usrname, string password)
        {
            Dictionary<int, User> userdic = Load_Users();
            //Aqui revisar el archivo, no el diccionario en el programa en si.
            foreach (User user in userdic.Values)
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

        static private void Save_Songs(Song SongGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Append, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, SongGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<Song> Load_Songs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Song> listSongsGlobal = new List<Song>();
            int lines = File.ReadAllLines("AllSongs.bin").Length;
            string sep;
            for (int i = 0; i < (lines - 1)/5; i++)
            {
                listSongsGlobal.Add((Song)formatter.Deserialize(stream));
                sep = (string)formatter.Deserialize(stream);
            }
            stream.Close();
            return listSongsGlobal;
        }

        static private void Save_Videos(Video VideosGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Append, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, VideosGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<Video> Load_Videos()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Video> listVideosGlobal = new List<Video>();
            int lines = File.ReadAllLines("AllVideos.bin").Length;
            string sep;
            for (int i = 0; i < (lines - 1)/7; i++)
            {
                listVideosGlobal.Add((Video)formatter.Deserialize(stream));
                sep = (string)formatter.Deserialize(stream);
            }
            stream.Close();
            return listVideosGlobal;
        }
        static private void Save_PLs(PlayList listPLsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Append, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listPLsGlobal);
            formatter.Serialize(stream, "\n");
            stream.Close();
        }
        public List<PlayList> Load_PLs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<PlayList> listPLsGlobal = new List<PlayList>();
            int lines = File.ReadAllLines("AllPlayLists.bin").Length;
            string sep;
            for (int i = 0; i < (lines - 1) / 4; i++)
            {
                listPLsGlobal.Add((PlayList)formatter.Deserialize(stream));
                sep = (string)formatter.Deserialize(stream);
            }
            stream.Close();
            return listPLsGlobal;
        }
        
        //Metodo para cambiar contraseña por newpsswds

        public void ChangePassword(string usr, string newpsswd)
        {
            foreach (User user in this.userDataBase.Values)
            {
                if (user.Username == usr)
                {
                    user.Password = newpsswd;
                }
            }
        }

        public string AddMult(int typeMult, List<string> multInfo)
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
                            description = "Esta cancion ya fue agregada al sistema.";
                            break;
                        }
                        else if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]) && (infoCompare[2] != multInfo[2]))
                        {
                            string confirm;
                            do
                            {
                                Console.Write("Esta cancion se encuentra en otro album, quiere agregarla de todas formas? (y/n): ");
                                confirm = Console.ReadLine();
                            } while ((confirm != "y" || confirm != "Y") || (confirm != "n" || confirm != "N"));

                            if ((confirm == "n" || confirm == "N"))
                            {
                                description = "La canción ya se encontraba en otro album y se decidió no agregarla";
                                break;
                            }
                        }
                    }

                    if(description == null)
                    {
                        Song cancion = new Song(multInfo[0], multInfo[1], multInfo[2], multInfo[3], multInfo[4], multInfo[5], multInfo[6], double.Parse(multInfo[7]), Convert.ToBoolean(multInfo[8]), multInfo[9]);
                        listSongsGlobal.Add(cancion);
                        Save_Songs(cancion);
                    }
                    break;


                case 1: // Case 1 es cuando quiere agregar video
                    foreach (Video v in listVideosGlobal)
                    {
                        infoCompare = v.InfoVideo();
                        if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]) && (infoCompare[2] == multInfo[2]))
                        {
                            description = "Este video ya fue agregado al sistema.";
                            break;
                        }

                    }

                    if (description == null)
                    {
                        Video video = new Video(multInfo[0], multInfo[1], multInfo[2], multInfo[3], int.Parse(multInfo[4]), multInfo[5], multInfo[6], multInfo[7], Convert.ToBoolean(multInfo[8]), double.Parse(multInfo[9]), Convert.ToBoolean(multInfo[10]), multInfo[11]);
                        listVideosGlobal.Add(video);
                        Save_Videos(video);
                        //Escribir de alguna forma de que se ha agregado al sistema.
                    }
                    break;

                case 2: // Case 2 es cuando quiere agregar Playlist
                    foreach (PlayList p in listPLsGlobal)
                    {
                        infoCompare = p.InfoPlayList();
                        if ((infoCompare[0] == multInfo[0]) && (infoCompare[1] == multInfo[1]))
                        {
                            description = "Esta Playlist ya fue agregada al sistema.";
                            break;
                        }
 
                    }

                    if (description == null)
                    {
                        PlayList playlist = new PlayList(multInfo[0], multInfo[1]);
                        listPLsGlobal.Add(playlist);
                        Save_PLs(playlist);
                    }
                    break;

                default: // Cuando escribe algo incorrecto(?)
                    description = "Ingresó una opción que no existe";
                    break;
            }
            return description;
        }
    }



    //Hacer evento que envie data a player y se pueda diferenciar ahi si son canciones o videos

    //Algun método para acceder al diccionario.



}

