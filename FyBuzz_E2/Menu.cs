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
        //ATRIBUTOS:

        protected List<string> filters;
        protected List<string> badWords = new List<string>() { "fuck", "sex", "niggas", "sexo", "ass", "nigger", "culo", "viola", "violar", "spank", "puta", "hooker", "perra", "hoe", "cocaina", "alchohol", "blunt", "weed", "marihuana", "lcd", "kush", "krippy", "penis", "dick", "cock", "shit", "percocet" };
        public List<Song> searchedSongs;
        public List<Video> searchedVideos;
        User user = new User();
        DataBase database = new DataBase();
        Player player = new Player();
        //--------------------------------------------------------------------------------------------------

        //METODOS:

        //METODOS DISPLAY
        //--------------------------------------------------------------------------------------------------

        //PRIMER DISPLAY
        public User DisplayLogin(List<User> userDataBase)                                //Este método genera un display del login, donde se encuentra la parte para registrarse, login o salir de la app.
        {
            //Creamos una instancia de server
            Server server = new Server(database);
            bool x = false;

            while (x == false)
            {
                //Menu de inicio de FyBuzz
                Console.Clear();
                Console.WriteLine("------------Welcome to FyBuZz--------------");
                Console.WriteLine("I) Log-In with a existing account.");
                Console.WriteLine("II) Register.");
                Console.WriteLine("III) Close App.");

                string dec = Console.ReadLine();
                switch (dec)
                {

                    case "I":
                        //Cargamos la informacion de los usuarios.
                        database.Load_Users();

                        //Pedimos el username y la contraseña
                        Console.Write("Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        //Utilizamos la funcion LogIn de database para confirmar de que el nombre de usuario y la contraseña son correctos.
                        if (database.LogIn(username, password, userDataBase) != null)
                        {
                            Console.WriteLine("Login Succesfull.");
                            Thread.Sleep(1000);
                            Console.Beep();

                            x = true;
                            user = database.LogIn(username, password, userDataBase);
                        }

                        //Condicion que ocurre en caso de que el nombre de usuario o la contraseña es incorrecto.
                        else
                        {
                            Console.WriteLine("ERROR[!] Invalid Username or Password");
                            Thread.Sleep(2000);
                        }

                        break;


                    case "II":
                        //Utiliza el método Register de Server para registrar al usuario.
                        server.Register(user, userDataBase);

                        //Guarda la información del usuario.
                        database.Save_Users(userDataBase);

                        x = false;
                        break;

                    case "III":
                        //Devuelve null a la funcion para que no se pueda ejecutar nada mas en la clase Program
                        return null;
                }
            }

            Console.Clear();
            return user; //Devuelve el usuario al que hizo login.
        }


        //SEGUNDO DISPLAY
        public Profile DisplayProfiles(User user, List<User> userDataBase)                //Este método Realiza un display en consola de los perfiles para escoger uno.
        {
            Console.WriteLine("Welcome: " + user.Username + "\n");

            bool x = true;
            int u = 0;

            //Este loop Busca el indice en donde se encuentra el usuario para poder obtener la informacion del objeto más tarde.
            foreach (User usr in userDataBase)
            {
                if (usr.Username == user.Username)
                {
                    break;
                }
                u++;
            }

            Console.WriteLine("---------Profiles----------");

            //Si es que el usuario no tiene perfiles, entonces es llevado a esta condición para crear uno.
            if (userDataBase[u].Perfiles.Count() == 0)
            {
                //Se le pide la información necesaria
                Console.Write("Select your gender(M/F): ");
                string gender = Console.ReadLine();

                Console.Write("Select your age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Select your Profile Type(creator/viewer): ");
                string profileType = Console.ReadLine();

                //Se crea el perfil y se agrega a la lista de perfiles que tiene el objeto user
                Profile profile = new Profile(user.Username, ".JPEG", profileType, user.Email, gender, age);
                user.Perfiles.Add(profile);
            }

            //En caso de que el usuario tenga minimo un perfil creado, se le dirije a esta condición.
            else
            {
                while (x == true)
                {
                    //Se muestra el menú de perfiles
                    Console.WriteLine("I) Choose a profile\nII) Create Profile\nIII) Close App.");
                    string dec = Console.ReadLine();

                    //Condicion utilizada para mostrar todos los perfiles del usuario y seleccionar uno.
                    if (dec == "I")
                    {
                        Console.WriteLine("List of profiles:");

                        //Genera una lista de perfiles para que el usuario pueda escoger
                        int i;
                        for (i = 0; i < userDataBase[u].Perfiles.Count(); i++)
                        {
                            Console.WriteLine("{0}).- {1}", i + 1, userDataBase[u].Perfiles[i].ProfileName);
                        }

                        //Permite al usuario escoger un perfil
                        Console.WriteLine("Choose a profile (Number):");
                        int index = int.Parse(Console.ReadLine()) - 1;

                        //Devuelve el perfil escogido por el usuario
                        return userDataBase[u].Perfiles[index];
                    }


                    else if (dec == "II")
                    {
                        //Se entra a esta condición si el usuario es Premium
                        if (user.Accountype == "premium")
                        {
                            Console.WriteLine("Create a profile:");

                            //Se pide toda la información necesaria para poder crear un perfil
                            Console.Write("Profile name: ");
                            string pname = Console.ReadLine();

                            Console.Write("Profile pic: ");
                            string ppic = Console.ReadLine();

                            Console.Write("Profile type(creator/viewer): ");
                            string ptype = Console.ReadLine();

                            Console.Write("Profile gender (M/F): ");
                            string pgender = Console.ReadLine();

                            Console.Write("Profile age: ");
                            int page = int.Parse(Console.ReadLine());

                            //Se utiliza la funcion CreateProfile de la clase User para crear el perfil
                            userDataBase[u].CreateProfile(pname, ppic, ptype, user.Email, pgender, page);

                            Console.Clear();
                        }

                        //Condición utilizada si el usuario es Standard
                        else if (user.Accountype == "standard")
                        {
                            //Menciona de que los usuarios "Standard" pueden tener un solo perfil (El default)
                            Console.WriteLine("ERROR [!] Standard Account Types, you can only have one profile.\nTry Upgrading to Premium Account.");

                            //Menciona si es que quiere cambiarse a "Premium"
                            Console.Write("Do you want to change your Account Type to premium?(y/n): ");

                            //Utiliza un do para poder corregir el error de que una persona escriba algo distinto a lo que se le pide.
                            string premium;
                            do
                            {

                                premium = Console.ReadLine();
                                if (premium != "y" && premium != "n")
                                    Console.Write("Please select one of these commands (y/n): ");

                            } while (premium != "y" && premium != "n");


                            //Si es que coloca que si, Cambia a premium
                            if (premium == "y") userDataBase[u].Accountype = "premium";

                            //En caso de que coloque que no, el programa continua.
                            else continue;
                        }

                        //Condición que se usa en caso de que el usuario es un "Admin", los admin no necesitan más de un perfil
                        else if (user.Accountype == "admin")
                        {
                            Console.WriteLine("ERROR [!] Admin Account Types, you can only have one profile.");
                            Thread.Sleep(1000);
                        }
                    }

                    //Condición utilizada para cerrar la aplicación
                    else if (dec == "III")
                    {
                        return null;
                    }
                }
            }

            Console.Clear();
            return null;
        }


        //TERCER DISPLAY
        //Método en donde se tiene el resto de la App
        public int DisplayStart(Profile userProfile, User usr, List<User> listUserGlobal, List<Song> listSongsGlobal, List<Song> DownloadSongs, List<Video> listVideosGlobal, List<PlayList> listPlayListGlobal, List<PlayList> listPlaylistPriv) // solo funciona si DisplayLogIn() retorna true se ve en program.
        {
            //Atributos utilizados en el método
            int ret = 0;
            Server server = new Server(database);

            // mostrará todas las playlist del usuario, si es primera vez que ingresa estara la playlist general y la favorita(esta sin nada)
            // es la lista global de playlist que viene de database, pero hay que conectarla

            //objeto playlist de canciones favoritas
            PlayList favSongs = new PlayList(".mp3", "FavoriteSongs", usr.Username, userProfile.ProfileName);
            Dictionary<string, List<Song>> playlistFavSongs = favSongs.DicCanciones;

            //objeto Playlist de videos favoritos
            PlayList favVideos = new PlayList(".mp3", "FavoriteVideos", usr.Username, userProfile.ProfileName);
            Dictionary<string, List<Video>> playlistFavVideos = favVideos.DicVideos;

            //Lista de todas las playlists
            List<PlayList> followedPL = userProfile.FollowedPlayList;

            //Si seguimos la usuario seguiremos todas sus playlist (REVISAR ESTO)


            Console.Clear();

            //incio del menú
            Console.WriteLine("------------Welcome to FyBuZz--------------");
            bool x = true;

            while (x == true)
            {
                //Menú de FyBuzz
                Console.WriteLine("I) Search Songs, Videos or Users."); //Faltaria la bsuqueda de gente.
                Console.WriteLine("II) Add/Show Songs, Videos or Playlists.");
                Console.WriteLine("III) Display all Playlists.");
                Console.WriteLine("IV) Account Settings/ Profile Settings.");
                Console.WriteLine("V) Play a Playlist.");
                Console.WriteLine("VI) LogOut from Profile.");
                Console.WriteLine("VII) CloseApp.");
                Console.WriteLine("VIII) Admin Menu.");

                //Condicion para poder corregir errores creados por el usuario
                string dec;
                do
                {
                    dec = Console.ReadLine();
                    if (dec != "I" && dec != "II" && dec != "III" && dec != "IV" && dec != "V" && dec != "VI" && dec != "VII" && dec != "VIII")
                        Console.Write("Porfavor escoga un comando correcto (I/II/III/IV/V/VI/VII/VIII): ");

                } while (dec != "I" && dec != "II" && dec != "III" && dec != "IV" && dec != "V" && dec != "VI" && dec != "VII" && dec != "VIII");

                switch (dec)
                {
                    // (REVISAR DESPUES) Mejorar el metodo de busqueda para que busque canciones que se parezca
                    case "I":

                        //Solicita busqueda de canciones, videos o usuarios.
                        Console.WriteLine("What would you like to search? (Songs/Videos/Users)");

                        //Condicion para poder corregir errores creados por el usuario
                        string type;
                        do
                        {
                            type = Console.ReadLine();
                            if (type != "Songs" && type != "Videos" && type != "Users")
                                Console.Write("Please select one of these options (Songs/Videos/Users): ");

                        } while (type != "Songs" && type != "Videos" && type != "Users");


                        //Entra a esta condición si es que el usuario escoge "Songs"
                        if (type == "Songs")
                        {
                            //Se le pregunta al usuario que desea buscar
                            Console.WriteLine("Type what you want to search...{name, artist, album, discography, studio, gender}");
                            string search = Console.ReadLine();

                            //Se obtiene una lista de strings con el resultado de las cosas que se encontraron
                            List<string> searchEngine = SearchEngine(search, type, database);

                            //
                            List<int> indexglobal = new List<int>();

                            //Se entra a esta condición si es que la lista no se encuentra vacía.
                            Console.WriteLine("Searched Songs: ");
                            if (searchEngine.Count() != 0)
                            {
                                //Se imprimen todos los elementos de la lista que se encuentra en searchEngine
                                for (int i = 0; i < searchEngine.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                                }

                                //Se le pide al usuario que escoga un numero de la lista de canciones
                                Console.WriteLine("Searched Songs, choose the position of the song you want to hear...");
                                int indice = int.Parse(Console.ReadLine()) - 1;

                                //Se obtiene la cancion de la lisa global de canciones
                                Song song = listSongsGlobal[indice]; 

                                //Realiza un foreach de las palabras que estan en la lista de malas palabras
                                int cont = 0;
                                foreach (string word in badWords)
                                {
                                    //Si es que una palabra mala se encuentra dentro de la lyrics, y la edad es menor a 16, se entra a esta condición
                                    if (song.Lyrics.Contains(word) == true && userProfile.Age < 16)
                                    {
                                        Console.WriteLine("ERROR[!] This content is age restricted");
                                        Thread.Sleep(1000);

                                        cont++;
                                        break;
                                    }
                                }

                                //Condicion para limitar a los usuarios restringidos
                                if (cont == 0)
                                {
                                    //se le pregunta al usuario si es que quiere escuchar, descargar o añadir a una playlist
                                    Console.WriteLine("Do you want to: \nI)Listen.\nII)Download.\nIII)Add to Playlist.");
                                    string want = Console.ReadLine();

                                    switch (want)
                                    {
                                        case "I":
                                            //Condicion para que no entren los usuarios restringidos
                                            if (cont == 0)
                                            {
                                                //Comienza la canción
                                                Console.Clear();
                                                player.PlaySong(song, null, database, usr, userProfile);
                                            }
                                            break;

                                        case "II":
                                            //Se descarga la canción y se guarda en la lista y en el archivo
                                            Console.WriteLine("Song is Downloading...");
                                            Thread.Sleep(1000 * 5);

                                            Console.WriteLine("Song Downloaded.");
                                            Thread.Sleep(1000);

                                            DownloadSongs.Add(song);
                                            database.Save_DSongs(DownloadSongs);

                                            break;

                                        case "III":

                                            //Condición que observa si es que el usuario ha creado una playlist
                                            if (userProfile.CreatedPlaylist.Count() != 0)
                                            {

                                                //Muestra un listado de las playlist que el usuario ha creado para que escoga una.
                                                int i = 1;
                                                Console.WriteLine("Where do you want to add this song?");
                                                foreach (PlayList playList in userProfile.CreatedPlaylist)
                                                {
                                                    Console.WriteLine(i + ") " + playList.DisplayInfoPlayList());
                                                    i++;
                                                }

                                                //Le pide al usuario escoger una playlist
                                                Console.WriteLine("Please select de number of the Playlist...");
                                                int createdPlaylistIndex = int.Parse(Console.ReadLine()) - 1;

                                                //Agrega la cancion
                                                userProfile.CreatedPlaylist[createdPlaylistIndex].Songs.Add(song);
                                            }

                                            //le menciona al usuario de que no tiene una playlist creada
                                            else
                                                Console.WriteLine("You don´t have any Playlists, please create one...");

                                            break;
                                    }
                                }

                                Console.WriteLine("\n");

                            }
                        }
                        else if (type == "Videos")
                        {
                            //Le pregunta al usuario que quiere buscar
                            Console.WriteLine("Type what you want to search...{ name, actors, directors, quality, category, rated, description}");
                            string search = Console.ReadLine();

                            //Se obtiene una lista de strings con el resultado de las cosas que se encontraron
                            List<string> searchEngine = SearchEngine(search, type, database);

                            //
                            List<int> indexglobal = new List<int>();

                            //Si es que la lista no esta vacía, entra a la condición
                            if (searchEngine.Count != 0)
                            {
                                //Muestra en la consola un listado de los videos que se encontraron con el criterio que colocó el usuario.
                                Console.WriteLine("Searched Videos: ");
                                for (int i = 0; i < searchEngine.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                                }

                                //Se le pide al usuario que escoga un video de este listado.
                                Console.WriteLine("Searched Songs, choose the position of the song you want to hear...");
                                int indice = int.Parse(Console.ReadLine()) - 1;

                                //Se obtiene el video de la lista global de videos
                                Video video = listVideosGlobal[indice];

                                //Por cada palabra que esta en la lista de malas palabras
                                int cont = 0;
                                foreach (string word in badWords)
                                {

                                    //Si es que la palabra de los subtitulos se encuentra dentro de esta lista y el usuario es menor a 16 años, entonces esta restringido a ver el video
                                    if ((video.Subtitles.Contains(word) && userProfile.Age < 16) || (int.Parse(video.Category) >= userProfile.Age))
                                    {
                                        Console.WriteLine("ERROR[!] This content is age restricted");
                                        Thread.Sleep(1000);
                                        cont++;
                                        break;
                                    }
                                }

                                //Condición para que no entren los usuarios restringidos
                                if (cont == 0)
                                {

                                    //Se le pregunta al usuario si quiere ver el video o agregarlo a una playlist
                                    Console.WriteLine("Do you want to: \nI)Watch.\nII)Add to Playlist.");
                                    string want = Console.ReadLine();

                                    switch (want)
                                    {
                                        case "I":

                                            //Si es que el usuario no es restringido, entonces ocurre un play al video.
                                            if (cont == 0)
                                            {
                                                Console.Clear();
                                                player.PlayVideo(video, null, database, usr, userProfile);
                                            }
                                            break;

                                        case "II":

                                            //Si es que el usuario ha reado alguna playlist, entonces entra a la condición
                                            if (userProfile.CreatedPlaylist.Count() != 0)
                                            {
                                                //se imprime un listado de las playlists creadas por el usuario.   
                                                int i = 1;
                                                Console.WriteLine("Where do you want to add this video?");
                                                foreach (PlayList playList in userProfile.CreatedPlaylist)
                                                {
                                                    Console.WriteLine(i + ") " + playList.DisplayInfoPlayList());
                                                    i++;
                                                }

                                                //se le pide al usuario que escriba el numero en donde se encuentra la playlist de la lista.
                                                Console.WriteLine("Please select a Playlist by writing their number on the list...");
                                                int createdPlaylistIndex = int.Parse(Console.ReadLine()) - 1;

                                                //Se agrega el video a la playlist.
                                                userProfile.CreatedPlaylist[createdPlaylistIndex].Videos.Add(video);
                                            }

                                            break;
                                    }

                                }

                                Console.WriteLine("\n");
                            }
                        }

                        else
                        {
                            //Le pide al usuario que escriba el nombre de un usuario que quiera buscar.
                            Console.WriteLine("Type the user you want to search...");
                            string search = Console.ReadLine();

                            //Se genera na lista de string con la información que se encuentra de acorde a lo que escribío el usuario
                            List<string> searchEngine = SearchEngine(search, type, database);

                            //Si es que no se creó una lista vacía...
                            if (searchEngine.Count != 0)
                            {

                                //Imprime un listado de los usuarios
                                Console.WriteLine("Searched Users: ");
                                for (int i = 0; i < searchEngine.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ") " + searchEngine[i]);
                                }

                                //Le pide que escriba el numero en donde se encuentra el usuario en la lista para poder seguirlo
                                Console.WriteLine("Searched Users, choose the position of the user you want to follow...");
                                int indice = int.Parse(Console.ReadLine()) - 1;

                                //Se crea una instancia del usuario
                                User u = listUserGlobal[indice];

                                //Se crea una lista de todos los usuarios que sigue este usuario.
                                List<string> listuser = usr.FollowingList;

                                //Se transforma la información a un string para poder manipularla
                                string tryingToFollow = u.Username;


                                //Si es que la lista de usuarios que sigue esta cuenta, no contiene al usuario que se trata de seguir, se entra a la condición
                                if (listuser.Contains(tryingToFollow) == false)
                                {

                                    //se aumenta la cantidad de Followers y la cantidad de Following.
                                    u.Followers = u.Followers + 1;
                                    usr.Following = usr.Following + 1;

                                    //Si es que el usuario ha creado una playlist. entonces entra a la condición
                                    if (u.ProfilePlaylists != null)
                                    {

                                        //por cada playlist que el usuario tenga, se agrega a la lista de playlist seguidas.
                                        foreach (PlayList Pls in u.ProfilePlaylists)
                                        {
                                            followedPL.Add(Pls);

                                        }
                                    }

                                    //En caso de que no cumple con la condicion anterior, entonces se menciona que el usuario no ha creado ninguna playlist.
                                    else
                                        Console.WriteLine("This user hasn´t created any PlayList");

                                    //Se agrega el usuario a la lista de seguidores.
                                    usr.FollowingList.Add(u.Username);

                                    //Se menciona por consola que esta siguiendo a esta cuenta.
                                    Console.WriteLine("\nFollowed: " + u.SearchedInfoUser());
                                    Thread.Sleep(2000);

                                    //Se guarda la lista de usuarios global
                                    database.Save_Users(listUserGlobal);

                                }

                                //En caso de que no entre a la condición anterior, significa que ya sigue al usuario
                                else
                                    Console.WriteLine("You already follow this user.");

                                Thread.Sleep(1000);

                                Console.WriteLine("\n");
                            }
                        }
                        Console.Clear();
                        /*database.Save_Users(database.UserDataBase);*/
                        break;

                    case "II":
                        Console.Clear();

                        //Le pregunta al usuario que desea agregar o si quiere ver una lista de multimedia específica
                        Console.Write("What do you wish to add? (song(0), video(1), Playlist(2))\nDo you wish to See a list of a specified multimedia? (song(3), video(4), Playlist(5)): ");
                        
                        //Condición para ver corregir errores del usuario
                        int opc;
                        do
                        {
                            opc = int.Parse(Console.ReadLine());
                            if (opc != 0 && opc != 1 && opc != 2 && opc != 3 && opc != 4 && opc != 5)
                                Console.Write("Please select one of the following options (0/1/2/3/4/5): ");

                        } while (opc != 0 && opc != 1 && opc != 2 && opc != 3 && opc != 4 && opc != 5);

                        //Condicion utilizada en caso de que el usuario escgoe una de las 3 primerasa opciones.
                        if (opc == 0 || opc == 1 || opc == 2)
                        {

                            //Si es que el perfil del usuario es creator o admin, entonces entra a esta condición
                            if (userProfile.ProfileType == "creator" || userProfile.ProfileType == "admin")
                            {
                                //Genera una lista de strings en donde se busca la información de la multimedia.
                                List<string> infoMult = AskInfoMult(opc);

                                //Sacamos el nombre de usuario y el nombre de perfil de la cuenta.
                                string username = usr.Username;
                                string profileUser = userProfile.ProfileName;

                                //si es que la opción escogida es crear una playlist, entra a esta condición.
                                if (opc == 2)
                                {
                                    Console.WriteLine("Do you want the Playlist to be private or public?(y/n)");
                                }

                                //condición que corrige errores creados por el usuario
                                string priv;
                                do
                                {
                                    priv = Console.ReadLine();
                                    if (priv != "y" && priv != "n")
                                        Console.Write("Please choose one of these options (y/n): ");

                                } while (priv != "y" && priv != "n");


                                //Se obtiene una descripción null si es que la multimedia se agregó correctamente.
                                string description = database.AddMult(opc, infoMult, listSongsGlobal, listPlayListGlobal, listVideosGlobal, username, profileUser, priv, listPlaylistPriv);
                                
                                //Entra a la condición si es que se agregó la multimedia correctamente.
                                if (description == null)
                                {
                                    //Le menciona al usuario que se agregó correctamente la multimedia
                                    Console.WriteLine("Multimedia has been registered into the system!");
                                    Thread.Sleep(3000);
                                }

                                //En caso de que no se agregó correctamente la multimedia, menciona este error.
                                else
                                    Console.WriteLine("ERROR[!] ~{0}", description);

                                //Si es que La opcion fue canciones, se guarda la lista de canciones en el archivo
                                if (opc == 0)
                                    database.Save_Songs(listSongsGlobal);

                                //Si es que La opcion fue videos, se guarda la lista de videos en el archivo
                                else if (opc == 1) 
                                    database.Save_Videos(listVideosGlobal);

                                //Si la opción fue playlist, entra a esta condición
                                else if (opc == 2)
                                {
                                    //se realiza un loop de todas las playlist en la lista de playlist globales.
                                    foreach (PlayList playList in listPlayListGlobal)
                                    {
                                        //Si es que la playlist fue creada por el usuario y el perfil, entonces se agrega a la lista de claylist creadas de ese usuario.
                                        if (playList.Creator == usr.Username && playList.ProfileCreator == userProfile.ProfileName)
                                        {
                                            userProfile.CreatedPlaylist.Add(playList);
                                            usr.ProfilePlaylists.Add(playList);
                                        }
                                    }

                                    //Se busca tambien todas las playlist que estan en la lista de playlists privadas.
                                    foreach (PlayList playList in listPlaylistPriv)
                                    {
                                        if (playList.Creator == usr.Username && playList.ProfileCreator == userProfile.ProfileName)
                                        {
                                            userProfile.CreatedPlaylist.Add(playList);
                                            /*usr.ProfilePlaylists.Add(playList); Si la PL es privada no se agrega al usuario, por lo tanto no se puede seguir.*/
                                        }
                                    }

                                    //Se guardan las playlists en los archivos correspondientes.
                                    database.Save_PLs(listPlayListGlobal);
                                    database.Save_PLs_Priv(listPlaylistPriv);

                                }
                            }

                            //En caso de que el usuario no es creador o admin, entra a esta condición
                            else
                            { 
                                //menciona que el usuario no tiene permiso para crear multimedia.
                                Console.WriteLine("You do not have permission to add Multimedia.");
                                Thread.Sleep(1000);

                            }
                        }

                        //En caso de que escogió las ultimas tres opciones, entra a la condición
                        else if (opc == 3 || opc == 4 || opc == 5)
                        {
                            //entra al método en donde imprime un listado de la multimedia seleccionada.
                            DisplayGlobalMult(opc - 3, database);
                            Thread.Sleep(5000);
                        }

                        Console.Clear();
                        break;

                    case "III":
                        Console.Clear();
                   
                        //Entra a esta condición en caso de que las playlist favoritas no se encuentren vacías
                        if (playlistFavSongs != null || playlistFavVideos != null)
                        {
                            //muestra la informacionde las playlists favoritas.
                            Console.WriteLine("Liked Playlist:");
                            Console.WriteLine(favSongs.InfoPlayList());
                            Console.WriteLine("\n");
                            Console.WriteLine(favSongs.InfoPlayList());
                            Console.WriteLine("\n");
                        }

                        //En caso contrario, entra a esta condición mencionando que no tiene playlist favortias.
                        else
                            Console.WriteLine("You don´t have a Favorite Playlist.");

                        Console.WriteLine("-------------------------------------------------------");
                        Thread.Sleep(1000);

                        //En caso de que el usuario siga una playlist, entra a esta condición
                        if (followedPL.Count() != 0)
                        {
                            //Muestra la informacion de las playlist que el usuario sigue.
                            Console.WriteLine("Followed Playlist:");
                            foreach (PlayList pl in followedPL)
                            {

                                //Muestra la información básica de la playlist
                                Console.WriteLine(pl.DisplayInfoPlayList());

                                //Si es que el formato es el utilizado para canciones, entra a esta condición
                                if (pl.Format == ".mp3" || pl.Format == ".wav")
                                {
                                    //Imprime las canciones que se encuentran en la playlist.
                                    Console.WriteLine("\tSongs in Playlist:");
                                    foreach (Song song in pl.Songs)
                                    {
                                        Console.WriteLine("\t" + song.SearchedInfoSong());
                                    }
                                }

                                //Si es que el formato es el utilizado para videos, entra a esta condición
                                else if (pl.Format == ".mp4" || pl.Format == ".mov")
                                {
                                    //Imprime los videos que se encuentran en la playlist.
                                    Console.WriteLine("\tVideos in Playlist:");
                                    foreach (Video video in pl.Videos)
                                    {
                                        Console.WriteLine("\t" + video.SearchedInfoVideo());
                                    }
                                }
                            }
                        }

                        //Entra a esta condición en caso de que no sigue ninguna playlist.
                        else
                            Console.WriteLine("You don´t follow anyone Playlist.");

                        Console.WriteLine("-------------------------------------------------------");
                        Thread.Sleep(1000);

                        //Si es que el usuario ha creado alguna playlist, entra a esta condición.
                        if (userProfile.CreatedPlaylist.Count() != 0)
                        {

                            //Entra a un loop de las paylist creadas por el usuario
                            Console.WriteLine("Personal Playlist:");
                            foreach (PlayList playList in userProfile.CreatedPlaylist)
                            {

                                //Muestra la información básica de la playlist
                                Console.WriteLine(playList.DisplayInfoPlayList());

                                //Si es que el formato es el utilizado para canciones, entra a esta condición
                                if (playList.Format == ".mp3" || playList.Format == ".wav")
                                {
                                    //Imprime las canciones que se encuentran en la playlist.
                                    Console.WriteLine("\tSongs in Playlist:");
                                    foreach (Song song in playList.Songs)
                                    {
                                        Console.WriteLine("\t" + song.SearchedInfoSong());
                                    }
                                }

                                //Si es que el formato es el utilizado para videos, entra a esta condición
                                else if (playList.Format == ".mp4" || playList.Format == ".mov")
                                {
                                    //Imprime los videos que se encuentran en la playlist.
                                    Console.WriteLine("\tVideos in Playlist:");
                                    foreach (Video video in playList.Videos)
                                    {
                                        Console.WriteLine("\t" + video.SearchedInfoVideo());
                                    }
                                }
                                Console.WriteLine("\n");
                            }
                        }

                        //En caso de que el usuario no ha creado ninguna playlist, entonces entra a esta condición
                        else
                            Console.WriteLine("You don´t have any personal Playlist.");

                        Console.WriteLine("-------------------------------------------------------");
                        Thread.Sleep(1000);

                        //Muestra las playlist globales
                        Console.WriteLine("Global Playlist:");
                        DisplayGlobalMult(2, database);

                        Thread.Sleep(6000);
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("\n");
                        Console.Clear();
                        break;


                    case "IV":
                        Console.Clear();
                        Console.WriteLine("---------------------------");

                        //Le pregunta al usuario que settings quiere acceder.
                        Console.Write("Which settings do you want to acces?(Account/Profile): ");
                        string settings = Console.ReadLine();

                        //En caso de que el usuario escoge "Account", entra a esta condición
                        if (settings == "Account")
                        {
                            AccountSettings(usr);

                            //Nos muestra una lista de username
                            Console.WriteLine("Following list:");
                            foreach (String username in usr.FollowingList)
                            {
                                Console.WriteLine(username);
                            }

                            //Le pregunta al usuario si es que quiere cambiar la contraseña
                            Console.WriteLine("Do you want to change password? (y/n)");
                            string pass = Console.ReadLine();

                            //Condición utilizada en caso de que el usuario, decida cambiar la contraseña.
                            if (pass == "y")
                                server.ChangePassword(listUserGlobal);

                            //En caso contrario, entra a esta condición.
                            else
                            {
                                Console.Clear();
                                break;
                            }
                        }

                        //En caso de que el usuario entra a los settings de "Profile", entra a esta condición.
                        else if (settings == "Profile") 
                            ProfileSettings(userProfile);

                        //En caso contrario, por error del usuario, entra a esta condición
                        else
                            Console.WriteLine("ERROR[!] Invalid Command");

                        Thread.Sleep(1000);
                        Console.WriteLine("---------------------------");
                        Console.Clear();

                        break;

                    //REVISAR CASO 5 DESPUES
                    case "V":
                        Console.Clear();
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

                                        if (rndSel != "r" && rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" && rndSel != "s");


                                    if (rndSel == "r")
                                    {
                                        player.PlaySong(userProfile.PlaylistFavoritosSongs[player.RandomMult(userProfile, 0, "Fav")], userProfile.PlaylistFavoritosSongs, database, user, userProfile);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < userProfile.PlaylistFavoritosSongs.Count(); i++)
                                        {
                                            Console.WriteLine("{0}) {1}", i + 1, userProfile.PlaylistFavoritosSongs[i].Name);
                                        }
                                        Console.Write("Escoga una cancion: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(userProfile.PlaylistFavoritosSongs[index], userProfile.PlaylistFavoritosSongs, database, user, userProfile);
                                    }

                                }
                                else
                                {
                                    player.PlaySong(userProfile.PlaylistFavoritosSongs[player.RandomMult(userProfile, 0, "Fav")], userProfile.PlaylistFavoritosSongs, database, user, userProfile);
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
                                        if (rndSel != "r" && rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" && rndSel != "v");


                                    if (rndSel == "r")
                                    {
                                        player.PlayVideo(userProfile.PlaylistFavoritosVideos[player.RandomMult(userProfile, 1, "Fav")], userProfile.PlaylistFavoritosVideos, database, user, userProfile);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < userProfile.PlaylistFavoritosVideos.Count(); i++)
                                        {
                                            Console.WriteLine("{0}) {1}", i + 1, userProfile.PlaylistFavoritosVideos[i].Name);
                                        }
                                        Console.Write("Escoga un video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(userProfile.PlaylistFavoritosVideos[index], userProfile.PlaylistFavoritosVideos, database, user, userProfile);
                                    }
                                }
                                else
                                {
                                    player.PlayVideo(userProfile.PlaylistFavoritosVideos[player.RandomMult(userProfile, 1, "Fav")], userProfile.PlaylistFavoritosVideos, database, user, userProfile);
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

                                if (sv != "s" && sv != "v")
                                    Console.Write("ERROR[!]~ Select a Valid command please: ");
                            } while (sv != "s" && sv != "v");
                            if (sv == "s")
                            {
                                int i = 0;
                                foreach (PlayList p in userProfile.FollowedPlayList)

                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = userProfile.FollowedPlayList[numPl].NamePlayList;

                                if (user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random song? or select it? (r/s): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();

                                        if (rndSel != "r" && rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" && rndSel != "s");
                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(userProfile, 0, "Foll");
                                        player.PlaySong(userProfile.FollowedPlayList[rnd].DicCanciones[nPl][rnd], userProfile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, userProfile);
                                    }
                                    else
                                    {

                                        for (int j = 0; j < userProfile.FollowedPlayList[numPl].DicCanciones[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, userProfile.FollowedPlayList[numPl].DicCanciones[nPl][j].Name);
                                        }
                                        Console.Write("Choose a song: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(userProfile.FollowedPlayList[numPl].DicCanciones[nPl][index], userProfile.FollowedPlayList[numPl].DicCanciones[nPl], database, user, userProfile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(userProfile, 0, "Foll");
                                    player.PlaySong(userProfile.FollowedPlayList[rnd].DicCanciones[nPl][rnd], userProfile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, userProfile);
                                }
                            }
                            else
                            {
                                int i = 0;
                                foreach (PlayList p in userProfile.FollowedPlayList)

                                {
                                    Console.WriteLine("{0}) {1}", i + 1, p.NamePlayList);
                                    i++;
                                }
                                Console.Write("Type the playlist that you want to play: ");
                                int numPl = int.Parse(Console.ReadLine()) - 1;
                                string nPl = userProfile.FollowedPlayList[numPl].NamePlayList;
                                if (user.Accountype == "Premium")
                                {
                                    Console.Write("Do you want to play a random video? or select it? (r/v): ");
                                    string rndSel;
                                    do
                                    {
                                        rndSel = Console.ReadLine();
                                        if (rndSel != "r" && rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" && rndSel != "v");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(userProfile, 1, "Foll");
                                        player.PlayVideo(userProfile.FollowedPlayList[rnd].DicVideos[nPl][rnd], userProfile.FollowedPlayList[rnd].DicVideos[nPl], database, user, userProfile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < userProfile.FollowedPlayList[numPl].DicVideos[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, userProfile.FollowedPlayList[numPl].DicVideos[nPl][j].Name);
                                        }

                                        Console.Write("Choose a video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(userProfile.FollowedPlayList[numPl].DicVideos[nPl][index], userProfile.FollowedPlayList[numPl].DicVideos[nPl], database, user, userProfile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(userProfile, 1, "Foll");
                                    player.PlayVideo(userProfile.FollowedPlayList[rnd].DicVideos[nPl][rnd], userProfile.FollowedPlayList[rnd].DicVideos[nPl], database, user, userProfile);
                                }
                            }
                        }

                        else if (play == "GlobalPlayLists")
                        {
                            Console.Write("Do you wish to play a song (s) or a video (v)?");
                            string sv;
                            do
                            {
                                sv = Console.ReadLine();
                                if (sv != "s" && sv != "v")
                                    Console.Write("ERROR[!]~ Select a Valid command please: ");
                            } while (sv != "s" && sv != "v");
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
                                        if (rndSel != "r" && rndSel != "s")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/s): ");
                                    } while (rndSel != "r" && rndSel != "s");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(userProfile, 0, "Gl");
                                        player.PlaySong(listPlayListGlobal[rnd].DicCanciones[nPl][rnd], listPlayListGlobal[rnd].DicCanciones[nPl], database, user, userProfile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < listPlayListGlobal[numPl].DicCanciones[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, listPlayListGlobal[numPl].DicCanciones[nPl][j].Name);
                                        }

                                        Console.Write("Choose a song: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlaySong(listPlayListGlobal[numPl].DicCanciones[nPl][index], listPlayListGlobal[numPl].DicCanciones[nPl], database, user, userProfile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(userProfile, 0, "Gl");
                                    player.PlaySong(listPlayListGlobal[rnd].DicCanciones[nPl][rnd], userProfile.FollowedPlayList[rnd].DicCanciones[nPl], database, user, userProfile);
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
                                        if (rndSel != "r" && rndSel != "v")
                                            Console.Write("ERROR [!] ~Select a valid command please (r/v): ");
                                    } while (rndSel != "r" && rndSel != "v");

                                    if (rndSel == "r")
                                    {
                                        int rnd = player.RandomMult(userProfile, 1, "Gl");
                                        player.PlayVideo(listPlayListGlobal[rnd].DicVideos[nPl][rnd], listPlayListGlobal[rnd].DicVideos[nPl], database, user, userProfile);
                                    }
                                    else
                                    {
                                        for (int j = 0; j < listPlayListGlobal[numPl].DicVideos[nPl].Count(); j++)
                                        {
                                            Console.WriteLine("{0}) {1}", j + 1, listPlayListGlobal[numPl].DicVideos[nPl][j].Name);
                                        }
                                        Console.Write("Choose a video: ");
                                        int index = int.Parse(Console.ReadLine()) - 1;
                                        player.PlayVideo(listPlayListGlobal[numPl].DicVideos[nPl][index], listPlayListGlobal[numPl].DicVideos[nPl], database, user, userProfile);
                                    }
                                }
                                else
                                {
                                    int rnd = player.RandomMult(userProfile, 1, "Gl");
                                    player.PlayVideo(listPlayListGlobal[rnd].DicVideos[nPl][rnd], listPlayListGlobal[rnd].DicVideos[nPl], database, user, userProfile);
                                }
                            }
                        }
                        break;

                    case "VI":

                        //El usuario Cierra sesión
                        Console.WriteLine("Logged Out");
                        ret = 0;

                        //Se guarda la información de los usuarios en el archivo
                        database.Save_Users(listUserGlobal);

                        //Se coloca x = false para salir del loop
                        x = false;
                        Console.Clear();

                        break;

                    case "VII":

                        //
                        ret = 1;

                        //Se guarda la informacion de los usuarios en el archivo
                        database.Save_Users(listUserGlobal);

                        //Se coloca x = false para salir del loop
                        x = false;
                        break;

                    case "VIII":
                        //Este caso representa el menú del admin
                        Console.Clear();

                        //Se confirma que el tipo de usuario es admin
                        if (usr.Accountype == "admin")
                        {

                            //Tenemos un menú con comandos que puede utilizar el admin.
                            Console.Clear();
                            Console.WriteLine("------Admin Menu-------");
                            Console.WriteLine("I)Erase Users.\nII)Ban Erase.");

                            //Condicion para corregir error humano.
                            string admin;
                            do
                            {
                                admin = Console.ReadLine();
                                if (admin != "I" && admin != "II")
                                    Console.Write("Please choose one of these options (I/II): ");
                            } while (admin != "I" && admin != "II");


                            //Switch, en donde se ve cada caso.
                            switch (admin)
                            {
                                case "I":

                                    //Le pide al admin que escriba el nombre de usuario que desea borrar.
                                    Console.WriteLine("Type the username, the user you want to delete...");
                                    string username = Console.ReadLine();

                                    //Busca el nombre de usuario en la lista global de usuarios
                                    foreach (User usernombre in listUserGlobal)
                                    {
                                        //si es que encentra una coincidencia, entra a esta condición
                                        if (username == usernombre.Username)
                                        {
                                            //Se borra al usuario de la lista.
                                            listUserGlobal.Remove(usernombre);

                                            Console.WriteLine("Delete succesfull");
                                            break;
                                        }
                                    }

                                    break;

                                case "II":

                                    //Le pide al admin que escriba el nombre de usuario que quiere banear
                                    int y = 0;
                                    Console.WriteLine("Type the username, the user you want to ban...");
                                    string user = Console.ReadLine();

                                    //Busca al usuario en la lista global de usuarios
                                    foreach (User usernombre in listUserGlobal)
                                    {
                                        //Si es que encuentra una coincidencia, entonces entra a la condición
                                        if (user == usernombre.Username && usernombre.Accountype == "premium")
                                        {
                                            //Le cambia el tipo de cuenta de premium a standard.
                                            listUserGlobal[y].Accountype.Replace("premium", "standard");
                                            Console.WriteLine("Ban succesfull");
                                            break;
                                        }
                                        y++;
                                    }

                                    break;
                            }
                        }

                        break;
                }
            }
            //Devuelve ...
            return ret;
        }


        /*tenemos que decidir si esta clase sera de inputs y outputs, o la que hace de reproductor.*/
        public void DisplayGlobalMult(int typeMult, DataBase database)                   //Metodo utilizado para mostrar la lista de multimedia
        {
            //typeMult = 0 significa que quiere mostrar canciones
            if (typeMult == 0)
            {
                //Carga el archivo de canciones globales y lo mete a una lista.
                List<Song> ListSongsGlobal = database.Load_Songs();

                //Por cada cancion que se encuentra en esta lista, imprime su información
                for (int i = 0; i < ListSongsGlobal.Count(); i++)
                {
                    Console.WriteLine("Cancion {0}", i + 1);
                    Console.WriteLine(ListSongsGlobal[i].DisplayInfoSong() + "\n");
                }
            }

            //typwMult = 1 significa que quiere mostrar videos
            else if (typeMult == 1)
            {
                //Se carga el archivo de videos y se agrega a una lista.
                List<Video> ListVideosGlobal = database.Load_Videos();

                //Por cada video que se encuentra en esta lista, imprime su información.
                for (int i = 0; i < ListVideosGlobal.Count(); i++)
                {
                    Console.WriteLine("Video {0}", i + 1);
                    Console.WriteLine(ListVideosGlobal[i].DisplayInfoVideo() + "\n");
                }
            }

            //typeMult = 2 significa que quiere mostrar playlists
            else if (typeMult == 2)
            {
                //se carga el archivo de playlists y se guarda en una lista.
                List<PlayList> ListPLsGlobal = database.Load_PLs();

                //Por cada playlist que se encuentra en la lista, imprime su información.
                for (int i = 0; i < ListPLsGlobal.Count(); i++)
                {
                    Console.WriteLine("Playlist {0}", i + 1);
                    Console.WriteLine(ListPLsGlobal[i].DisplayInfoPlayList() + "\n");
                }
            }

        }

        public void DisplayPlaylists(List<PlayList> playlist)                            //Este método realiza un display a playlists especificas.
        {
            //Por cada playlist que se encuentra en la lista de playlist, imprime su información.
            for (int i = 0; i < playlist.Count(); i++)
            {
                Console.WriteLine(i + ") " + playlist[i].InfoPlayList());
            }
        }

        public void DisplayHistory(List<Song> searchStorySongs, List<Video> searchStoryVideos)
        {                                                                                //Este método es utilizado para mostrar la multimedia que se ha buscado por el usuario.
            
            //Condición que se cumple cuando el listado de busqueda de una multimedia es menor a 5
            if (searchStorySongs.Count() < 5 && searchStoryVideos.Count() < 5)
            {
                //Realiza un for loop en donde imprime lo que buscó el usuario.
                for (int i = 0; i < searchStorySongs.Count(); i++)
                {
                    Console.WriteLine(searchStorySongs[i]);
                    /*Recordar que cada eleemento de estas listas van a ser la información de cada archivo multimedia.*/
                }

                //Realiza un for loop en donde se imprime lo que buscó el usuario
                for (int i = 0; i < searchStoryVideos.Count(); i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }

            //Condición que se cumple cuando la busqueda de canciones sobrepasa o es igual a 5
            else if (searchStorySongs.Count() >= 5 && searchStoryVideos.Count() < 5)
            {
                //Realiza un for loop en donde imprime lo que buscó el usuario. (Max prints = 5)
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStorySongs[i]);
                }

                //Realiza un for loop en donde se imprime lo que buscó el usuario
                for (int i = 0; i < searchStoryVideos.Count(); i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }

            //Condición que se cumple cuando la busqueda de videos sobrepasa o es igual a 5
            else if (searchStorySongs.Count() < 5 && searchStoryVideos.Count() >= 5)
            {
                //Realiza un for loop en donde imprime lo que buscó el usuario.
                for (int i = 0; i < searchStorySongs.Count(); i++)
                {
                    Console.WriteLine(searchStorySongs[i]);
                }

                //Realiza un for loop en donde se imprime lo que buscó el usuario (Max prints = 5)
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }

            //Condición que se cumple cuando la busqueda de canciones y videos sobrepasan o son igual a 5
            else
            {
                //Realiza un for loop en donde imprime lo que buscó el usuario. (Max prints = 5)
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStorySongs[i]);
                }

                //Realiza un for loop en donde se imprime lo que buscó el usuario (Max prints = 5)
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(searchStoryVideos[i]);
                }
            }
        }

        //--------------------------------------------------------------------------------------------------

        //MÉTODOS SETTINGS 
        //--------------------------------------------------------------------------------------------------
        public void AccountSettings(User user)                                           //Este método muestra la información de la cuenta de user
        {
            //Imprime el username, password, Email, tipo de cuenta, cantidad de followers y cantidad de following
            Console.WriteLine("Username: " + user.AccountSettings()[0] + "\n");
            Console.WriteLine("Password: " + user.AccountSettings()[1] + "\n");
            Console.WriteLine("Email: " + user.AccountSettings()[2] + "\n");
            Console.WriteLine("Account type: " + user.AccountSettings()[3] + "\n");
            Console.WriteLine("Followers: " + user.AccountSettings()[4] + "\n");
            Console.WriteLine("Following: " + user.AccountSettings()[5] + "\n");

        }

        public void ProfileSettings(Profile profile)                                     //Este método muestra la información del perfil.
        {
            //Imprime el nombre, tipo de perfil, foto, genero, edad.
            Console.WriteLine("Name: " + profile.ProfileSettings()[0] + "\n");
            Console.WriteLine("Profile Type: " + profile.ProfileSettings()[1] + "\n");
            Console.WriteLine("Profile Pic: " + profile.ProfileSettings()[2] + "\n");
            Console.WriteLine("Gender: " + profile.ProfileSettings()[3] + "\n");
            Console.WriteLine("Age: " + profile.ProfileSettings()[4] + "\n");
        }
        //--------------------------------------------------------------------------------------------------

        //MÉTODOS EXTRAS UTILIZADOS EN LA CLASE MENÚ
        //--------------------------------------------------------------------------------------------------
        public List<string> SearchEngine(string searching, string type, DataBase dataBase)
        {                                                                                //Este método Busca la multimedia que coincide con la que esscribiío con el usuario.
            //Crea una lista de string vacía
            List<string> searchEngine = new List<string>();


            //Esta condición se cumple, cuando el usuario esta buscando cacnciones.
            if (type == "Songs")
            {
                //Se carga el archivo de canciones y se guarda en una lista.
                List<Song> listSongsGlobal = dataBase.Load_Songs();

                //Se recorre la lista global de canciones
                for (int i = 0; i < listSongsGlobal.Count(); i++)
                {
                    //Se recorre la lista de información de la canción.
                    for (int x = 0; x < listSongsGlobal[i].InfoSong().Count(); x++)
                    {
                        //Si es que encuentra una coincidencia con lo que escribío el usuario, se agrega la información de este a la lista searchEngine.
                        if (listSongsGlobal[i].InfoSong()[x] == searching)
                        {
                            searchEngine.Add(listSongsGlobal[i].SearchedInfoSong() + ", Position: " + (i + 1));
                            /*num_s es un int y no me patalea, si tira error es aca. Usar remove leguer*/
                        }
                    }
                }
            }

            //Esta condición se cumple cuando el usuario esta buscando videos.
            else if (type == "Videos")
            {
                //Se carga el archivo de videos y se guarda en una lista.
                List<Video> listVideosGlobal = dataBase.Load_Videos();

                //Se recorre la lista global de videos
                for (int i = 0; i < listVideosGlobal.Count(); i++)
                {
                    //Se recorre la lista de información del video.
                    for (int x = 0; x < listVideosGlobal[i].InfoVideo().Count(); x++)
                    {
                        //Si es que encuentra una coincidencia con lo que escribío el usuario, se agrega la información de este a la lista searchEngine.
                        if (listVideosGlobal[i].InfoVideo()[x] == searching)
                        {
                            searchEngine.Add(listVideosGlobal[i].SearchedInfoVideo() + ", Position: " + (i + 1));
                            /*num_s es un int y no me patalea, si tira error es aca. Usar remove leguer*/
                        }
                    }
                }
            }

            //esta condición se cumple cuando el usuario esta buscando otros usuarios.
            else if (type == "Users")
            {
                //Se carga el archivo de usuarios y se guarda en una lista.
                List<User> diceUserGlobal = dataBase.Load_Users();

                //Se recorre la lista global de usuarios
                for (int i = 0; i < diceUserGlobal.Count(); i++)
                {
                    //Se recorre la lista de información del usuario.
                    for (int x = 0; x < diceUserGlobal[i].infoUser().Count(); x++)
                    {
                        //Si es que encuentra una coincidencia con lo que escribío el usuario, se agrega la información de este a la lista searchEngine.
                        if (diceUserGlobal[i].infoUser()[x] == searching)
                        {
                            //Condiciones que permiten observar o no la informacíon, dependiendo si es que esta se encuentra privada o no.
                            if (diceUserGlobal[i].Privacy != true)
                                searchEngine.Add(diceUserGlobal[i].SearchedInfoUser() + ", Position: " + (i + 1));

                            else if (diceUserGlobal[i].Privacy == true)
                                searchEngine.Add(diceUserGlobal[i].SearchedInfoUser() + ", Position: " + "???");
                        }
                    }
                }
            }

            //Si es que la listea creada al cmienzo, no contiene ningun elemento, entonces no se logró encontrar nada que coincidíera con lo que buscaba el usuario.
            if (searchEngine.Count() == 0)
                Console.WriteLine("No match found...");

            //Devuelve la lista que se encontraba al comienzo.
            return searchEngine;
        }

        

        public List<string> AskInfoMult(int type)                                        //Método utilizado para consultar la información de la canción que quieres agregar.
        {
            //Se crea una lista de string en donde se guarda la información.
            List<string> infoMult = new List<string>();

            //Si type = 0, entonces se trata de la informacción de una canción.
            if (type == 0)
            {
                //Se consulta toda la información necesaria para crear una canción
                Console.Write("Song name: ");                                                                               string n = Console.ReadLine();
                Console.Write("Song artist or artists(if this is the case separate them by '-', Bad Bunny-Ricky Martin: "); string art = Console.ReadLine();
                Console.Write("Album: ");                                                                                   string alb = Console.ReadLine();
                Console.Write("Discography: ");                                                                             string disc = Console.ReadLine();
                Console.Write("Song Gender: ");                                                                             string gen = Console.ReadLine();
                Console.Write("Publish date (dd/mm/aa): ");                                                                 string date = Console.ReadLine();
                Console.Write("Studio: ");                                                                                  string std = Console.ReadLine();
                Console.Write("Song Duration (format: min.seg)(double): ");                                                 string dur = Console.ReadLine();
                Console.Write("Song Format(.mp3 || .wav): ");
                
                //Condición para corregir posible error creado por el usuario.
                string format;
                do
                {
                    format = Console.ReadLine();
                    if (format != ".mp3" && format != ".wav")
                        Console.Write("Please choose one of these options (.mp3/.wav): ");

                } while (format != ".mp3" && format != ".wav");

                Console.Write("Song lyrics(write: We will, we will rock you!!): ");                                         string lyr = Console.ReadLine();

                //Se crea la lista con toda la información.
                infoMult = new List<string>() { n, art, alb, disc, gen, date, std, dur, lyr, format };
            }
            else if (type == 1)
            {
                Console.Write("Video Name: ");                                                                                   string n = Console.ReadLine();
                Console.Write("Video actor or actors(if this is the case separate them by '-', Bad Bunny-Ricky Martin: ");       string act = Console.ReadLine();
                Console.Write("Video director or directors(if this is the case separate them by '-', Bad Bunny-Ricky Martin: "); string dir = Console.ReadLine();
                Console.Write("Publish date (dd/mm/aa): ");                                                                      string date = Console.ReadLine();
                Console.Write("Video Dimension (16:9): ");                                                                       string dim = Console.ReadLine();
                Console.Write("Video Quality");                                                                                  string cal = Console.ReadLine();
                Console.Write("Video category(number) (0 = all espectator, 16 = above 16 years, etc.): ");                       string cat = Console.ReadLine();
                Console.Write("Video Description: ");                                                                            string des = Console.ReadLine();
                Console.Write("Video Duration(format: min.seg)(double): ");                                                      string dur = Console.ReadLine();

                //Condiciones para corregir errores creados por el usuario
                Console.Write("Song Format(.mp4 || .mov): ");
                string format;
                do
                {
                    format = Console.ReadLine();
                    if (format != ".mp4" && format != ".mov")
                        Console.Write("Please choose one of these options (.mp4/.mov): ");

                } while (format != ".mp4" && format != ".mov");


                Console.Write("Video Image(y/n): ");
                string im;
                do
                {
                    im = Console.ReadLine();
                    if (im != "y" && im != "n")
                        Console.Write("Please choose one of these options (y/n): ");

                } while (im != "y" && im != "n");


                Console.Write("Video subtitles(write): "); string sub = Console.ReadLine();

                //Se crea la lista con toda la infromación necesaria para crear un video.
                infoMult = new List<string>() { n, act, dir, date, dim, cal, cat, des, im, dur, sub, format };
            }

            //Si type = 2 entonces se desea agregar una playlist.
            else if (type == 2)
            {

                //Se solicita que escriba el nombre de la playlist.
                Console.Write("Playlist Name: ");    string n = Console.ReadLine();

                //Condición utilizada para corregir posibles errores creados por el usuario
                Console.Write("Do you want your playlist with songs or videos? (c/v): ");
                string choice;
                do
                {
                    choice = Console.ReadLine();
                    if (choice != "c" && choice != "v")
                        Console.Write("Please choose one of these options (c/v): ");

                } while (choice != "c" && choice != "v");

                //Condiciones para poder corregir posibles errores creados por el usuario.
                string format = null;
                if (choice == "c")
                {
                    Console.Write("Song Format(.mp3 || .wav): ");
                    do
                    {
                        format = Console.ReadLine();
                        if (format != ".mp3" && format != ".wav")
                            Console.Write("Please choose one of these options (.mp3/.wav): ");

                    } while (format != ".mp3" && format != ".wav");
                }
                else if (choice == "v")
                {
                    Console.Write("Song Format(.mp4 || .mov): ");
                    do
                    {
                        format = Console.ReadLine();
                        if (format != ".mp4" && format != ".mov")
                            Console.Write("Please choose one of these options (.mp4/.mov): ");

                    } while (format != ".mp4" && format != ".mov");
                }

                //Se agrega la información para crear la playlist a la lista.
                infoMult = new List<string>() { n, format };
            }

            //Se devuelve la lista.
            return infoMult;

        }
        //--------------------------------------------------------------------------------------------------
    }
}