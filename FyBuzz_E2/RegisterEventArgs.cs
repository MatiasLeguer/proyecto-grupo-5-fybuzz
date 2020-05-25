using System;
namespace FyBuzz_E2
{
    public class RegisterEventArgs:EventArgs
    {
        /* Que más atributos necesito? Ver clase user de jaco para eso */

        //GETTERS Y SETTERS
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
