using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    [Serializable]
    public class User
    {
        private string username;
        private string password;
        private string accountType; // Premium,Standard //Yo le pondria un 0 y un 1, asi es mas facil
        private string email;
        private int followers;
        private int following;
        private bool verified;
        private bool adsOn;
        private bool privacy;
        private Profile perfil;
        //private Dictionary<int, Profile> perfiles = new Dictionary<int, Profile>(); Agregar perfiles a el archivo de usuario para que no s epierdan cuando se cierrre el program

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Accountype { get => accountType; set => accountType = value; }
        public int Followers { get => followers; set => followers = value; }
        public int Following { get => following; set => following = value; }
        public bool Verified { get => verified; set => verified = value; }
        public bool AdsOn { get => adsOn; set => adsOn = value; }
        public bool Privacy { get => privacy; set => privacy = value; }
        public Profile Perfil { get => perfil; set => perfil = value; } 

        // Constructor
        public User()
        {

        }

        /*
        public void CreateProfile(string pname, string ppic, string ptype, string pmail, string pgender, int page,int cont)
        {
            Profile profileX = new Profile(pname, ppic, ptype, pmail, pgender, page);
            perfiles.Add(cont, profileX);
        }*/

        public List<string> AccountSettings()
        {
            // Metodo que entrega la lista de informacion del usuario seleccionado
            List<string> Settings = new List<string>() { username, password, email, accountType };
            return Settings;

            // ver si efectivamente esta informacion proviene del usuario o si se adquiere de database.

        }

        public List<string> infoUser()
        {
            return new List<string> { username,email };
        }
        public string SearchedInfoUser()
        {
            return "Name: " + username + " " + "Email: " + email;
        }

        public bool GetVerification()
        {
            // Decide si tiene o no verificacion a partir de sus seguidores
            if (followers > 100000)
            {
                verified = true;
                return verified;
            }
            else
            {
                verified = false;
                return verified;
            }
        }


        // Diccionario de usuarios {key int, lista de palabras}
        /*
        public void AdminDeleteUser(DataBase data, int key)
        {
            // if y fors para borrar la informacion del usuario
            // acceder al diccionario con la clave key
            if (key == registerNumber)
            {
                data.Load_Users().Remove(key);
            }

        }*/

        public void AdminBanUser()
        {
            // Cambiar el account tyoe a uno menor
            if (accountType == "Premium")
            {
                accountType.Replace("Premium", "Standard");
            }

        }
        // Encontrar mas metodos para admin +Admin...

    }
}
