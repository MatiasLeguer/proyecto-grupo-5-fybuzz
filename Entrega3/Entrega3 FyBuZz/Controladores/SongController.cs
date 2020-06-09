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
        List<Song> songDatabase = new List<Song>() {new Song("Safaera","Bad Bunny", "YHLQMDLM","Rimas entertainment LLC", "Trap","20/01/2020","BB Rcds.", 4.9, "Bad-Bunny-Safaera.srt",".mp3","Bad Bunny ft Jowell & Randy ft Ñengo Flow - Safaera.mp3"),
                                                    new Song("MAS DE UNA CITA", "Bad Bunny Zion & Lenox", "LAS QUE NO IBAN A SALIR", "Rimas entertainment LLC", "Trap", "10/05/2020", "Z&L Rcds.", 3.5, "Bad-Bunny-MAS-DE-UNA-CITA-feat.-Zion-y-Lennox-Las-Que-No-Iban-A-Salir.srt", ".mp3", "02-Bad-Bunny-Zion-Lennox-MÁS-DE-UNA-CITA.mp3"), 
                                                    new Song("Aplausos durante el confinamiento", "la gente", "covid 2020", "covid disc","Indie","12/05/2020","covid stdio",4, null,".wav","aplausosduranteelconfinamiento_01.wav")};

        FyBuZz fyBuZz;
        DataBase dataBase = new DataBase();
        public SongController(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.CreateSongCreateSongButton_Clicked += OnCreateSongCreateSongButton_Clicked;
            this.fyBuZz.SearchSongButton_Clicked += OnSearchSongButton_Clicked;
            this.fyBuZz.GetSongInformation += ReturnSongInfo;
            this.fyBuZz.GetAllSongInformation += ReturnAllSongsInfo;
            this.fyBuZz.PlaysSongRateButton_Clicked += RateSong;
            this.fyBuZz.SkipOrPreviousSongButton_Clicked += OnSkipOrPreviousSongButton_Clicked;
            this.fyBuZz.LikedSong_Done += LikeSong;
        }

        public void Initialize()
        {
            if (File.Exists("AllSongs.bin") != true) dataBase.Save_Songs(songDatabase);
            songDatabase = dataBase.Load_Songs();
        }

        private string RateSong(object sender, SongEventArgs e)
        {
            string result = null;
            foreach (Song song in songDatabase)
            {
                string name = e.NameText;
                string artists = e.ArtistText;
                if (e.NameText.Contains(song.Name) == true && e.ArtistText.Contains(song.Artist) == true)
                {
                    song.CantRated = song.CantRated + 1;
                    song.AccumulativeRated = song.AccumulativeRated + e.RankingText;
                    song.Ranking = song.AccumulativeRated/song.CantRated;
                    result = "Ranking summited.";
                }
            }
            dataBase.Save_Songs(songDatabase);
            return result;
        }
        private bool OnCreateSongCreateSongButton_Clicked(object sender, SongEventArgs e)
        {
            string date = e.DateText.ToShortDateString();
            string duration = e.DurationText.ToString();
            File.Copy(e.FileDestName, e.FileNameText);
            File.Copy(e.FileLyricsSource, e.LyricsText);
            List<string> infoMult = new List<string> {e.NameText, e.ArtistText, e.AlbumText, e.DiscographyText, e.GenderText,date, e.StudioText, duration, e.LyricsText, e.FormatText, e.FileNameText};
            string description = dataBase.AddMult(0, infoMult, songDatabase, null, null, null, null, null, null);
            if (description == null)
            {
                dataBase.Save_Songs(songDatabase);
                return true;
            }
            else
            {
                File.Delete(e.FileNameText);
                File.Delete(e.LyricsText);
                return false;
            }
        }
        private List<Song> OnSearchSongButton_Clicked(object sender, SongEventArgs e)
        {
            return songDatabase;
        }
        private List<string> ReturnSongInfo(object sender, SongEventArgs e)
        {
            List<string> songInfo = new List<string>();
            foreach(Song song in songDatabase)
            {

                if(e.NameText.Contains(song.Name) == true && e.ArtistText.Contains(song.Artist) == true)
                {
                    songInfo.Add(song.Album);
                    songInfo.Add(song.Artist);
                    songInfo.Add(song.Discography);
                    songInfo.Add(song.Gender);
                    songInfo.Add(song.Studio);
                    songInfo.Add(song.Lyrics);
                    songInfo.Add(song.SongFile);
                    songInfo.Add(song.Ranking.ToString());
                }
            }
            return songInfo;
        }
        private List<List<string>> ReturnAllSongsInfo(object sender, SongEventArgs e)
        {
            List<List<string>> allSongsInfo = new List<List<string>>();
            foreach(Song song in songDatabase)
            {
                allSongsInfo.Add(song.InfoSong());
            }
            return allSongsInfo;
        }


        private Song OnSkipOrPreviousSongButton_Clicked(object sender, SongEventArgs e)
        {
            if(e.SkipOrPrevious == 0) //0 = skip
            {
                if (e.OnQueueText.Count == 0)
                {
                    if (e.playlistSong == null)
                    { 
                        for (int i = e.NumberText; i < songDatabase.Count() - 1; i++)
                        {
                            if (i == songDatabase.Count() - 1) return songDatabase[0];
                            else return songDatabase[i + 1];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < e.playlistSong.Songs.Count(); i++)
                        {
                            if (((e.NameText.Contains(e.playlistSong.Songs[i].InfoSong()[0])) && (e.ArtistText.Contains(e.playlistSong.Songs[i].InfoSong()[1]))) && (i != (e.playlistSong.Songs.Count() - 1))) return e.playlistSong.Songs[i + 1];
                            else if (((e.NameText.Contains(e.playlistSong.Songs[i].InfoSong()[0])) && (e.ArtistText.Contains(e.playlistSong.Songs[i].InfoSong()[1]))) && (i == (e.playlistSong.Songs.Count() - 1))) return e.playlistSong.Songs[0];

                        }
                    }
                }
                else
                {
                    int n = 0;
                    foreach(string songfile in e.OnQueueText)
                    {
                        if (songfile.Contains(".mp4") == false && songfile.Contains(".avi") == false && songfile.Contains(".mov") == false)
                        {
                            foreach (Song song in songDatabase)
                            {
                                if (songfile.Contains(song.SongFile))
                                {
                                    e.OnQueueText.RemoveAt(n);
                                    return song;
                                }
                            }
                        }
                        n++;
                    }
                }

                return null;
            }
            else
            {
                if(e.playlistSong == null)
                {
                    for (int i = e.NumberText; i < songDatabase.Count(); i--)
                    {
                        if (i == 0) return songDatabase[songDatabase.Count() - 1];
                        else return songDatabase[i - 1];
                    }
                }
                else
                {
                    for (int i = 0; i < e.playlistSong.Songs.Count(); i++)
                    {
                        if (((e.NameText.Contains(e.playlistSong.Songs[i].InfoSong()[0])) && (e.ArtistText.Contains(e.playlistSong.Songs[i].InfoSong()[1]))) && (i != 0)) return e.playlistSong.Songs[i - 1];
                        else if (((e.NameText.Contains(e.playlistSong.Songs[i].InfoSong()[0])) && (e.ArtistText.Contains(e.playlistSong.Songs[i].InfoSong()[1]))) && (i == 0)) return e.playlistSong.Songs[(e.playlistSong.Songs.Count() - 1)];

                    }
                }

                return null;
            }
        }
        private string LikeSong(object sender, SongEventArgs e)
        {
            string result = null;
            foreach(Song song in songDatabase)
            {
                if(e.NameText.Contains(song.Name) && e.ArtistText.Contains(song.Artist))
                {
                    song.Likes = song.Likes + 1;
                    result = "Song liked";
                }
            }
            dataBase.Save_Songs(songDatabase);
            return result;
        } 

    }
}
