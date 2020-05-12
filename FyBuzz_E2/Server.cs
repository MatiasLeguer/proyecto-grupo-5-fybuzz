using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
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
        public delegate void RegisterEventHandler(object source, RegisterEventArgs args);
        public event RegisterEventHandler Registered;

        protected virtual void OnRegistered(string username, string password, string email)
        {
            if (Registered != null)
            {
                Registered(this, new RegisterEventArgs() { Username = username, Password = password, Email = email });
            }
        }

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


        public void Register(User userlist)
        {

            // Pedimos todos los datos necesarios
            Console.Write("Welcome! Type your information in FyBuZz\nUsername: ");
            string usr = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string psswd = Console.ReadLine();
            Console.Write("Would you like to pay for the premium subscription?(premium/standard):");
            string premium = Console.ReadLine();
            Console.Write("Would you like to have a private user?(true/false):");
            bool priv = bool.Parse(Console.ReadLine());
            Console.Write("Select your gender(M/F): ");
            string gender = Console.ReadLine();
            Console.Write("Select your age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Select your Profile Type(creator/viewer): ");
            string profileType = Console.ReadLine();

            if (premium == "premium") userlist.AdsOn = false;
            else if (premium == "standard") userlist.AdsOn = true;
            else Console.WriteLine("Error [!] Invalid Subscription.");
            userlist.Followers = 0;
            userlist.Following = 0;
            userlist.Perfiles.Add(new Profile(usr,".JPG", profileType, email, gender, age));
            

            userlist.Username = usr; userlist.Email = email; userlist.Password = psswd; userlist.Accountype = premium;userlist.Privacy = priv;
            string result = Data.AddUser(userlist);
            if (result == null)
            {
                // Disparamos el evento
                OnRegistered(usr, psswd,/* verificationlink: verificationLink,*/ email: email);
                Console.WriteLine("Register Succesfull");
                
            }
            else
            {
                // Mostramos el error
                Console.WriteLine("[!] ERROR: " + result + "\n");
            }
        }
        /*
        public void ChangePassword()
        {
            Console.Write("Ingrese su nombre de usuario: ");
            string user = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            string pass = Console.ReadLine();

            User result = Data.LogIn(user,pass);
            if (result != null)
            {
                Console.Write("Ingrese la nueva contraseña: ");
                string newPass = Console.ReadLine();

                Data.ChangePassword(user, newPass);
                //List<string> data = Data.GetData(user);
                //OnPasswordChanged(data[0], data[1], data[2]);
            }
            else
            {
                Console.WriteLine("[!]ERROR: {0}", result);
            }

        }*/
    }
}
