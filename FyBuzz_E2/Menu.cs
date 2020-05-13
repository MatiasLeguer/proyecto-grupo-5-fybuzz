using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Menu
    {
        protected List<string> filters;
        protected List<string> badWords = new List<string>() { "fuck", "sex", "niggas", "sexo", "ass", "nigger", "culo", "viola", "violar", "spank", "puta", "hooker","perra","hoe","cocaina","alchohol","blunt","weed","marihuana","lcd","kush","krippy","penis","dick","cock","shit","percocet" };
        public List<Song> searchedSongs;
        public List<Video> searchedVideos;
        User user = new User();
        DataBase database = new DataBase();
        Player player = new Player();


        public User DisplayLogin(List<User> userDataBase)
        {
            Server server = new Server(database);
            bool x = false;
            while (x == false)
            {
                Console.WriteLine("------------Welcome to FyBuZz--------------");
                Console.WriteLine("I) Log-In with a existing account.");
                Console.WriteLine("II) Register.");
                Console.WriteLine("III) Close App.");
                string dec = Console.ReadLine();
                switch (dec)
                {
                    case "I":
                        Console.Write("Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        if (database.LogIn(username, password,userDataBase) != null)
                        {
                            Console.WriteLine("Login Succesfull.");
                            Console.Beep();
                            x = true;
                            user = database.LogIn(username, password,userDataBase); 
                        }
                        else
                        {
                            Console.WriteLine("ERROR[!] Invalid Username or Password");
                        }
                        break;


                    case "II":
                        server.Register(user,userDataBase);
                        database.Save_Users(userDataBase);
                        x = false;
                        break;
                    case "III":
                        return null;    
                }
            }
            return user;
        }
        
        public Profile DisplayProfiles(User user,List<User> userDataBase)
        {
            Console.WriteLine("Welcome: " + user.Username + "\n");
            bool x = true;
            int u = 0;
            foreach(User usr in userDataBase)
            {
                if(usr.Username == user.Username)
                {
                    break;
                }
                u++;
            }
            Console.WriteLine("---------Profiles----------");

            if (userDataBase[u].Perfiles.Count() == 0)
            {
                Console.Write("Select your gender(M/F): ");
                string gender = Console.ReadLine();
                Console.Write("Select your age: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Select your Profile Type(creator/viewer): ");
                string profileType = Console.ReadLine();
                Profile profile = new Profile(user.Username, ".JPEG", profileType, user.Email, gender, age);
                user.Perfiles.Add(profile);

            }
            else
            {
                while (x == true)
                {
                    Console.WriteLine("I) Choose a profile\nII) Create Profile\nIII) Close App.");
                    string dec = Console.ReadLine();
                    if (dec == "I")
                    {
                        Console.WriteLine("List of profiles:");
                        int i;
                        for (i = 0; i < userDataBase[u].Perfiles.Count(); i++)
                        {
                            Console.WriteLine("{0}).- {1}", i + 1, userDataBase[u].Perfiles[i].ProfileName);
                        }
                        Console.WriteLine("Choose a profile:");
                        int index = int.Parse(Console.ReadLine()) - 1;
                        return userDataBase[u].Perfiles[index];
                    }
                    else if (dec == "II")
                    {
                        if (user.Accountype == "premium")
                        {
                            Console.WriteLine("Create a profile:");
                            Console.Write("Profile name: ");
                            string pname = Console.ReadLine();
                            Console.Write("Profile pic: ");
                            string ppic = Console.ReadLine();
                            Console.Write("Profile type(creator/viewer): ");
                            string ptype = Console.ReadLine();
                            string pmail = user.Email;
                            Console.Write("Profile gender (M/F): ");
                            string pgender = Console.ReadLine();
                            Console.Write("Profile age: ");
                            int page = int.Parse(Console.ReadLine());
                            userDataBase[u].CreateProfile(pname, ppic, ptype, pmail, pgender, page);
                        }
                        else if (user.Accountype == "standard")
                        {
                            Console.WriteLine("ERROR [!] Standard Account Types, you can only have one profile.\nTry Upgrading to Premium Account.");
                            Console.Write("Do you want to change your Account Type to premium?(y/n): ");
                            string premium = Console.ReadLine();
                            if (premium == "y") userDataBase[u].Accountype = "premium"; //Falta actualizar la lista de perfiles.
                            else continue;
                        }
                    }
                    else if(dec == "III")
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public int DisplayStart(Profile profile,User usr, List<User> listUserGlobal, List<Song> listSongsGlobal, List<Video> listVideosGlobal, List<PlayList> listPlayListGlobal) // solo funciona si DisplayLogIn() retorna true se ve en program.
        {
            int ret = 0;

            // mostrará todas las playlist del usuario, si es primera vez que ingresa estara la playlist general y la favorita(esta sin nada)
            // es la lista global de playlist que viene de database, pero hay que conectarla

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
                Console.WriteLine("I) Search Songs, Videos or Users."); //Faltaria la bsuqueda de gente.
                Console.WriteLine("II) Add Songs, Videos or Playlists.");
                Console.WriteLine("III) Display all Playlists.");
                Console.WriteLine("IV) Account Settings/ Profile Settings.");
                Console.WriteLine("V) Play a Playlist.");
                Console.WriteLine("VI) LogOut from Prrofile.");
                Console.WriteLine("VII) CloseApp.");
                string dec = Console.ReadLine();
                switch (dec)
                { //(REVISAR DESPUES)Mejorar el metodo de busqueda para que busque canciones que se parezca
                    case "I":
                        //Método de buscar, una vez buscada la canción y elegida.
                        Console.WriteLine("What would you like to search? (Songs/Videos/Users)");
                        string type = Console.ReadLine();
                        if (type == "Songs")
                        {
                            Console.WriteLine("Type what you want to search...");
                            string search = Console.ReadLine();
                            List<string> searchEngine = SearchEngine(search, type, database);
                            List<int> indexglobal = new List<int>();
                            Console.WriteLine("Searched Songs: ");
                            for (int i = 0; i < searchEngine.Count(); i++)
                            {
                                Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                            }
                            Console.WriteLine("Searched Songs, choose the position of the song you want to hear...");
                            int indice = int.Parse(Console.ReadLine()) - 1;
                            Song song = listSongsGlobal[indice]; //La cancion a la que querria escuchar

                            int cont = 0;
                            foreach(string word in badWords)
                            {
                                if (song.Lyrics.Contains(word) == true && profile.Age < 16)
                                {
                                    Console.WriteLine("ERROR[!] This content is age restricted");
                                    cont++;
                                    break;
                                }
                            }
                            if(cont == 0)
                            {
                                Console.Clear();
                                player.PlaySong(song, null, database, usr, profile);
                            }
                            Console.WriteLine("\n");
                        }
                        else if(type == "Videos")
                        {
                            Console.WriteLine("Type what you want to search...");
                            string search = Console.ReadLine();
                            List<string> searchEngine = SearchEngine(search, type, database);
                            List<int> indexglobal = new List<int>();
                            if (searchEngine.Count != 0)
                            {
                                Console.WriteLine("Searched Videos: ");
                                for (int i = 0; i < searchEngine.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                                }
                                Console.WriteLine("Searched Songs, choose the position of the song you want to hear...");
                                int indice = int.Parse(Console.ReadLine()) - 1;
                                Video video = listVideosGlobal[indice]; //La cancion a la que querria escuchar
                                int cont = 0;
                                foreach (string word in badWords)
                                {
                                    if ((video.Subtitles.Contains(word) && profile.Age < 16 )|| (int.Parse(video.Category) >= profile.Age))
                                    {
                                        Console.WriteLine("ERROR[!] This content is age restricted");
                                        cont++;
                                        break;
                                    }
                                }
                                if (cont == 0)
                                {
                                    Console.Clear();
                                    player.PlayVideo(video, null, database, usr,profile);
                                }
                                Console.WriteLine("\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Type the user you want to search...");
                            string search = Console.ReadLine();
                            List<string> searchEngine = SearchEngine(search, type, database);
                            if (searchEngine.Count != 0)
                            {
                                Console.WriteLine("Searched Users: ");
                                for (int i = 0; i < searchEngine.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                                }
                                Console.WriteLine("Searched Users, choose the position of the user you want to follow...");
                                int indice = int.Parse(Console.ReadLine()) - 1;
                                User u = listUserGlobal[indice];
                                u.Followers++;
                                Console.WriteLine("Followed: " + u.SearchedInfoUser());
                                Console.WriteLine("\n");
                            }
                        }
                        //database.Save_Users(database.UserDataBase);
                        break;

                    case "II":
                        if(profile.ProfileType == "creator")
                        {

                            Console.Write("What do you wish to add? (song(0), video(1), Playlist(2))\tDo you wish to Show the list of a specified multimedia? (song(3), video(4), Playlist(5)): ");
                            int opc = int.Parse(Console.ReadLine());
                            if(opc == 0 || opc == 1 || opc == 2)
                            {
                                List<string> infoMult = AskInfoMult(opc);
                                string description = database.AddMult(opc, infoMult,listSongsGlobal,listPlayListGlobal,listVideosGlobal);
                                if (description == null) Console.WriteLine("Multimedia has been registered into the system!");
                                else Console.WriteLine("ERROR[!] ~{0}", description);
                                if (opc == 0) database.Save_Songs(listSongsGlobal);
                                else if (opc == 1) database.Save_Videos(listVideosGlobal);
                                else if (opc == 2) database.Save_PLs(listPlayListGlobal);
                            }
                            else if(opc == 3 || opc == 4 || opc == 5)
                            {
                                DisplayGlobalMult(opc - 3, database);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You do not have permission to add Multimedia.");
                        }
                        break;
                    case "III":

                        if (playlistFavSongs != null || playlistFavVideos != null)
                        {
                            Console.WriteLine(favSongs.InfoPlayList());
                            Console.WriteLine(favSongs.InfoPlayList());
                        }
                        else Console.WriteLine("You don´t have a Favorite Playlist.");

                        if (followedPL != null)
                        {
                            DisplayPlaylists(followedPL); //Este metodo no esta bueno.
                        }
                        else Console.WriteLine("You don´t follow anyone Playlist.");
                        Console.WriteLine("Global Playlist:");
                        DisplayGlobalMult(2,database); //No imprime la lista
                        //database.Save_Users(database.UserDataBase);
                        Console.WriteLine("\n");
                        break;
                    case "IV":
                        Console.WriteLine("---------------------------");
                        Console.Write("Which settings do you want to acces?(Account/Profile): ");
                        string settings = Console.ReadLine();
                        if (settings == "Account") AccountSettings(usr);
                        else if (settings == "Profile") ProfileSettings(profile);
                        else Console.WriteLine("ERROR[!] Invalid Command");
                        Console.WriteLine("---------------------------");
                        break;

                    case "V":
                        Console.WriteLine("What playlist do you want to play?(GlobalPlayLists, FollowedPlaylists or FavoritePlayList)");
                        string play;
                        do
                        {
                            play = Console.ReadLine();
                        } while (play != "GlobalPlayLists" && play != "FollowedPlaylists" && play != "FavoritePlayList");
    
                        if (play == "FavoritePlayList")
                        {

                            Console.Write("Do you want to listen to a song (s) or watch a video (v)?: ");
                            string op;
                            do
                            {
                                op = Console.ReadLine();
                                if ((op != "s" && op != "v") && (op != "S" && op != "V"))
                                    Console.Write("ERROR[!] ~Select a valid command please");
                            } while ((op != "s" && op != "v") && (op != "S" && op != "V"));

                            if (op == "s")
                            {
                                if (user.Accountype == "premium")
                                {
                                    Console.WriteLine("Do you want to play a random song? or select it from the playlist? (r/s): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" || rndSel != "s");

                                    if (rndSel == "r")
                                    {
                                        player.PlaySong(profile.PlaylistFavoritosSongs[player.RandomMult(profile, 0, "Fav")], profile.PlaylistFavoritosSongs, database, user, profile);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < profile.PlaylistFavoritosSongs.Count(); i++)
                                        {
                                            Console.WriteLine("{0}) {1}", i + 1, profile.PlaylistFavoritosSongs[i].Name);
                                        }
                                        Console.Write("Escoga una cancion: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(profile.PlaylistFavoritosSongs[index], profile.PlaylistFavoritosSongs, database, user, profile);
                                    }

                                }
                                else
                                {
                                    player.PlaySong(profile.PlaylistFavoritosSongs[player.RandomMult(profile, 0, "Fav")], profile.PlaylistFavoritosSongs, database, user, profile);
                                }

                            }
                            else if (op == "v")
                            {
                                if (user.Accountype == "premium")
                                {
                                    Console.Write("Do you want to play a random video? or select it from the playlist? (r/v): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" || rndSel != "v");

                                    if (rndSel == "r")
                                    {
                                        player.PlayVideo(profile.PlaylistFavoritosVideos[player.RandomMult(profile, 1, "Fav")], profile.PlaylistFavoritosVideos, database, user, profile);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < profile.PlaylistFavoritosVideos.Count(); i++)
                                        {
                                            Console.WriteLine("{0}) {1}", i + 1, profile.PlaylistFavoritosVideos[i].Name);
                                        }
                                        Console.Write("Escoga un video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(profile.PlaylistFavoritosVideos[index], profile.PlaylistFavoritosVideos, database, user, profile);
                                    }
                                }
                                else
                                {
                                    player.PlayVideo(profile.PlaylistFavoritosVideos[player.RandomMult(profile, 1, "Fav")], profile.PlaylistFavoritosVideos, database, user, profile);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR[!]~How did you get here? WTF");
                            }
                        }
                        else if (play == "FollowedPlaylist") 
                        {
                            Console.Write("Do you wish to play a song (s) or a video (v)?");
                            string sv;
                            do
                            {
                                sv = Console.ReadLine();
                                if (sv != "s" || sv != "v")
                                    Console.Write("ERROR[!]~ Select a Valid command please: ");
                            } while (sv != "s" || sv != "v");
                            if(sv == "s")
                            {
                                int i = 0;
                                foreach(PlayList p in profile.FollowedPlayList)
                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = profile.FollowedPlayList[numPl].NamePlayList;
                                if(user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random song? or select it? (r/s): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" || rndSel != "s");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(profile, 0, "Foll");
                                        player.PlaySong(profile.FollowedPlayList[rnd].DicCanciones[nPl][rnd], profile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, profile);
                                    }
                                    else
                                    {
                                        for(int j = 0; j < profile.FollowedPlayList[numPl].DicCanciones[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, profile.FollowedPlayList[numPl].DicCanciones[nPl][j].Name);
                                        }
                                        
                                        Console.Write("Choose a song: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(profile.FollowedPlayList[numPl].DicCanciones[nPl][index], profile.FollowedPlayList[numPl].DicCanciones[nPl], database, user, profile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(profile, 0, "Foll");
                                    player.PlaySong(profile.FollowedPlayList[rnd].DicCanciones[nPl][rnd], profile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, profile);
                                }
                            }
                            else
                            {
                                int i = 0;
                                foreach (PlayList p in profile.FollowedPlayList)
                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = profile.FollowedPlayList[numPl].NamePlayList;
                                if (user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random video? or select it? (r/v): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" || rndSel != "v");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(profile, 1, "Foll");
                                        player.PlayVideo(profile.FollowedPlayList[rnd].DicVideos[nPl][rnd], profile.FollowedPlayList[rnd].DicVideos[nPl], database, user, profile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < profile.FollowedPlayList[numPl].DicVideos[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, profile.FollowedPlayList[numPl].DicVideos[nPl][j].Name);
                                        }

                                        Console.Write("Choose a video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(profile.FollowedPlayList[numPl].DicVideos[nPl][index], profile.FollowedPlayList[numPl].DicVideos[nPl], database, user, profile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(profile, 1, "Foll");
                                    player.PlayVideo(profile.FollowedPlayList[rnd].DicVideos[nPl][rnd], profile.FollowedPlayList[rnd].DicVideos[nPl], database, user, profile);
                                }
                            }
                        }
                        else if(play == "GlobalPlayList")
                        {
                            Console.Write("Do you wish to play a song (s) or a video (v)?");
                            string sv;
                            do
                            {
                                sv = Console.ReadLine();
                                if (sv != "s" || sv != "v")
                                    Console.Write("ERROR[!]~ Select a Valid command please: ");
                            } while (sv != "s" || sv != "v");
                            if (sv == "s")
                            {
                                int i = 0;
                                foreach (PlayList p in listPlayListGlobal)
                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = listPlayListGlobal[numPl].NamePlayList;
                                if (user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random song? or select it? (r/s): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" || rndSel != "s");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(profile, 0, "Gl");
                                        player.PlaySong(listPlayListGlobal[rnd].DicCanciones[nPl][rnd], listPlayListGlobal[rnd].DicCanciones[nPl], database, user, profile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < listPlayListGlobal[numPl].DicCanciones[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, listPlayListGlobal[numPl].DicCanciones[nPl][j].Name);
                                        }

                                        Console.Write("Choose a song: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(listPlayListGlobal[numPl].DicCanciones[nPl][index], listPlayListGlobal[numPl].DicCanciones[nPl], database, user, profile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(profile, 0, "Gl");
                                    player.PlaySong(listPlayListGlobal[rnd].DicCanciones[nPl][rnd], profile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, profile);
                                }
                            }
                            else
                            {
                                int i = 0;
                                foreach (PlayList p in listPlayListGlobal)
                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = listPlayListGlobal[numPl].NamePlayList;
                                if (user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random video? or select it? (r/v): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" || rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" || rndSel != "v");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(profile, 1, "Gl");
                                        player.PlayVideo(listPlayListGlobal[rnd].DicVideos[nPl][rnd], listPlayListGlobal[rnd].DicVideos[nPl], database, user, profile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < listPlayListGlobal[numPl].DicVideos[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, listPlayListGlobal[numPl].DicVideos[nPl][j].Name);
                                        }

                                        Console.Write("Choose a video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(listPlayListGlobal[numPl].DicVideos[nPl][index], listPlayListGlobal[numPl].DicVideos[nPl], database, user, profile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(profile, 1, "Gl");
                                    player.PlayVideo(listPlayListGlobal[rnd].DicVideos[nPl][rnd], listPlayListGlobal[rnd].DicVideos[nPl], database, user, profile);
                                }
                            }
                        }
                        break;
                    case "VI":
                        Console.WriteLine("Logged Out");
                        ret = 0;
                        database.Save_Users(listUserGlobal);
                        x = false;
                        Console.Clear();
                        break;
                    case "VII":
                        ret = 1;
                        database.Save_Users(listUserGlobal);
                        x = false;
                        break;
                }
            }
            return ret;
        }

        //tenemos que decidir si esta clase sera de inputs y outputs, o la que hace de reproductor.
        public void DisplayGlobalMult(int typeMult, DataBase database)
        {
            if (typeMult == 0)
            {
                List<Song> ListSongsGlobal = database.Load_Songs();
                for (int i = 0; i < ListSongsGlobal.Count(); i++)
                {
                    Console.WriteLine("Cancion {0}", i+1);
                    Console.WriteLine(ListSongsGlobal[i].DisplayInfoSong() + "\n");
                }
            }
            else if(typeMult == 1)
            {
                List<Video> ListVideosGlobal = database.Load_Videos();
                for (int i = 0; i < ListVideosGlobal.Count(); i++)
                {
                    Console.WriteLine("Video {0}", i + 1);
                    Console.WriteLine(ListVideosGlobal[i].DisplayInfoVideo() + "\n");
                }
            }
            else if(typeMult == 2)
            {
                List<PlayList> ListPLsGlobal = database.Load_PLs();
                for (int i = 0; i < ListPLsGlobal.Count(); i++)
                {
                    Console.WriteLine("Playlist {0}", i + 1);
                    Console.WriteLine(ListPLsGlobal[i].DisplayInfoPlayList() + "\n");
                }
            }

        }

        public void DisplayPlaylists(List<PlayList> playlist)
        {
            for (int i = 0; i < playlist.Count(); i++)
            {
                Console.WriteLine(i + ") " + playlist[i].InfoPlayList());
            }
        }
        public void AccountSettings(User user)
        {
            Console.WriteLine("Username: " + user.AccountSettings()[0] + "\n");
            Console.WriteLine("Password: " + user.AccountSettings()[1] + "\n");
            Console.WriteLine("Email: " + user.AccountSettings()[2] + "\n");
            Console.WriteLine("Account type: " + user.AccountSettings()[3] + "\n");
            
        }
        public void ProfileSettings(Profile profile)
        {
            Console.WriteLine("Name: " + profile.ProfileSettings()[0] + "\n");
            Console.WriteLine("Profile Type: " + profile.ProfileSettings()[1] + "\n");
            Console.WriteLine("Profile Pic: " + profile.ProfileSettings()[2] + "\n");
            Console.WriteLine("Gender: " + profile.ProfileSettings()[3] + "\n");
            Console.WriteLine("Age: " + profile.ProfileSettings()[4] + "\n");
        }


        public List<string> SearchEngine(string searching, string type, DataBase dataBase)
        {
            
            List<string> searchEngine = new List<string>();
            int num_v = 0;


            if (type == "Songs")
            {
                List<Song> listSongsGlobal = dataBase.Load_Songs();
                for(int i = 0; i < listSongsGlobal.Count(); i++)
                {
                    for (int x = 0; x < listSongsGlobal[i].InfoSong().Count(); x++)
                    {
                        if (listSongsGlobal[i].InfoSong()[x] == searching)
                        {
                            searchEngine.Add(listSongsGlobal[i].SearchedInfoSong() + ", Position: " + (i+1)); //num_s es un int y no me patalea, si tira error es aca. Usar remove leguer
                        }
                    }
                }
            }
            else if(type == "Videos")
            {
                List<Video> listVideosGlobal = dataBase.Load_Videos();
                for (int i = 0; i < listVideosGlobal.Count(); i++)
                {
                    for (int x = 0; x < listVideosGlobal[i].InfoVideo().Count(); x++)
                    {
                        if (listVideosGlobal[i].InfoVideo()[x] == searching)
                        {
                            searchEngine.Add(listVideosGlobal[i].SearchedInfoVideo() + ", Position: " + (i+1)); //num_s es un int y no me patalea, si tira error es aca. Usar remove leguer
                        }
                    }
                }
            }
            else if(type == "Users")
            {
                List<User> diceUserGlobal = dataBase.Load_Users();

                for(int i = 0; i < diceUserGlobal.Count(); i++)
                {
                    for (int x = 0; x < diceUserGlobal[i].infoUser().Count(); x++)
                    {
                        if (diceUserGlobal[i].infoUser()[x] == searching)
                        {
                            if(diceUserGlobal[i].Privacy != true) searchEngine.Add(diceUserGlobal[i].SearchedInfoUser() + ", Position: " + (i + 1));
                            else if (diceUserGlobal[i].Privacy == true) searchEngine.Add(diceUserGlobal[i].SearchedInfoUser() + ", Position: " + "???");
                        }
                    }
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

        public List<string> AskInfoMult(int type)
        {
            List<string> infoMult = new List<string>();
            if(type == 0)
            {
                Console.Write("Song name: ");                                                                               string n = Console.ReadLine();
                Console.Write("Song artist or artists(if this is the case separate them by '-', Bad Bunny-Ricky Martin: "); string art = Console.ReadLine();
                Console.Write("Album: ");                                                                                   string alb = Console.ReadLine();
                Console.Write("Discography: ");                                                                             string disc = Console.ReadLine();
                Console.Write("Song Gender: ");                                                                             string gen = Console.ReadLine();
                Console.Write("Publish date (dd/mm/aa): ");                                                                 string date = Console.ReadLine();
                Console.Write("Studio: ");                                                                                  string std = Console.ReadLine();
                Console.Write("Song Duration (format: min.seg)(double): ");                                                 string dur = Console.ReadLine();
                Console.Write("Song Format(.mp3 || .wav): ");                                                               string format = Console.ReadLine();
                Console.Write("Song lyrics(write): ");                                                                      string lyr = Console.ReadLine();


                if ((lyr == "y") || (lyr == "Y"))
                    lyr = "true";
                else 
                    lyr = "false";

                infoMult = new List<string>() { n, art, alb, disc, gen, date, std, dur, lyr, format };
            }
            else if(type == 1)
            {
                Console.Write("Video Name: ");                                                                                    string n = Console.ReadLine();
                Console.Write("Video actor or actors(if this is the case separate them by '-', Bad Bunny-Ricky Martin: ");        string act = Console.ReadLine();
                Console.Write("Video director or directors(if this is the case separate them by '-', Bad Bunny-Ricky Martin: ");  string dir = Console.ReadLine();
                Console.Write("Publish date (dd/mm/aa): ");                                                                       string date = Console.ReadLine();
                Console.Write("Video Dimension (16:9): ");                                                                        string dim = Console.ReadLine();
                Console.Write("Video Quality");                                                                                   string cal = Console.ReadLine();
                Console.Write("Video category(number) (0 = all espectator, 16 = above 16 years, etc.): ");                        string cat = Console.ReadLine();
                Console.Write("Video Description: ");                                                                             string des = Console.ReadLine();
                Console.Write("Video Duration(format: min.seg)(double): ");                                                       string dur = Console.ReadLine();
                Console.Write("Video Format(.mp4 || .mov): ");                                                                    string format = Console.ReadLine();
                Console.Write("Video Image(y/n): ");                                                                              string im = Console.ReadLine();
                Console.Write("Video subtitles(write): ");                                                                        string sub = Console.ReadLine();
                if ((im == "y") || (im == "Y"))
                    im = "true";
                else
                    im = "false";

                if ((sub== "y") || (sub == "Y"))
                    sub = "true";
                else
                    sub = "false";

                infoMult = new List<string>() { n, act, dir, date, dim, cal, cat, des, im, dur, sub, format };
            }
            else if(type == 2)
            {
                Console.Write("Escriba el nombre de la playlist: ");                     string n = Console.ReadLine();
                Console.Write("Quiere que su playlist sea de cancion o video? (c/v): "); string choice = Console.ReadLine();
                string format = null;

                if (choice == "c" || choice == "C")
                {
                    Console.Write("Escriba el formato de la playlist de cancion (.mp3|.wav): ");
                    format = Console.ReadLine();
                }
                else if (choice == "v" || choice == "V")
                {
                    Console.Write("Escriba el formato de la playlist de video (.mp4|.mov): ");
                    format = Console.ReadLine();
                }

                    infoMult = new List<string>() { n, format};
            }
            return infoMult;
            
        }
    }
}

