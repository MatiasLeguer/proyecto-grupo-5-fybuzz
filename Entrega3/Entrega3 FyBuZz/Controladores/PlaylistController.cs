using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public PlaylistController(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.DisplayPlaylistsGlobalPlaylist_Clicked += OnDisplayPlaylistsGlobalPlaylist_Clicked;
            this.fyBuZz.CreatePlaylistCreatePlaylistButton_Clicked += CreatePlaylistButton_Clicked;
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
            List<string> infoMult = new List<string> { e.NameText, e.FormatText};

            string privacy = null;
            if(e.PrivacyText == true)
            {
                privacy = "y";
            }
            else
            {
                privacy = "n";
            }

            string description = dataBase.AddMult(2, infoMult, null, playlistDataBase, null, e.CreatorText.Username, e.ProfileCreatorText.ProfileName, privacy, privatePlaylistsDatabase );
            foreach (PlayList playList in playlistDataBase)
            {
                if (playList.Creator == e.CreatorText.Username && playList.ProfileCreator == e.ProfileCreatorText.ProfileName)
                {
                    e.ProfileCreatorText.CreatedPlaylist.Add(playList);
                    e.CreatorText.ProfilePlaylists.Add(playList);
                }
            }
            foreach (PlayList playList in privatePlaylistsDatabase)
            {
                if (playList.Creator == e.CreatorText.Username && playList.ProfileCreator == e.ProfileCreatorText.ProfileName)
                {
                    e.ProfileCreatorText.CreatedPlaylist.Add(playList);
                    //usr.ProfilePlaylists.Add(playList); Si la PL es privada no se agrega al usuario, por lo tanto no se puede seguir.
                }
            }
            dataBase.Save_PLs(playlistDataBase);
            dataBase.Save_PLs_Priv(privatePlaylistsDatabase);
            return description;
        }

        //Aqui deberia haber un metodo para ponerle play a la mult de la playlist

    }
}