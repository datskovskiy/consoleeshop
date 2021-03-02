using ConsoleEShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop
{
    public class MenuManager
    {
        private readonly Menu _root;

        public MenuManager(Menu root)
        {
            _root = root;
        }

        public void Run()
        {
            _root.Run();
        }
    }
}
