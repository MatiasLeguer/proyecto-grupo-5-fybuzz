using Entrega3_FyBuZz.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
       {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FyBuZz fyBuZz = new FyBuZz();
            UserControler userController = new UserControler(fyBuZz);
            SongController songController = new SongController(fyBuZz);
            PlaylistController playlistController = new PlaylistController(fyBuZz);
            Application.Run(fyBuZz);
        }
    }
}
