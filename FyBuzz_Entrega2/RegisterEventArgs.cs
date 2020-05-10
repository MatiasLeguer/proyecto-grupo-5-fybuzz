using System;
namespace FyBuzz_Entrega2
{
    public class RegisterEventArgs:EventArgs
    {
        //Que más atributos necesito? Ver clase user de jaco para eso
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
