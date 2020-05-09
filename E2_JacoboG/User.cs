using System;
using System.Collections.Generic;

namespace E2_JacoboG
{
    public class User 
    {
        private int registerNumber;
        private string username;
        private string password;
        private string accountType; // Premium,Standard
        private string email;
        private int followers;
        private int following;
        private bool verified;
        private bool adsOn;
        private bool privacy;

        // Constructor
        public User()
        {
            
        }

        public void CreateProfile(string pname, string ppic, string ptype, string pgender, int page)
        {
            Profile profileX = new Profile(pname, ppic, ptype, pgender, page);
        }

        public List<string> AccountSettings()
        {
            // Metodo que entrega la lista de informacion del usuario seleccionado
            List<string> Settings = new List<string>() { username, password, email, accountType };
            return Settings;

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
        public DataBase data { get; }
        // Diccionario de usuarios {key int, lista de palabras}
        public void AdminDeleteUser(DataBase data, int key)
        {
            // if y fors para borrar la informacion del usuario
            // acceder al diccionario con la clave key
            
        }

        public void AdminBanUser()
        {
            // Cambiar el account tyoe a uno menor
            if (accountType=="Premium")
            {
                accountType.Replace("Premium", "Standard");
            }

        }
        // Encontrar mas metodos para admin +Admin...





    }
}
