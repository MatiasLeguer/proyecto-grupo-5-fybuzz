using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class PlaylistEventArgs:EventArgs
    {
        public string NameText { get; set; }
        public string FormatText { get; set; }
        public string CreatorText { get; set; }
        public string ProfileCreatorText { get; set; }
    }
}
