﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FyBuzz_E2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            User LogInUser = menu.DisplayLogin();
            
            if (LogInUser != null)
            {
                Profile profileMain = menu.DisplayProfiles(LogInUser);
                menu.DisplayStart(profileMain, LogInUser);
            }

        }
    }
}
