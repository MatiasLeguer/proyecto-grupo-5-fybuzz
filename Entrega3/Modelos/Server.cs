using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Server
    {
        public DataBase Data;

        //Obtenemos el constructor para poder recibir la base de datos.
        public Server(DataBase data)
        {
            this.Data = data;
        }

        //Creamos evento para Registrarse
        /*public delegate void RegisterEventHandler(object source, RegisterEventArgs args);
        public event RegisterEventHandler Registered;

        protected virtual void OnRegistered(string username, string password, string email)
        {
            if (Registered != null)
            {
                Registered(this, new RegisterEventArgs() { Username = username, Password = password, Email = email });
            }
        }*/

        //Creamos evento para cambiar la contraseña
        public delegate void ChangePasswordEventHandler(object source, ChangePasswordEventArgs args);
        public event ChangePasswordEventHandler PasswordChanged;

        protected virtual void OnPasswordChanged(string username, string email)
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(this, new ChangePasswordEventArgs() { Username = username, Email = email });
            }
        }


        public bool Register(User userlist, List<User> userDataBase, string usr, string email, string psswd, string premium, bool priv, string gender, DateTime age, string profileType)
        {
            if (premium == "premium") userlist.AdsOn = false;
            else if (premium == "standard") userlist.AdsOn = true;
            else if (premium == "admin") userlist.AdsOn = false; //añadi publicidad a admin
            else Console.WriteLine("Error [!] Invalid Subscription.");
            userlist.Followers = 0;
            userlist.Following = 0;
            int Age = 2020 - age.Year;
            userlist.Perfiles.Add(new Profile(usr, ".JPG", profileType, email, gender, Age));


            userlist.Username = usr; userlist.Email = email; userlist.Password = psswd; userlist.Accountype = premium; userlist.Privacy = priv;
            string result = Data.AddUser(userlist, userDataBase);
            if (result == null)
            {
                // Disparamos el evento
                //OnRegistered(usr, psswd,/* verificationlink: verificationLink,*/ email: email);
                return true;

            }
            else
            {
                // Mostramos el error
                return false;
            }
        }

        public void ChangePassword(List<User> userdatabase)
        {
            Console.Write("Ingrese su nombre de usuario: ");
            string user = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            string pass = Console.ReadLine();

            User result = Data.LogIn(user, pass, userdatabase);
            if (result != null)
            {
                Console.Write("Ingrese la nueva contraseña: ");
                string newPass = Console.ReadLine();

                Data.ChangePassword(user, newPass);
                List<string> data = Data.GetData(user, userdatabase);
                OnPasswordChanged(data[0], data[1]);
            }
            else
            {
                Console.WriteLine("[!]ERROR: {0}", result);
            }

        }
    }
}
