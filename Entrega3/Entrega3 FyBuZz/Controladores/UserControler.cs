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
        Server server = new Server(data);
        FyBuZz fyBuZz;


        public UserControler(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.LogInLogInButton_Clicked += OnLoginButtonClicked;
            this.fyBuZz.RegisterRegisterButton_Clicked += OnRegisterRegisterButtonClicked;
        }

        public void Initialize()
        {
            if (File.Exists("AllUsers.bin") != true) dataBase.Save_Users(userDataBase);
            userDataBase = dataBase.Load_Users();
        }

        private bool OnLoginButtonClicked(object sender, LogInEventArgs e)
        {
            User user = new User();
            
            bool x = true;
            
            if(dataBase.LogIn(e.UsernameText, e.PasswrodText, userDataBase) != null)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            
            return x;
        }
        private bool OnRegisterRegisterButtonClicked(object sender, RegisterEventArgs e)
        {
            User user = new User();
            bool x;
            x = server.Register(user, userDataBase,e.UsernameText, e.EmailText, e.PasswrodText, e.SubsText, e.PrivacyText, e.GenderText, e.BirthdayText, e.ProfileTypeText);
            dataBase.Save_Users(userDataBase);
            return x;
        }
    }
}
