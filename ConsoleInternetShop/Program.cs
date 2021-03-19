using ConsoleEShop;
using ConsoleEShop.Data;
using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleInternetShop
{
    static class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StoreContext(options);

            var unitOfWork = new UnitOfWork(context);

            var productService = new ProductService(unitOfWork);
            var authService = new UserService(unitOfWork);
            var orderService = new OrderService(unitOfWork);

            byte[] passwordHash, passwordSalt;

            authService.CreatePasswordHash("1", out passwordHash, out passwordSalt);

            var userToCreate = new User { Id = 1, PasswordHash = passwordHash, PasswordSalt = passwordSalt, UserName = "admin", UserRole = UserRoles.Administrator };

            context.Users.Add(userToCreate);

            context.Products.Add(new Product { Id = 1, Name = "Orange", Price = 100, Category = "Fruits" });
            context.Products.Add(new Product { Id = 2, Name = "Melon", Price = 150, Category = "Fruits" });

            context.SaveChanges();

            new ConsoleEShopBuilder(productService, authService, orderService).Run();
        }

    }
}

