using System;

namespace FyBuzz_E2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            DataBase database = new DataBase();
            Server server = new Server(database);
            User LogInUser = menu.DisplayLogin();
            if(LogInUser != null)
            {
                menu.DisplayProfiles(LogInUser);
                menu.DisplayStart(LogInUser.Perfil,LogInUser);
            }
            
        }
    }
}
