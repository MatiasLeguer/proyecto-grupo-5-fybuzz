using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_Entrega2
{
    public class ProfilePreferences:DataBase
    {
        protected List<Song> searchHistorySongs;
        protected List<Video> searchHistoryVideos;

        public List<Song> BrowserHistorySongs(Song multimedia) //Tengo dudas si es solo las palabra y estan haran la conexión con la canción mediante algun evento o algo que ponga play a la wea, o hacemos 2 histrial de búsqueda(cancion y vids)
        {
            searchHistorySongs.Add(multimedia); //atributo de profilepreference tal vez se podria hacer un evento que agregue canciones
            return searchHistorySongs;

        }
        public List<Video> BrowserHistoryVideos(Video multimedia)
        {
            // Una vez que busca el archivo multimedia y lo igualaré a una variable de tipo string que sera el metodo InfoSong o InfoVideo dependiendo su formato.

            searchHistoryVideos.Add(multimedia); //atributo profilepreference tal vez se podria hacer un evento que agregue videos
            return searchHistoryVideos;

        }

        public string ProfilePreferencesSongs(List<Song> multimedia, int preferencia) //seria la lista de canciones que esuchó el usuario y el parametro del que s equiere la preferncia.
        {
            List<Song> pref = new List<Song>();
            int cont = 0;
            for (int i = 0; i < multimedia.Count(); i++)
            {
                if (multimedia[i].InfoRep()[1] > cont) //Cada cancion con ese metodo entregara una lista que en una posición específica
                {
                    cont = multimedia[i].InfoRep();
                    pref = multimedia[i].InfoSong(); //Recordar que infosongs es una lista, luego la preferencia seria la posicion de la palabra que se busca.
                }
            }
            return pref[preferencia]; //Seria la preferencia (string) que se estaba buscando.
        }
        public string ProfilePreferencesVideos(List<Video> multimedia, int preferencia) //Entrega la preferencia según el parametro que se quiera.
        {
            List<Video> pref = new List<Video>();
            int cont = 0;
            for (int i = 0; i < multimedia.Count(); i++)
            {
                if (multimedia[i].InfoRep() > cont) //Cada cancion con ese metodo entregara una lista que en una posición específica
                {
                    cont = multimedia[i].InfoRep();
                    pref = multimedia[i].InfoVideo(); //Recordar que infosongs es una lista, luego la preferencia seria la posicion de la palabra que se busca.
                }
            }
            return pref[preferencia];
        }
        //Podria sacar...
        //public string ProfilePreferencesPlaylists(List<Playlist> multimedia, int preferencia)
        //{ 
        //mismo metodo que arriba, pero tengo que cachar los diccionarios de jacobo
        //}
    }
}
