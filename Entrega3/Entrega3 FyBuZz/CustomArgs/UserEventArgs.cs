using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class UserEventArgs : EventArgs
    {
        public User UserLogIn { get; set; }
        public User UserSearched { get; set; }
        public Profile ProfileUserLogIn {get;set;}

    }
}
