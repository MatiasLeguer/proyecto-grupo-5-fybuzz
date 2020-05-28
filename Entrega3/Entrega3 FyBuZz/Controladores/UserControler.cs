using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz.Controladores
{
    public class UserControler
    {
        List<User> userDataBase = new List<User>() { new User() };
        DataBase dataBase = new DataBase();
        static DataBase data = new DataBase();
        Modelos.Menu menu = new Modelos.Menu();
        Server server = new Server(data);
        FyBuZz fyBuZz;


        public UserControler(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.LogInLogInButton_Clicked += OnLoginButtonClicked;
            this.fyBuZz.RegisterRegisterButton_Clicked += OnRegisterRegisterButtonClicked;
            this.fyBuZz.CreateProfileCreateProfileButton_Clicked += OnCreateProfileCreateProfileButton_Clicked;
            this.fyBuZz.ProfilesChooseProfile_Clicked += OnProfilesChooseProfile_Click;
            this.fyBuZz.SearchUserButton_Clicked += OnSearchUserButton_Click;
        }

        public void Initialize()
        {
            if (File.Exists("AllUsers.bin") != true) dataBase.Save_Users(userDataBase);
            userDataBase = dataBase.Load_Users();
        }

        private int UserIndex(ProfileEventArgs e)
        {
            int u = 0;
            foreach(User user in userDataBase)
            {
                if (user.Username == e.UsernameText && user.Password == e.PasswordText)
                {
                    break;
                }
                u++;
            }
            return u;
        }

        private User OnLoginButtonClicked(object sender, LogInEventArgs e)
        {
            return dataBase.LogIn(e.UsernameText, e.PasswrodText, userDataBase);
        }
        private bool OnRegisterRegisterButtonClicked(object sender, RegisterEventArgs e)
        {
            User user = new User();
            bool x;
            x = server.Register(user, userDataBase,e.UsernameText, e.EmailText, e.PasswrodText, e.SubsText, e.PrivacyText, e.GenderText, e.BirthdayText, e.ProfileTypeText);
            dataBase.Save_Users(userDataBase);
            return x;
        }
        private Profile OnCreateProfileCreateProfileButton_Clicked(object sender, ProfileEventArgs e)
        {
            int u = UserIndex(e);
            if (userDataBase[u].Accountype == "premium")
            {   
                int pAge = DateTime.Now.Year - e.BirthdayText.Year;
                string pPic = "...";
                string name = userDataBase[u].Username;
                Profile profile = userDataBase[u].CreateProfile(e.ProfileNameText, pPic, e.ProfileTypeText, e.EmailText, e.GenderText, pAge);
                userDataBase[u].Perfiles.Add(profile);
                dataBase.Save_Users(userDataBase);
                return profile;
            }
            else
            {
                return null;
            }
        }
        private Profile OnProfilesChooseProfile_Click(object sender, ProfileEventArgs e)
        {
            int u = UserIndex(e);
            int pAge = DateTime.Now.Year - e.BirthdayText.Year;
            Profile prof = new Profile(e.ProfileNameText,"..",e.ProfileTypeText,e.EmailText,e.GenderText, pAge);
            foreach(Profile profile in userDataBase[u].Perfiles)
            {
                
                if(profile.ProfileName == prof.ProfileName || profile.Username == prof.ProfileName)
                {
                    return profile;
                }
            }
            return null;
        }
        private List<User> OnSearchUserButton_Click(object sender, RegisterEventArgs e)
        {
            return userDataBase;
        }
    }
}
