using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FyBuzz_Entrega2
{
    public class Menu
    {
        private List<string> filters;
        public List<Song> searchedSongs;
        public List<Video> searchedVideos;
        User user = new User();
        public bool DisplayLogin()
        {
            DataBase database = new DataBase();

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
                    //poner le metodo de register
                    //verificar mail.
                    if (todo ok)
                    {
                        Console.WriteLine("Account created, now log in.");
                        x = false;
                    }
                }
            }
            return x;
        }
        public void DisplayProfiles()
        {
            Dictionary<int, Profile> dicprofile = new Dictionary<int, Profile>();
            List<Profile> profilelist = new List<Profile>();
            Console.WriteLine("---------Profiles----------");
            Console.WriteLine("Choose a profile or Create Profile");
            string dec = Console.ReadLine();
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
                        return profilelist[i];
                    }
                }

            }
        }

        public void DisplayStart() // solo funciona si DisplayLogIn() retorna true se ve en program.
        {
            Console.WriteLine("------------Welcome to FyBuZz--------------");
            // mostrará todas las playlist del usuario, si es primera vez que ingresa estara la playlist general y la favorita(esta sin nada)
            DisplayPlaylist(listPlaylistGlobal); // es la lista global de playlist que viene de database, pero hay que conectarla
            if (PlaylistFav().Count() != 0)
            {
                Console.WriteLine(PlaylistFav().InfoPlaylist());
            }
            if (Playlistseguidos.Count() != 0)
            {
                Console.WriteLine(DisplayPlaylist(PlaylistSeguidos));
            }
            bool x = true;
            while (x == true)
            {
                Console.WriteLine("I) Search Songs or Videos.");
                Console.WriteLine("II) Account Settings.");
                Console.WriteLine("III) Play a Playlist.");
                Console.WriteLine("IV) LogOut.");
                Console.WriteLine("V) CloseApp.");
                string dec = Console.ReadLine();
                switch (dec)
                {
                    case "I":
                        //Método de buscar, una vez buscada la canción y elegida.
                        Console.WriteLine("What would you like to search?");
                        string search = Console.ReadLine();
                        if (search == "Songs")
                        {
                            SongsSearchEngine(searchedSongs);
                            Reproduction(1, 0, false);
                        }
                        else
                        {
                            VideosSearchEngine(searchedVideos);
                            Reproduction(1, 1, false);
                        }
                        break;

                    case "II":
                        AccountSettings(user); // incorporar el usuario.

                        break;

                    case "III":
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
                        break;
                    case "IV":
                        //termina el método y llamaria al metodo de inicio en program.
                        Console.WriteLine("LoggedOut");
                        x = false;
                        break;
                    case "V":
                        x = false;
                        break;
                }
            }
        }

        //tenemos que decidir si esta clase sera de inputs y outputs, o la que hace de reproductor.
        public void DisplayPlaylist(List<Playist> playlist)
        {
            for (int i = 0; i < playlist.Count(); i++)
            {
                Console.WriteLine(i + ") " + playlist[i].InfoPlaylist());
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
            Player player = new Player();

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
    }

    public void SongsSearchEngine(List<String> songsSearched)//No se si meter parametros, si es asi, serian las listas de profile preference
    {
        ProfilePreferences profilePreferences = new ProfilePreferences();
        for (int i = 1; i <= filters.count(); i++)
        {
            Console.WriteLine(i + ") " + filters[i]); //imprime todos los filtros que tengamos.
        }
        //Console.WriteLine("Type the filters you will use separated by space: (use filter from above)");
        //string user_filters = Console.ReadLine();
        //No se como ver que filtros habrian en la busqueda
        //Buscar el archivo multimedia y agregarlo a una variable llamada multimedia que se diferecniarar segun el ipo del archivo. Este se ira a el BrowserHistory

        List<Song> searchedStorySongs = profilePreferences.BrowserHistorySongs(multimedia);

        Displayistory(searchedStorySongs, searchedStoryVideos);
        //podria llamar al método displayhistory en este metodo y hacer una clase que se vaya modificando cada 10 busquedas, y esta entregarsela al metodo history para que la use y la ponga. 
    }
    public void VideosSearchEngine(List<String> videosSearched)//No se si meter parametros, si es asi, serian las listas de profile preference
    {
        ProfilePreferences profilePreferences = new ProfilePreferences();

        for (int i = 1; i <= filters.count(); i++)
        {
            Console.WriteLine(i + ") " + filters[i]); //imprime todos los filtros que tengamos.
        }
        //Console.WriteLine("Type the filters you will use separated by space: (use filter from above)");
        //string user_filters = Console.ReadLine();
        //No se como ver que filtros habrian en la busqueda
        //De alguna manera tengo que acceder a la lista de canciones en database.
        // Si se encuentra la video esat se agregará a la lista ed canciones, 

        List<Video> searchedStoryVideos = profilePreferences.BrowserHistoryVideos(multimedia);
        Displayhistory(searchedStorySongs, searchedStoryVideos);


        //podria llamar al método displayhistory en este metodo y hacer una clase que se vaya modificando cada 10 busquedas, y esta entregarsela al metodo history para que la use y la ponga. 
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

