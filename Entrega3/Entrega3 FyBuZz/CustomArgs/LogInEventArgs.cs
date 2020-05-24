using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class LogInEventArgs: EventArgs
    {
        public string UsernameText { get; set; }
        public string PasswrodText { get; set; }
    }
}
