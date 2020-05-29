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
        List<PlayList> privatePlaylistsDatabase = new List<PlayList>() { new PlayList("","","","")};
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
            if (File.Exists("PrivatePlaylists.bin") != true) dataBase.Save_PLs_Priv(privatePlaylistsDatabase);
            privatePlaylistsDatabase = dataBase.Load_PLs_Priv();
        }

        private List<PlayList> OnDisplayPlaylistsGlobalPlaylist_Clicked(object sender, PlaylistEventArgs e)
        {
            return playlistDataBase;
        }

        private string CreatePlaylistButton_Clicked(object sender, PlaylistEventArgs e)
        {
            List<string> infoMult = new List<string> { e.NameText, e.FormatText, e.CreatorText, e.ProfileCreatorText};
            //e.Privacy tiene que ser "y" or "n"
            string description = dataBase.AddMult(2, infoMult, null, playlistDataBase, null, e.CreatorText, e.ProfileCreatorText,e.PrivacyText, privatePlaylistsDatabase );
            return description;
        }

        //Aqui deberia haber un metodo para ponerle play a la mult de la playlist

    }
}
