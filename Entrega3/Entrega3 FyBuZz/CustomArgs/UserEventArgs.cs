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
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string EmailText { get; set; }
        public string AccountypeText { get; set; }
        public string FollowersText { get; set; }
        public string FollowingText { get; set; }
        public string VerifiedText { get; set; }
        public string AddsOnText { get; set; }
        public string PrivacyText { get; set; }
        public string ProfilenameText { get; set; }
        public int WantToChangeText { get; set; }
        public string ChangedText { get; set; }
        public List<string> FollowingListText { get; set; }
        public List<string> FollowersListText { get; set; }
        public User UserLogIn { get; set; }
        public User UserSearched { get; set; }
        public Profile ProfileUserLogIn {get;set;}
        public string SongFileText { get; set; }
        public string VideoFileText { get; set; }
        public List<string> SharedMult { get; set; }
        

    }
}
