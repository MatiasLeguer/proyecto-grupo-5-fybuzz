using System;
namespace FyBuzz_Entrega2
{
    public class ChangePasswordEventArgs:EventArgs
    {
        //Necesito algun requisito más para poder cambiar la contraseña?

        public string Username { get; set; }
        public string Email { get; set; }
    }
}
