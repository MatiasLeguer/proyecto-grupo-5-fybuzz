using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Player
    {
        //Hay que usar eventos que vengan desde menú y triguereen los métodos de aca con las canciones de Database.
        // hacer un get a la duracion y al nombre


        public void PlaySong(int cont, Song song, PlayList p, DataBase d, string name, User user, Profile profile)
        {
            Song s = song;
            while (true)
            {
                double seconds = 60 * s.Duration; //duration viene de multimedia y es la cantidad de minutos como decimal, hay que establecer una relacion
                Console.WriteLine("Playing: " + s.Name); //name tambien viene de multimedia.
                string verif = "-1";
                int condition = -1;
                while (cont < seconds)
                {
                    if (user.AdsOn == true && cont % 26 == 0) 
                    {
                        List<string> AdsList = new List<string>(){ "Are you a standar user? Pfff try upgrading to premium and stop getting Ads!!"
                                    , "Keep Calm Leguer's Toilet Paper doesn't run out of stock in this Quarentine, come and buy it!!"
                                    , "Do you want to be good at videogames? try watching Juan Jacobo's tip and tricks videos."
                                    , "Are you into Podcasts? COMING SOON Diego's Podcast 'FyBuZz in tha house' "};
                        Random random = new Random();
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("ADS-ON");
                        Console.WriteLine(AdsList[random.Next(4)]);
                        Console.WriteLine("-------------------------------------------------------");
                        for (int i = 0; i < 1; i++)
                        {
                            Thread.Sleep(5000);
                        }
                    }
                    if (cont % 10 == 0)
                    {
                        Console.WriteLine("To pause press 0.");
                        Console.WriteLine("To skip press 1.");
                        Console.WriteLine("To preview press 2.");
                        Console.WriteLine("If you want to keep listening, press ENTER");
                        verif = Console.ReadLine();
                    }
                    if (verif == "0")
                    {
                        condition = Pause();
                        if (condition == 2) return;
                        else if (condition == 1)
                        {
                            Console.WriteLine("Playing: {0}", s.Name);
                            verif = "-1";
                        }
                    }
                    else if (verif == "1")
                    {
                         s = SkipOrPreviousSong(s, p, d, name, 1);
                        if (s != song) break;
                    }
                    else if (verif == "2")
                    {
                        s = SkipOrPreviousSong(s, p, d, name, 2);
                        if (s != song) break;
                    }

                    Thread.Sleep(500);
                    cont++;
                }
                if (cont == seconds)
                {
                    Console.Write("Did you like the song? Give it a like!! (y/n): ");
                    string like = Console.ReadLine();
                    if (like == "y")
                    {
                        profile.AddFavSongs(song);
                        song.Likes++;
                    }
                    song.GeneralRep++;
                    return;
                }
            }
            //Si es menor de tal edad no puede ver esta pelicula;



        }
        public void PlayVideo(int cont, Video video, PlayList p, DataBase d, string name,User user, Profile profile)
        {
            Video v = video;
            while (true)
            {
                double seconds = 60 * v.Duration; //duration viene de multimedia y es la cantidad de minutos como decimal, hay que establecer una relacion
                Console.WriteLine("Playing: " + v.Name); //name tambien viene de multimedia.
                string verif = "-1";
                int condition = -1;
                while (cont < seconds)
                {
                    if (user.AdsOn == true && cont % 26 == 0)
                    {
                        List<string> AdsList = new List<string>(){ "Are you a standar user? Pfff try upgrading to premium and stop getting Ads!!"
                                    , "Keep Calm Leguer's Toilet Paper doesn't run out of stock in this Quarentine, come and buy it!!"
                                    , "Do you want to be good at videogames? try watching Juan Jacobo's tip and tricks videos."
                                    , "Are you into Podcasts? COMING SOON Diego's Podcast 'FyBuZz in tha house' "};
                        Random random = new Random();
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("ADS-ON");
                        Console.WriteLine(AdsList[random.Next(4)]);
                        Console.WriteLine("-------------------------------------------------------");
                        for (int i = 0; i < 1; i++)
                        {
                            Thread.Sleep(5000);
                        }
                        
                    }
                    if (cont % 10 == 0)
                    {
                        Console.WriteLine("To pause press 0.");
                        Console.WriteLine("To skip press 1.");
                        Console.WriteLine("To preview press 2.");
                        Console.WriteLine("If you want to keep listening, press ENTER");
                        verif = Console.ReadLine();
                    }
                    if (verif == "0")
                    {
                        condition = Pause();
                        if (condition == 2) return;
                        else if (condition == 1)
                        {
                            Console.WriteLine("Playing: {0}", v.Name);
                            verif = "-1";
                        }
                    }
                    else if (verif == "1")
                    {
                        v = SkipOrPreviousVideo(v, p, d, name, 1);
                        if (v != video) break;
                    }
                    else if (verif == "2")
                    {
                        v = SkipOrPreviousVideo(v, p, d, name, 2);
                        if (v != video) break;
                    }

                    Thread.Sleep(500);
                    cont++;
                }
                if (cont == seconds)
                {
                    Console.Write("Did you like the video? Give it a like!! (y/n): ");
                    string like = Console.ReadLine();
                    if (like == "y")
                    {
                        profile.AddFavVideos(video);
                        video.Likes++;
                    }
                    video.GeneralRep++;
                    return;
                }

            }
            //Si es menor de tal edad no puede ver esta pelicula;

        }

        public int Pause()
        {
            string play = "0";
            int aux = 0;
            while (true)
            {
                if (aux % 5 == 0)
                {
                    Console.WriteLine("To resume, press 1");
                    Console.WriteLine("To stop, press 2");
                    Console.WriteLine("If you want to continue like this, press ENTER");
                    play = Console.ReadLine();
                }
                if (play == "1" || play == "2") break;
                aux++;
                Thread.Sleep(500);
            }
            return int.Parse(play);
        }

        public Song SkipOrPreviousSong(Song s, PlayList p, DataBase d, string name, int typeOption)
        {
            List<Song> ListSongsGlobal = new List<Song>();
            ListSongsGlobal = d.Load_Songs();

            if (typeOption == 1)
            {
                if (p == null)
                {
                    for (int i = 0; i < ListSongsGlobal.Count(); i++)
                    {
                        if (((s.InfoSong()[0] == ListSongsGlobal[i].InfoSong()[0]) && (s.InfoSong()[1] == ListSongsGlobal[i].InfoSong()[1])) && (i != (ListSongsGlobal.Count() - 1))) return ListSongsGlobal[i + 1];
                        else if (((s.InfoSong()[0] == ListSongsGlobal[i].InfoSong()[0]) && (s.InfoSong()[1] == ListSongsGlobal[i].InfoSong()[1])) && (i == (ListSongsGlobal.Count() - 1))) return ListSongsGlobal[0];

                    }
                    return s;
                }
                else
                {
                    List<Song> sPlaylist = p.DicCanciones[name];
                    for (int i = 0; i < sPlaylist.Count(); i++)
                    {
                        if ((s == sPlaylist[i]) && (i != (sPlaylist.Count() - 1))) return sPlaylist[i + 1];
                        else if ((s == sPlaylist[i]) && (i == (sPlaylist.Count() - 1))) return sPlaylist[0];
                    }
                    return s;
                }
            }
            else
            {
                if (p == null)
                {
                    for (int i = 0; i < ListSongsGlobal.Count(); i++)
                    {
                        if (((s.InfoSong()[0] == ListSongsGlobal[i].InfoSong()[0]) && (s.InfoSong()[1] == ListSongsGlobal[i].InfoSong()[1])) && (i != 0)) return ListSongsGlobal[i - 1];
                        else if (((s.InfoSong()[0] == ListSongsGlobal[i].InfoSong()[0]) && (s.InfoSong()[1] == ListSongsGlobal[i].InfoSong()[1])) && (i == 0)) return ListSongsGlobal[(ListSongsGlobal.Count() - 1)];

                    }
                    return s;
                }
                else
                {
                    List<Song> sPlaylist = p.DicCanciones[name];
                    for (int i = 0; i < sPlaylist.Count(); i++)
                    {
                        if ((s == sPlaylist[i]) && (i != 0)) return sPlaylist[i - 1];
                        else if ((s == sPlaylist[i]) && (i == 0)) return sPlaylist[(sPlaylist.Count() - 1)];
                    }
                    return s;
                }
            }


        }

        public Video SkipOrPreviousVideo(Video v, PlayList p, DataBase d, string name, int typeOption)
        {
            List<Video> ListVideosGlobal = new List<Video>();
            ListVideosGlobal = d.Load_Videos();

            if (typeOption == 1)
            {
                if (p == null)
                {
                    for (int i = 0; i < ListVideosGlobal.Count(); i++)
                    {
                        if (((v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0]) && (v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0])) && (i != (ListVideosGlobal.Count() - 1))) return ListVideosGlobal[i + 1];
                        else if (((v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0]) && (v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0])) && (i == (ListVideosGlobal.Count() - 1))) return ListVideosGlobal[0];

                    }
                    return v;
                }
                else
                {
                    List<Video> vPlaylist = p.DicVideos[name];
                    for (int i = 0; i < vPlaylist.Count(); i++)
                    {
                        if ((v == vPlaylist[i]) && (i != (vPlaylist.Count() - 1))) return vPlaylist[i + 1];
                        else if ((v == vPlaylist[i]) && (i == (vPlaylist.Count() - 1))) return vPlaylist[0];
                    }
                    return v;
                }
            }
            else
            {
                if (p == null)
                {
                    for (int i = 0; i < ListVideosGlobal.Count(); i++)
                    {
                        if (((v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0]) && (v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0])) && (i != 0)) return ListVideosGlobal[i - 1];
                        else if (((v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0]) && (v.InfoVideo()[0] == ListVideosGlobal[i].InfoVideo()[0])) && (i == 0)) return ListVideosGlobal[(ListVideosGlobal.Count() - 1)];

                    }
                    return v;
                }
                else
                {
                    List<Video> vPlaylist = p.DicVideos[name];
                    for (int i = 0; i < vPlaylist.Count(); i++)
                    {
                        if ((v == vPlaylist[i]) && (i != 0)) return vPlaylist[i - 1];
                        else if ((v == vPlaylist[i]) && (i == 0)) return vPlaylist[(vPlaylist.Count() - 1)];
                    }
                    return v;
                }
            }


        }



        /*public void Random(PlayList playList)
        {
            Random random = new Random();
            int cont = 0;
            if (playList.Format == ".mp3" || playList.Format == ".wav")
            {
                Dictionary<string, List<Song>> playlistDicSong = playList.DicCanciones;
                foreach (List<Song> listsong in playlistDicSong.Values)
                {
                    int len = listsong.Count();
                    
                    PlaySong(cont, listsong[random.Next(0,len)], true);
                    
                }
            }
            else
            {
                Dictionary<string, List<Video>> playlistDicVideo = playList.DicVideos;
                foreach (List<Video> listvideo in playlistDicVideo.Values)
                {
                    int len = listvideo.Count();

                    PlayVideo(cont, listvideo[random.Next(0, len)], true);
                }
            }
        }*/
    }
}
