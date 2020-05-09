using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pino_Entrega2
{
    class Menu
    {
        public bool DisplayLogin()
        {
            bool x = false;
            while (x == false) {
                Console.WriteLine("------------Welcome to FyBuZz--------------");
                Console.WriteLine("I) Log-In with a existing account.");
                Console.WriteLine("II) Register.");
                string dec = Console.ReadLine();
                if (dec == "I")
                {
                    //poner el metodo de server o algo.
                    if (loginsuccesfull)
                    {
                        Console.WriteLine("Login Succesfull.");
                        return true;
                    }
                }
                else
                {
                    //poner le metodo de register
                    //verificar mail.
                    if (todo ok)
                    {
                        Console.WriteLine("Account created, now log in.")
                        return false;
                    }
                }
            }
        }
        public void DisplayStart() // solo funciona si DisplayLogIn() retorna true
        {
            Console.WriteLine("------------Welcome to FyBuZz--------------");
            // mostrará todas las playlist del usuario, si es primera vez que ingresa estara la playlist general y la favorita(esta sin nada)
            DisplayPlaylist(listPlaylistGlobal); // es la lista global de playlist que viene de database, pero hay que conectarla
            if(PlaylistFav().Count() != 0)
            {
                Console.WriteLine(PlaylistFav().InfoPlaylist()); 
            }
            if(Playlistseguidos.Count() != 0)
            {
                Console.WriteLine(DisplayPlaylist(PlaylistSeguidos));
            }
            Console.WriteLine("I) Search Songs or Videos.");
            Console.WriteLine("II) Account Settings.");
            Console.WriteLine("III) Play a Playlist.");
            string dec = Console.ReadLine();
            if(dec == "I")
            {
                //Método de buscar
            }
            else if(dec == "II")
            {
                AccountSettings(); // incorporar el usuario.
            }
            else
            {
                Console.WriteLine("What playlist do you want to play?(GlobalPlayLists, FollowedPlaylists or FavoritePlayList)");
                string play = Console.ReadLine();
                if(play == "FavoritePlayList")
                {
                   //Método de reproducción. 
                }
                else if(play == "GlobalPlayList")
                {
                    Console.WriteLine("Please select the number...");
                    string num = Console.ReadLine();
                    //Método de reproducción segun el número que te dan.
                }
                else if(play == "FollowedPlaylist")
                {
                    Console.WriteLine("Please select the number...");
                    string num = Console.ReadLine();
                    
                }
            }
        }

        //tenemos que decidir si esta clase sera de inputs y outputs, o la que hace de reproductor.
        public void DisplayPlaylist(List<Playist> playlist)
        {
            for(int i = 0; i < playlist.Count(); i++)
            {
                Console.WriteLine(i + ") " + playlist[i].InfoPlaylist());
            }
        }
        public void AccountSettings(User user)
        {
            for(int i = 0; i < user.AccountSettings().Count(); i++)
            {
                Console.WriteLine("Username: ");
                Console.WriteLine(user.AccountSettings()[?]);
                Console.WriteLine("Password: ");
                Console.WriteLine(user.AccountSettings()[?]);
                Console.WriteLine("Email: ");
                Console.WriteLine(user.AccountSettings()[?]);
                Console.WriteLine("Account type: ");
                Console.WriteLine(user.AccountSettings()[?]);
            }
        }

        public void Reproduction()
        {
            Player player = new Player();

            //Cuando entre a un mutlimedia utilizar este evento.
            player.Play();
            player.Stop();
            player.Skip();
            player.Previous();
            player.Random();
            //Obtener algun archivo multimedia.

        }

    }
}
