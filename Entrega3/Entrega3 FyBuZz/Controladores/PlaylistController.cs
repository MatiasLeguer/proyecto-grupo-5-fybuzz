using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.Controladores
{
    public class PlaylistController
    {
        List<PlayList> playlistDataBase = new List<PlayList>() { new PlayList("Programming hard", ".wav","FyBuZz", "FyBuZz"),
                                                                 new PlayList("FyBuZz Global Songs",".mp3","FyBuZz","FyBuZz"),
                                                                 new PlayList("FyBuZz Global Videos",".mp4","FyBuZz","FyBuZz")};
        DataBase dataBase = new DataBase();
        FyBuZz fyBuZz;


        public PlaylistController()
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.DisplayPlaylistsGlobalPlaylist_Clicked += OnDisplayPlaylistsGlobalPlaylist_Clicked;
        }
        public void Initialize()
        {
            if (File.Exists("AllPlaylists.bin") != true) dataBase.Save_PLs(playlistDataBase);
            playlistDataBase = dataBase.Load_PLs();
        }

        private List<PlayList> OnDisplayPlaylistsGlobalPlaylist_Clicked(object sender, PlaylistEventArgs e)
        {
            return playlistDataBase;
        }

        //Aqui deberia haber un metodo para ponerle play a la mult de la playlist

    }
}