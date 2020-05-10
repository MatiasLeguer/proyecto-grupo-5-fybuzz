using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Menu
    {
        private List<String> filters;
        public List<Song> searchedSongs;
        public List<Video> searchedVideos;
        User user = new User();
        DataBase database = new DataBase();
        Player player = new Player();

        public bool DisplayLogin()
        {
            Server server = new Server(database);

            bool x = false;
            while (x == false)
            {
                Console.WriteLine("------------Welcome to FyBuZz--------------");
                Console.WriteLine("I) Log-In with a existing account.");
                Console.WriteLine("II) Register.");
                string dec = Console.ReadLine();
                if (dec == "I")
                {
                    //poner el metodo de server o algo.
                    if (database.LogIn(user.Username, user.Password) == null) //tengo que obtener mediante un get el nombre de usuario y password
                    {
                        Console.WriteLine("Login Succesfull.");
                        x = true;
                    }
                }
                else
                {
                    server.Register(); //Agregue el metodo de server register.
                    x = false;
                }
            }
            return x;
        }
        //if DisplayLogin == true:
        public Profile DisplayProfiles()
        {
            Dictionary<int, Profile> dicprofile = new Dictionary<int, Profile>();
            List<Profile> profilelist = new List<Profile>();
            Profile profile_n = new Profile("", "", "", "", "", 0);

            Console.WriteLine("---------Profiles----------");
            Console.WriteLine("Choose a profile or Create Profile");
            string dec = Console.ReadLine();
            bool x = true;
            while (x == true)
            {
                if (dec == "Choose a profile")
                {
                    Console.WriteLine("Choose a profile:");
                    dicprofile = user.Perfiles;
                    foreach (Profile profile in dicprofile.Values)
                    {
                        Console.WriteLine(profile.ProfileName);
                        profilelist.Add(profile);
                    }
                    string perfil = Console.ReadLine();
                    for (int i = 0; i < profilelist.Count(); i++)
                    {
                        if (perfil == profilelist[i].ProfileName)
                        {
                            profile_n = profilelist[i]; // tengo que devolver algun perfil
                            x = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Create a profile:");
                    //Metodo de crear perfil y agregarlo al diccionario del usuario.
                    //Vuelves de nuevo a la decision si escoges o creas un perfil...
                }
            }
            return profile_n;
        }

        //Se necesita el perfil con el que quiere acceder
        public void DisplayStart(Profile profile) // solo funciona si DisplayLogIn() retorna true se ve en program.
        {
            

            List<PlayList> listPlayListGlobal = new List<PlayList>();
            listPlayListGlobal = database.Load_PLs();

            List<Video> listVideosGlobal = new List<Video>();
            listVideosGlobal = database.Load_Videos();

            List<Song> listSongsGlobal = new List<Song>();
            listSongsGlobal = database.Load_Songs();

            
            // mostrará todas las playlist del usuario, si es primera vez que ingresa estara la playlist general y la favorita(esta sin nada)
            DisplayPlaylist(listPlayListGlobal); // es la lista global de playlist que viene de database, pero hay que conectarla

            PlayList favSongs = new PlayList(".mp3", "FavoriteSongs");
            Dictionary<string, List<Song>> playlistFavSongs = favSongs.DicCanciones; //Playlist de favoritos que su nombre es el de arriba.

            PlayList favVideos = new PlayList(".mp3", "FavoriteVideos");
            Dictionary<string, List<Video>> playlistFavVideos = favVideos.DicVideos; //Playlist de favoritos que su nombre es el de arriba.

            List<PlayList> followedPL = profile.FollowedPlayList; //una lista de todas las playlist, discos, usuarios, etc.
            //Si seguimos la usuario seguiremos todas sus playlist (REVISAR ESTO)

            Console.WriteLine("------------Welcome to FyBuZz--------------"); //Se inicia el menu en si.
            bool x = true;
            while (x == true)
            {
                Console.WriteLine("I) Search Songs or Videos."); //Faltaria la bsuqueda de gente.
                Console.WriteLine("II) Display all Playlists.");
                Console.WriteLine("III) Account Settings.");
                Console.WriteLine("IV) Play a Playlist.");
                Console.WriteLine("V) LogOut.");
                Console.WriteLine("VI) CloseApp.");
                string dec = Console.ReadLine();
                switch (dec)
                { //(REVISAR DESPUES)Mejorar el metodo de busqueda para que busque canciones que se parezca
                    case "I":
                        //Método de buscar, una vez buscada la canción y elegida.
                        
                        Console.WriteLine("What would you like to search? (Songs/Videos)");
                        string type = Console.ReadLine();
                        if (type == "Songs")
                        {
                            Console.WriteLine("Type what you want to search...");
                            string search = Console.ReadLine();
                            List<string> searchEngine = SearchEngine(search, type);
                            List<int> indexglobal = new List<int>();
                            Console.WriteLine("Searched Songs, choose one...");
                            for(int i = 0; i < searchEngine.Count(); i++)
                            {
                                searchEngine[i].Split('>'); // [[Bad Bunny Safaera etc],[12]]
                                indexglobal.Add(searchEngine[i][1]);

                                Console.WriteLine((i+1) + ") " + searchEngine[i][0]);
                            }
                            int indice = int.Parse(Console.ReadLine())-1;
                            Song song = listSongsGlobal[indexglobal[indice]]; //La cancion a la que querria escuchar

                            Reproduction(1, 0, false); //Falta arreglar el método de reproduccion
                        }
                        else
                        {
                            Console.WriteLine("Type what you want to search...");
                            string search = Console.ReadLine();
                            List<string> searchEngine = SearchEngine(search, type);
                            List<int> indexglobal = new List<int>();
                            Console.WriteLine("Searched Videos, choose one...");
                            for (int i = 0; i < searchEngine.Count(); i++)
                            {
                                searchEngine[i].Split('>'); // [[Bad Bunny Safaera etc],[12]]
                                indexglobal.Add(searchEngine[i][1]);

                                Console.WriteLine((i + 1) + ") " + searchEngine[i][0]);
                            }
                            int indice = int.Parse(Console.ReadLine()) - 1;
                            Video video = listVideosGlobal[indexglobal[indice]]; //La video a la que querria escuchar

                            Reproduction(1, 0, false); //Falta arreglar el método de reproduccion
                        }
                        break;
                    case "II":
                        if (playlistFavSongs.Count() != 0)
                        {
                            Console.WriteLine(favSongs.InfoPlayList());
                        }
                        if (playlistFavVideos.Count() != 0)
                        {
                            Console.WriteLine(favSongs.InfoPlayList());
                        }
                        if (followedPL.Count() != 0)
                        {
                            DisplayPlaylist(followedPL);
                        }
                        DisplayPlaylist(listPlayListGlobal);

                        break;
                    case "III":
                        AccountSettings(user); // incorporar el usuario.

                        break;

                    case "IV":
                        Console.WriteLine("What playlist do you want to play?(GlobalPlayLists, FollowedPlaylists or FavoritePlayList)");
                        string play = Console.ReadLine();
                        if (play == "FavoritePlayList")
                        {
                            Console.WriteLine("Random or select a song/video?");
                            string rand = Console.ReadLine();
                            if (rand == "Random")
                            {
                                Reproduction(4, true);
                            }
                            else
                            {
                                Console.WriteLine("What would you like to play?");
                                string mult = Console.ReadLine();
                                if (mult == "Songs")
                                {
                                    SongsSearchEngine(searchedSongs);
                                    Reproduction(1, 0, false);
                                }
                                else
                                {
                                    VideosSearchEngine(searchedVideos);
                                    Reproduction(1, 1, false);
                                }
                            }
                        }
                        else if (play == "GlobalPlayList")
                        {
                            Console.WriteLine("Please select the number...");
                            string num = Console.ReadLine();
                            //Elegir la playlist que te dan y según eso lo siguiente.
                            Console.WriteLine("Random or select a song?");
                            string rand = Console.ReadLine();
                            if (rand == "Random")
                            {
                                Reproduction(4, true);
                            }
                            else
                            {
                                Console.WriteLine("What would you like to play?");
                                string mult = Console.ReadLine();
                                if (mult == "Songs")
                                {
                                    SongsSearchEngine(searchedSongs);
                                    Reproduction(1, 0, false);
                                }
                                else
                                {
                                    VideosSearchEngine(searchedVideos);
                                    Reproduction(1, 1, false);
                                }
                            }
                        }
                        else if (play == "FollowedPlaylist")
                        {
                            Console.WriteLine("Please select the number...");
                            string num = Console.ReadLine();
                            //Elegir la playlist que te dan y según eso lo siguiente.
                            Console.WriteLine("Random or select multimedia?");
                            string rand = Console.ReadLine();
                            if (rand == "Random")
                            {
                                Reproduction(4, true);
                            }
                            else
                            {
                                Console.WriteLine("What would you like to play?");
                                string mult = Console.ReadLine();
                                if (mult == "Songs")
                                {
                                    SongsSearchEngine(searchedSongs);
                                    Reproduction(1, 0, false);
                                }
                                else
                                {
                                    VideosSearchEngine(searchedVideos);
                                    Reproduction(1, 1, false);
                                }
                            }
                        }
                        break;
                    case "V":
                        //termina el método y llamaria al metodo de inicio en program.
                        Console.WriteLine("LoggedOut");
                        x = false;
                        break;
                    case "VI":
                        x = false;
                        break;
                }
            }
        }

        //tenemos que decidir si esta clase sera de inputs y outputs, o la que hace de reproductor.
        public void DisplayPlaylist(List<PlayList> playlist)
        {
            for (int i = 0; i < playlist.Count(); i++)
            {
                Console.WriteLine(i + ") " + playlist[i].InfoPlayList());
            }
        }
        public void AccountSettings(User user)
        {
            for (int i = 0; i < user.AccountSettings().Count(); i++)
            {
                Console.WriteLine("Username: " + user.AccountSettings()[0] + "\n");
                Console.WriteLine("Password: " + user.AccountSettings()[1] + "\n");
                Console.WriteLine("Email: " + user.AccountSettings()[2] + "\n");
                Console.WriteLine("Account type: " + user.AccountSettings()[3] + "\n");
            }
        }

        public void Reproduction(int verif, int multimediafile, bool ver) // Si viene de una playlist y se decide poner aleatorio verif sera 4, si se elige una canción sera 1.
        {
            if (verif == 1)
            {
                int x = 0;
                int cont = 0;
                while (cont != -1)
                {
                    cont = player.Play(cont, multimediafile, ver, x); //Devuelve el tiempo en el que se para la canción
                    if (cont != -1) player.Stop(cont); // devuelve el contador cuando se detiene para empezar de nuevo.
                }
            }
            else
            {
                player.Random();
                //Ponerle Play a cualquier canción en la Playlist
            }
        }

        public List<string> SearchEngine(string searching, string type)
        {
            List<Video> listVideosGlobal = new List<Video>();
            listVideosGlobal = database.Load_Videos();

            List<Song> listSongsGlobal = new List<Song>();
            listSongsGlobal = database.Load_Songs();

            List<string> searchEngine = new List<string>();
            int num_s = 0;
            int num_v = 0;


            if (type == "Songs")
            {
                foreach (Song song in listSongsGlobal)
                {
                    for (int i = 0; i < song.InfoSong().Count(); i++)
                    {
                        if (song.InfoSong()[i] == searching)
                        {
                            searchEngine.Add(song.SearchedInfoSong() + ">" + num_s); //num_s es un int y no me patalea, si tira error es aca. Usar remove leguer
                        }
                    }
                    num_s++;
                }
            }
            else
            {
                foreach (Video video in listVideosGlobal)
                {
                    for (int i = 0; i < video.InfoVideo().Count(); i++)
                    {
                        if (video.InfoVideo()[i] == searching)
                        {
                            searchEngine.Add(video.SearchedInfoVideo() + ">" + num_v);
                        }
                        
                    }
                    num_v++;
                }
            }
            if (searchEngine.Count() == 0) Console.WriteLine("No match found...");
            return searchEngine;
        }

        public void DisplayHistory(List<Song> searchStorySongs, List<Video> searchStoryVideos)
        {
            if (searchStorySongs.Count() < 5 || searchStoryVideos.Count() < 5)
            {
                for (int i = 0; i < searchStorySongs.Count(); i++)
                {
                    Console.WriteLine(searchStorySongs[i]); //Recordar que cada eleemento de estas listas van a ser la información de cada archivo multimedia.
                }
                for (int i = 0; i < searchStoryVideos.Count(); i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStorySongs[i]);
                }
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }
        }
    }
}

