using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pino_Entrega2
{
    class ProfilePreferences:DataBase
    {
        protected List<Song> searchHistorySongs;
        protected List<Video> searchHistoryVideos;

        //No incorporaré en DisplayHistory(), pq eso debe ser parte de la clase de inputs y outpust según yo.
        public List<string> BrowserHistory() //Tengo dudas si es solo las palabra y estan haran la conexión con la canción mediante algun evento o algo que ponga play a la wea, o hacemos 2 histrial de búsqueda(cancion y vids)
        {
            
        }
        public string ProfilePreferencesSongs(List<Song> multimedia, int preferencia) //seria la lista de canciones que esuchó el usuario y el parametro del que s equiere la preferncia.
        {
            string pref = "";
            int cont = 0;
            for(int i = 0; i < multimedia.Count(); i++)
            {
                if (multimedia[i].InfoRep()[?] > cont) //Cada cancion con ese metodo entregara una lista que en una posición específica
                {
                    cont = multimedia[i].InfoRep(?);
                    pref = multimedia[i].InfoSong(?);
                }
            }
            return pref;
        }
        public List<Video> ProfilePreferencesVideos(List<Video> multimedia) //seria la lista de canciones que esuchó el usuario.
        {
            //Mismo metodo que antes
        }
        public List<Playlist> ProfilePreferencesPlaylists(List<Playlist> multimedia) //seria la lista de canciones que esuchó el usuario.
        {
            //Mismo metodo que antes
        }

    }
}
