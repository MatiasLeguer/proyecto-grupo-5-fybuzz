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
        protected List<String> gender;

        private Dictionary<int, List<string>> userDataBase;

        protected List<Song> listSongsGlobal = new List<Song>();
        protected List<Video> listVideosGlobal = new List<Video>();
        protected List<PlayList> listPLsGlobal = new List<PlayList>();

        //Guarda usuarios en archivos, pero necesito el diccionario.
        static private void Save_Users(Dictionary<int, List<string>> userDic)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, userDic);
            stream.Close();
        }

        //Muestra el archivo de todos los usuarios existentes.
        static private Dictionary<int, List<string>> Load_Users()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllUsers.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Dictionary<int, List<string>> userDic = (Dictionary<int, List<string>>)formatter.Deserialize(stream);
            stream.Close();
            return userDic;
        }

        // Metodo para agregar un nuevo usuario, verificando ademas que no exista
        public string AddUser(List<string> data)
        {
            string description = null;
            foreach (List<string> value in this.userDataBase.Values)
            {
                if (data[0] == value[0])
                {
                    description = "El nombre de usuario especificado ya existe";
                }
                else if (data[1] == value[1])
                {
                    description = "El correo ingresado ya existe";
                }
            }
            if (description == null)
            {
                this.userDataBase.Add(userDataBase.Count + 1, data);
                Save_Users(userDataBase); // No se si hacerlo aca o afuera.
            }
            return description;
        }

        // Metodo para obtener los datos de usr
        public List<string> GetData(string usr)
        {
            foreach (List<string> user in this.userDataBase.Values)
            {
                if (user[0] == usr)
                {
                    return user;
                }
            }

            return new List<string>();
        }
        // Metodo para realizar el LogIn
        public string LogIn(string usrname, string password)
        {
            string description = null;
            foreach (List<string> user in this.userDataBase.Values)
            {
                if (user[0] == usrname && user[2] == password)
                {
                    return description;
                }
            }
            return "Usuario o contrasena incorrecta";
        }
        //Metodo que viene de evento que verifica y agrega elementos a las listas especificas.
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

        //Guarda canciones en archivos, pero necesito la lista de canciones.
        static private void Save_Songs(List<Song> listSongsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listSongsGlobal);
            stream.Close();
        }
        static private List<Song> Load_Songs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Song> listSongsGlobal = (List<Song>)formatter.Deserialize(stream);
            stream.Close();
            return listSongsGlobal;
        }
        static private void Save_Videos(List<Video> listVideosGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listVideosGlobal);
            stream.Close();
        }
        static private List<Video> Load_Videos()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllVideos.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Video> listVideosGlobal = (List<Video>)formatter.Deserialize(stream);
            stream.Close();
            return listVideosGlobal;
        }
        static private void Save_PLs(List<PlayList> listPLsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listPLsGlobal);
            stream.Close();
        }
        static private List<PlayList> Load_PLs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<PlayList> listPLsGlobal = (List<PlayList>)formatter.Deserialize(stream);
            stream.Close();
            return listPLsGlobal;
        }
    }
    //Hacer evento que envie data a player y se pueda diferenciar ahi si son canciones o videos

    //Algun método para acceder al diccionario.
}

