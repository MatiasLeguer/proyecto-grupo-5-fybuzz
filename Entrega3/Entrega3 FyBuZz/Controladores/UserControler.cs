using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz.Controladores
{
    public class UserControler
    {
        List<User> userDataBase = new List<User>();
        DataBase dataBase = new DataBase();
        FyBuZz fyBuZz;


        public UserControler(Form fyBuZz)
        {
            Initialize();
            this.fyBuZz = fyBuZz as FyBuZz;
            this.fyBuZz.LogInLogInButton_Clicked += OnLoginButtonClicked;
        }

        public void Initialize()
        {
            userDataBase = dataBase.Load_Users();
        }

        private bool OnLoginButtonClicked(object sender, LogInEventArgs e)
        {
            User user = null;
            user = userDataBase.Where(u => u.Username.Contains(e.UsernameText)).FirstOrDefault();
            bool x = true;
            if(user is null)
            {
                return false;
            }
            else
            {
                if(dataBase.LogIn(user.Username, user.Password, userDataBase) != null)
                {
                    x = true;
                }
                return x;
            }
        }
    }
}
