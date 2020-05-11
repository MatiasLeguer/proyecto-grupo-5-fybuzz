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
        public int PlaySong(int cont, Song song, bool inplaylist)
        {
            
            //Si es menor de tal edad no puede ver esta pelicula;
            double seconds = 60 * song.Duration; //duration viene de multimedia y es la cantidad de minutos como decimal, hay que establecer una relacion
            Console.WriteLine("Playing: " + song.Name); //name tambien viene de multimedia.
            Console.WriteLine("To stop press 0.\n");
            Console.WriteLine("To skip press 2.\n");
            Console.WriteLine("To preview press 3.\n");
            while (cont < seconds)
            {
                int verif = int.Parse(Console.ReadLine());
                if (verif == 0) break;
                //Solo se puede pponer play y pausa
                Thread.Sleep(1000);
                cont++;
            }
            if (cont == seconds) cont = -1;
            return cont;
        }
        public int PlayVideo(int cont, Video video, bool playlist)
        {

            //Si es menor de tal edad no puede ver esta pelicula;
            double seconds = 60 * video.Duration; //duration viene de multimedia y es la cantidad de minutos como decimal, hay que establecer una relacion
            Console.WriteLine("Playing: " + video.Name); //name tambien viene de multimedia.
            Console.WriteLine("To stop press 0.\n");
            Console.WriteLine("To skip press 2.\n");
            Console.WriteLine("To preview press 3.\n");
            while(cont <= seconds)
            {
                cont++;
                int verif = int.Parse(Console.ReadLine());
                if (verif == 0) break;
                
                Thread.Sleep(1000);
            }
            if (cont == seconds) cont = -1;
            return cont;
        }

        public int Stop(int cont)
        {
            Console.WriteLine("To play press 1.\n");
            int play = 0;
            while (play != 1)
            {
                play = int.Parse(Console.ReadLine());
                if (play == 1) return cont;
                else continue;
            }
            return cont;
        }
        /*
        public void Skip(PlayList playList, int indexcurrent)
        {
            int cont = 0;
            indexcurrent++;
            if (playList.Format == ".mp3" || playList.Format == ".wav")
            {
                Dictionary<string, List<Song>> playlistDicSong = playList.DicCanciones;
                foreach(List<Song> listsong in playlistDicSong.Values)
                {
                    if(indexcurrent < listsong.Count())
                    {
                        PlaySong(cont, listsong[indexcurrent], true, indexcurrent);
                    }
                    else Console.WriteLine("Last multimedia archive in the playlist. Error");
                }
            }
            else
            {
                Dictionary<string, List<Video>> playlistDicVideo = playList.DicVideos;
                foreach (List<Video> listvideo in playlistDicVideo.Values)
                {
                    if (indexcurrent < listvideo.Count())
                    {
                        PlayVideo(cont, listvideo[indexcurrent], true, indexcurrent);
                    }
                    else Console.WriteLine("Last multimedia archive in the playlist. Error");
                }
            }

        }

        public int Previous(int cont, PlayList playList, int indexcurrent)
        {
            if (cont == 0)
            {
                indexcurrent--;
                if (playList.Format == ".mp3" || playList.Format == ".wav")
                {
                    Dictionary<string, List<Song>> playlistDicSong = playList.DicCanciones;
                    foreach (List<Song> listsong in playlistDicSong.Values)
                    {
                        if (indexcurrent > 0)
                        {
                            PlaySong(cont, listsong[indexcurrent], true, indexcurrent);
                        }
                        else Console.WriteLine("First multimedia archive in the playlist. Error");
                    }
                }
                else
                {
                    Dictionary<string, List<Video>> playlistDicVideo = playList.DicVideos;
                    foreach (List<Video> listvideo in playlistDicVideo.Values)
                    {
                        if (indexcurrent > 0)
                        {
                            PlayVideo(cont, listvideo[indexcurrent], true, indexcurrent);
                        }
                        else Console.WriteLine("First multimedia archive in the playlist. Error");
                    }
                }
                
            }
            else cont = 0;
            return cont;
        }
        */
        public void Random(PlayList playList)
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
        }
    }
}

