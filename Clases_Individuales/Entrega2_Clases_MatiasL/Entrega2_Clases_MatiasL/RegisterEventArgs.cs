﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_Matias_Leguer_entrega2
{
    public class RegisterEventArgs : EventArgs
    {
        //Que más atributos necesito? Ver clase user de jaco para eso
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
