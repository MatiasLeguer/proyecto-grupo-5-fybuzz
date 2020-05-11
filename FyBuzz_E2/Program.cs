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
            //database.createFiles();
            bool LogIn = menu.DisplayLogin();
            if(LogIn == true)
            {
                Profile profile = menu.DisplayProfiles();
                menu.DisplayStart(profile);
            }
            
        }
    }
}
