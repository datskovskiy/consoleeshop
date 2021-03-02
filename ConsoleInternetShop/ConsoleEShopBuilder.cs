
using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using ConsoleEShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ConsoleEShop
{
    public class ConsoleEShopBuilder
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsoleEShopBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            var choicesMain = new List<MenuChoice>();

            var root = new Menu(choicesMain, null);

            choicesMain.Add(new MenuChoice("if you want to view a list of products.", PrintProducts, UserRoles.Guest));
            choicesMain.Add(new MenuChoice("if you want to find product by name.", SearchProductByName, UserRoles.Guest));
            choicesMain.Add(new MenuChoice("if you want to sign up.", Register, UserRoles.Guest));
            choicesMain.Add(new MenuChoice("if you want to sign in.", Login, UserRoles.Guest));
            choicesMain.Add(new MenuChoice("if you want to create order.", CreateOrder, UserRoles.User));
            choicesMain.Add(new MenuChoice("if you want to sign out.", SignOut, UserRoles.Guest));

            new MenuManager(root).Run();
        }

        void CreateOrder()
        {
            while ( Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var product = FindProductByName();
                if (product != null)

            }
        }

        void PrintProducts()
        {
            foreach (var product in _unitOfWork.ProductRepository.GetProducts())
            {
                Console.WriteLine($"{product.Id}. {product.Name} ({product.Price}).");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        void SearchProductByName()
        {
            FindProductByName();
        }

        IProduct FindProductByName()
        {
            var productName = String.Empty;
            while (productName == String.Empty)
            {
                Console.WriteLine("Please enter product`s name.");
                productName = Console.ReadLine();
                productName = productName.Trim();
            }

            var productFromRepo = _unitOfWork.ProductRepository.GetProductByName(productName);
            if (productFromRepo == null)
                Console.WriteLine("Product not found!");
            else
                Console.WriteLine($"{productFromRepo.Id}. {productFromRepo.Name} ({productFromRepo.Price}).");

            Console.WriteLine("Press any key to continue.");

            return productFromRepo;
        }

        void SignOut()
        {
            Menu.CurrentUser = null;
        }

        void Login()
        {
            var username = String.Empty;
            while (username == String.Empty)
            {
                Console.WriteLine("Please enter username");
                username = Console.ReadLine();
                username = username.Trim();
            }

            Console.WriteLine("Please enter password");
            var password = GetConsoleSecurePassword().ToString();
            Console.WriteLine();

            var userFromRepo = _unitOfWork.AuthRepository.Login(username, password);
            if (userFromRepo == null)
                Console.WriteLine("User not found! Press any key to continue.");
            else
                Console.WriteLine($"Welcome {userFromRepo.UserName}! Press any key to continue.");

            Console.ReadKey();
            Menu.CurrentUser = userFromRepo;
        }

        void Register()
        {
            var username = String.Empty;
            while (username == String.Empty)
            {
                Console.WriteLine("Please enter username");
                username = Console.ReadLine();
                username = username.Trim();
            }

            Console.WriteLine("Please enter password");
            var password = GetConsoleSecurePassword().ToString();

            var userToCreate = new User
            {
                UserName = username
            };

            _unitOfWork.AuthRepository.Register(userToCreate, password);
        }

        private static SecureString GetConsoleSecurePassword()
        {
            SecureString pwd = new SecureString();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    pwd.RemoveAt(pwd.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd;
        }

    }


}
