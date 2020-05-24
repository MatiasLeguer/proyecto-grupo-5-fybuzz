using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class RegisterEventArgs:EventArgs
    {
        public string UsernameText { get; set; }
        public string EmailText { get; set; }
        public string PasswrodText { get; set; }
        public string SubsText { get; set; }
        public bool PrivacyText { get; set; }
        public string GenderText { get; set; }
        public DateTime BirthdayText { get; set; }
        public string ProfileTypeText { get; set; }
    }
}
