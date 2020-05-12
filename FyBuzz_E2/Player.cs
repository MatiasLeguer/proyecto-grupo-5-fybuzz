using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Player
    {
        //Hay que usar eventos que vengan desde menú y triguereen los métodos de aca con las canciones de Database.
        // hacer un get a la duracion y al nombre
        public void PlaySong(int cont, Song song, PlayList p, DataBase d, string name)
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
                        Song ss = SkipOrPreviousSong(s, p, d, name, 1);
                        if (ss != song) break;
                    }
                    else if (verif == "2")
                    {
                        Song ss = SkipOrPreviousSong(s, p, d, name, 2);
                        if (ss != song) break;
                    }

                    Thread.Sleep(500);
                    cont++;
                }
                if (cont == seconds) return;
            }
            //Si es menor de tal edad no puede ver esta pelicula;



        }
        public void PlayVideo(int cont, Video video, PlayList p, DataBase d, string name)
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
                if (cont == seconds) return;
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
            if (typeOption == 1)
            {
                if (p == null)
                {
                    for (int i = 0; i < d.ListSongsGlobal.Count(); i++)
                    {
                        if ((s == d.ListSongsGlobal[i]) && (i != (d.ListSongsGlobal.Count() - 1))) return d.ListSongsGlobal[i + 1];
                        else if ((s == d.ListSongsGlobal[i]) && (i == (d.ListSongsGlobal.Count() - 1))) return d.ListSongsGlobal[0];

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
                    for (int i = 0; i < d.ListSongsGlobal.Count(); i++)
                    {
                        if ((s == d.ListSongsGlobal[i]) && (i != 0)) return d.ListSongsGlobal[i - 1];
                        else if ((s == d.ListSongsGlobal[i]) && (i == 0)) return d.ListSongsGlobal[(d.ListSongsGlobal.Count() - 1)];

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
            if (typeOption == 1)
            {
                if (p == null)
                {
                    for (int i = 0; i < d.ListVideosGlobal.Count(); i++)
                    {
                        if ((v == d.ListVideosGlobal[i]) && (i != (d.ListVideosGlobal.Count() - 1))) return d.ListVideosGlobal[i + 1];
                        else if ((v == d.ListVideosGlobal[i]) && (i == (d.ListVideosGlobal.Count() - 1))) return d.ListVideosGlobal[0];

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
                    for (int i = 0; i < d.ListVideosGlobal.Count(); i++)
                    {
                        if ((v == d.ListVideosGlobal[i]) && (i != 0)) return d.ListVideosGlobal[i - 1];
                        else if ((v == d.ListVideosGlobal[i]) && (i == 0)) return d.ListVideosGlobal[(d.ListVideosGlobal.Count() - 1)];

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
