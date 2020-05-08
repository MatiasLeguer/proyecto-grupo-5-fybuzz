using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pino_Entrega2
{
    class DataBase : IOperationMult
    {
        protected List<Song> listSongsGlobal;
        protected List<Video> listVideoGlobal; //get y un set
        protected List<Playlist> listPlaylistGlobal;
        protected List<String> gender;

        private Dictionary<int, List<string>> userDataBase;

        List<Song> listSongsGlobal = new List<Song>();
        List<Video> listSongsGlobal = new List<Video>();
        List<Playlist> listPLsGlobal = new List<Playlist>();

        //Crear la cancion o obtenerla de algun lugar;

        listSongsGlobal.Add(Song); // Esto lo tengo por mientras, en la interfaz podriamos hacer metodos como los que hace el profe en la solucion de serialización;
        listVideosGlobal.Add(Video);
        listPLGlobal.Add(Playlist);

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
            Stream stream = new FileStream("AllSongs.bin", FileMode.Create, FileAccess.Write, FileShare.None);
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
        static private void Save_PLs(List<Playlist> listPLsGlobal)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllSongs.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, listPLsGlobal);
            stream.Close();
        }
        static private List<Playlist> Load_PLs()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("AllPlayLists.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Playlist> listPLsGlobal = (List<Playlist>)formatter.Deserialize(stream);
            stream.Close();
            return listPLsGlobal;
        }
    }
}
