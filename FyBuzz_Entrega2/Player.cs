using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FyBuzz_Entrega2
{
    public class Player //Ncesito obtener la duración del archivo multimedia....
    {
        //Hay que usar eventos que vengan desde menú y triguereen los métodos de aca con las canciones de Database.
        // hacer un get a la duracion y al nombre
        public int Play(int cont, int multimediafile, bool playlist, int x)
        {
            if (multimediafile == 0)
            {

            }
            else
            {

            }
            //Si es menor de tal edad no puede ver esta pelicula;
            int seconds = 60 * duration; //duration viene de multimedia y es la cantidad de minutos como decimal, hay que establecer una relacion
            Console.WriteLine("Playing: " + name); //name tambien viene de multimedia.
            Console.WriteLine("To stop press 0.\n");
            Console.WriteLine("To skip press 2.\n");
            Console.WriteLine("To preview press 3.\n");
            for (int i = 0; i < seconds; i++)
            {
                cont++;
                int verif = int.Parse(Console.ReadLine());
                if (verif == 0) break;
                else if (verif == 2 && playlist == true) // Solo se podra saltar si se encuentra en una playlist, si no no.
                {
                    Skip(List, x);
                    break;
                }
                else if (verif == 3 && playlist == true)
                {
                    Previous(cont, List, x);
                    break;
                }
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
        public void Skip(List<Playlist> PLlist, int i)
        {
            int cont = 0;
            //Aumentar 1 espacio en la playlist o lista de canciones o videos
            if (i < PLlist.Count()) Play(cont, multimedia, true); // play la canción sgte
            else Console.WriteLine("Last multimedia archive in the playlist. Error");

        }
        public int Previous(int cont, List<Playlist> PLlist, int i)
        {
            if (cont == 0)
            {
                //Disminuir un espacio en al playlist o lista
                if (i < PLlist.Count()) Play(cont, multimedia, true); // play la canción anterior
                else Console.WriteLine("First multimedia archive in the playlist. Error");
            }
            else cont = 0;
            return cont;
        }
        public void Random()
        {
            int x = 0;
            int cont = 0;
            while (cont != -1)
            {
                cont = Play(cont, multimedia, true, x); //Devuelve el tiempo en el que se para la canción
                if (cont != -1) Stop(cont); // devuelve el contador cuando se detiene para empezar de nuevo.
            }
        }
    }
}

