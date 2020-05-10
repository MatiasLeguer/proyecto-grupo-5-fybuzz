using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntrega2
{
    public class ChangePasswordEventArgs : EventArgs
    {
        //Necesito algun requisito más para poder cambiar la contraseña?

        public string Username { get; set; }
        public string Email { get; set; }
    }
}
