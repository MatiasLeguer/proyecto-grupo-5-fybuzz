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


        public void Register()
        {
            // Pedimos todos los datos necesarios
            Console.Write("Bienvenido! Ingrese sus datos de registro en FyBuZz\nUsuario: ");
            string usr = Console.ReadLine();
            Console.Write("Correo: ");
            string email = Console.ReadLine();
            Console.Write("Contraseña: ");
            string psswd = Console.ReadLine();

            // Genera el link de verificacion para el usuario
            //string verificationLink = GenerateLink(usr);

            User userlist = new User(); 
            userlist.Username = usr; userlist.Email = email; userlist.Password = psswd;
            string result = Data.AddUser(userlist);
            if (result == null)
            {
                // Disparamos el evento
                OnRegistered(usr, psswd,/* verificationlink: verificationLink,*/ email: email);
                
            }
            else
            {
                // Mostramos el error
                Console.WriteLine("[!] ERROR: " + result + "\n");
            }
        }
        public void ChangePassword()
        {
            Console.Write("Ingrese su nombre de usuario: ");
            string user = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            string pass = Console.ReadLine();

            string result = Data.LogIn(user, pass);
            if (result == null)
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

        }
    }
}
