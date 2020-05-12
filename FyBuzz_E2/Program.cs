using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FyBuzz_E2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            Menu menu = new Menu();
            List<Song> baseListSong = new List<Song>(){ new Song("Safaera","Bad Bunny", "YHLQMDLM","Rimas entertainment LLC", "Trap","20/01/2020","BB Rcds.", 4.9, true,".mp3"),
                new Song("MAS DE UNA CITA", "Zion & Lenox", "LAS QUE NO IBAN A SALIR", "Rimas entertainment LLC", "Trap", "10/05/2020", "Z&L Rcds.", 3.5, false, ".wav") };
            List<Video> baseListVideo = new List<Video>() { new Video("United","Tom Holland-Cris Pratt","Disney", "", "16:9" ,"1080x1920", "0","Movie", false,120,true,".mp4"), 
                new Video("Create a C# App from start to finish","freecodecamp.org","freecodecamp.org","12/12/2019","16:9","1080x1920","16","C# Course",true,1440,false, ".mov") };
            List<PlayList> baseListPLs = new List<PlayList>() { new PlayList("Programming hard", ".mp3"), new PlayList("TikToks that cured my depression", ".mp4") };
                
            if(File.Exists("AllSongs.bin") != true) dataBase.Save_Songs(baseListSong);
            if (File.Exists("AllVideos.bin") != true) dataBase.Save_Videos(baseListVideo);
            if (File.Exists("AllPLs.bin") != true) dataBase.Save_PLs(baseListPLs);

            User LogInUser = menu.DisplayLogin();
            
            if (LogInUser != null)
            {
                Profile profileMain = menu.DisplayProfiles(LogInUser);
                menu.DisplayStart(profileMain, LogInUser);
            }

        }
    }
}
