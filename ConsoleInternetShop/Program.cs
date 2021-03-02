using ConsoleEShop;
using ConsoleEShop.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInternetShop
{
    static class Program
    {
        static void Main(string[] args)
        {
            var context = new StoreContext();
            var unitOfWork = new UnitOfWork(context);

            new ConsoleEShopBuilder(unitOfWork).Run();
        }

    }
}

