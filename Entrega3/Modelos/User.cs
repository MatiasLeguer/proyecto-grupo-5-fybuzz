using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    [Serializable]
    public class User
    {
        //ATRIBUTOS:

        protected int registerNumber;
        protected string username;
        protected string password;
        protected string accountType;
        protected string email;
        protected int followers;
        protected int following;
        protected List<string> followingList = new List<string>();
        protected List<string> followerList = new List<string>();
        protected bool verified;
        protected bool adsOn;
        protected bool privacy;
        protected List<PlayList> profilePlaylists = new List<PlayList>();
        protected List<Profile> perfiles = new List<Profile>();
        //--------------------------------------------------------------------------------------------------



        //GETTERS Y SETTERS
        //--------------------------------------------------------------------------------------------------
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Accountype { get => accountType; set => accountType = value; }
        public int Followers { get => followers; set => followers = value; }
        public int Following { get => following; set => following = value; }
        public bool Verified { get => verified; set => verified = value; }
        public bool AdsOn { get => adsOn; set => adsOn = value; }
        public bool Privacy { get => privacy; set => privacy = value; }
        public List<Profile> Perfiles { get => perfiles; set => perfiles = value; }
        public List<string> FollowingList { get => followingList; set => followingList = value; }
        public List<string> FollowerList { get => followerList; set => followerList = value; }
        public List<PlayList> ProfilePlaylists { get => profilePlaylists; set => profilePlaylists = value; }
        //--------------------------------------------------------------------------------------------------


        //METODOS:

        //MÉTODO PARA CREAR UN PERFIL
        //--------------------------------------------------------------------------------------------------
        public Profile CreateProfile(string pname, string ppic, string ptype, string pmail, string pgender, int page)
        {
            Profile profileX = new Profile(pname, ppic, ptype, pmail, pgender, page);
            perfiles.Add(profileX);
            return profileX;
        }
        //--------------------------------------------------------------------------------------------------



        //MÉTODOS PARA ENTREGAR INFORMACIÓN DE LA CLASE
        //--------------------------------------------------------------------------------------------------
        public List<string> AccountSettings()                      //Entrega la lista de informacion del usuario seleccionado
        {

            List<string> Settings = new List<string>() { username, password, email, accountType, followers.ToString(), following.ToString() };
            return Settings;
            // ver si efectivamente esta informacion proviene del usuario o si se adquiere de database.


        }

        public List<string> infoUser()                             //Entrega el username y el email del usuario
        {
            return new List<string> { username, email };
        }

        public string SearchedInfoUser()                          //Entrega un string con el username y el email del usuario
        {
            return "Name: " + username + " " + "Email: " + email;
        }

        public bool GetVerification()                            //Decide si tiene o no verificacion a partir de sus seguidores
        {

            if (followers > 3)
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
        //--------------------------------------------------------------------------------------------------

        //MÉTODOS QUE PUEDE UTILIZAR EL ADMIN
        //--------------------------------------------------------------------------------------------------
        public void AdminBanUser()                              // Cambiar el account tyoe a uno menor
        {

            if (accountType == "Premium")
            {
                accountType.Replace("Premium", "Standard");
            }

        }
        // Encontrar mas metodos para admin +Admin...
        //--------------------------------------------------------------------------------------------------

    }
}

//SE ENCONTRABA EN LA LINEA 82

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
