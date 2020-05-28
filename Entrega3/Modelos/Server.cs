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
        }
        */

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


        public void Register(User userlist, List<User> userDataBase)
        {

            // Pedimos todos los datos necesarios
            Console.Write("Welcome! Type your information in FyBuZz\nUsername: ");
            string usr = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string psswd = Console.ReadLine();
            string premium;
            do
            {
                Console.Write("Would you like to pay for the premium subscription?(premium/standard/admin):");
                premium = Console.ReadLine();
            } while (premium != "premium" && premium != "standard" && premium != "admin");
            string pars;
            bool priv;
            do
            {
                Console.Write("Would you like to have a private user?(true/false):");
                pars = Console.ReadLine();
            } while (pars != "true" && pars != "false");
            priv = bool.Parse(pars);
            string gender;
            do
            {
                Console.Write("Select your gender(M/F): ");
                gender = Console.ReadLine();
            } while (gender != "M" && gender != "F");
            int age;
            do
            {
                Console.Write("Select your age: ");
                age = int.Parse(Console.ReadLine());
            } while (age / age != 1);
            string profileType;
            do
            {
                Console.Write("Select your Profile Type(creator/viewer): ");
                profileType = Console.ReadLine();
            } while (profileType != "creator" && profileType != "viewer");

            if (premium == "premium") userlist.AdsOn = false;
            else if (premium == "standard") userlist.AdsOn = true;
            else if (premium == "admin") userlist.AdsOn = false; //añadi publicidad a admin
            else Console.WriteLine("Error [!] Invalid Subscription.");
            userlist.Followers = 0;
            userlist.Following = 0;
            userlist.Perfiles.Add(new Profile(usr, ".JPG", profileType, email, gender, age));


            userlist.Username = usr; userlist.Email = email; userlist.Password = psswd; userlist.Accountype = premium; userlist.Privacy = priv;
            string result = Data.AddUser(userlist, userDataBase);
            if (result == null)
            {
                // Disparamos el evento
                //OnRegistered(usr, psswd,/* verificationlink: verificationLink,*/ email: email);
                Console.WriteLine("Register Succesfull");

            }
            else
            {
                // Mostramos el error
                Console.WriteLine("[!] ERROR: " + result + "\n");
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
