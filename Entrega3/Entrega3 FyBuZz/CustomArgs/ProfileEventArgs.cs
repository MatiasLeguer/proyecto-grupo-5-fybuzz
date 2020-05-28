using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class ProfileEventArgs:EventArgs
    {
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string ProfileNameText { get; set; }
        public string EmailText { get; set; }
        public string GenderText { get; set; }
        public DateTime BirthdayText { get; set; }
        public string ProfileTypeText { get; set; }
        public Image PicImage { get; set; }
    }
}
