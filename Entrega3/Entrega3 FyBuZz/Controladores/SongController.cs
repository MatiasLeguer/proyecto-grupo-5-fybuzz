using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz.Controladores
{
    public class SongController
    {
        List<Song> songDatabase = new List<Song>() {new Song("Safaera","Bad Bunny", "YHLQMDLM","Rimas entertainment LLC", "Trap","20/01/2020","BB Rcds.", 4.9, "Tú tiene' un culo cabrón",".mp3"),
                                                    new Song("MAS DE UNA CITA", "Zion & Lenox", "LAS QUE NO IBAN A SALIR", "Rimas entertainment LLC", "Trap", "10/05/2020", "Z&L Rcds.", 3.5, "Se necesita, ey, más de una cita, ey", ".wav") };

        FyBuZz fyBuZz;
        DataBase dataBase = new DataBase();
        public SongController(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.CreateSongCreateSongButton_Clicked += OnCreateSongCreateSongButton_Clicked;
        }

        public void Initialize()
        {
            if (File.Exists("AllSongs.bin") != true) dataBase.Save_Songs(songDatabase);
            songDatabase = dataBase.Load_Songs();
        }

        private bool OnCreateSongCreateSongButton_Clicked(object sender, SongEventArgs e)
        {
            string date = e.DateText.ToShortDateString();
            string duration = e.DurationText.ToString();
            List<string> infoMult = new List<string> {e.NameText, e.ArtistText, e.AlbumText, e.DiscographyText, e.GenderText,date, e.StudioText, duration, e.LyricsText, e.FormatText};
            string description = dataBase.AddMult(0, infoMult, songDatabase, null, null, null, null, null, null);
            dataBase.Save_Songs(songDatabase);
            if (description == null)
            {
                return true;
            }
            else return false;
        }
    }
}
