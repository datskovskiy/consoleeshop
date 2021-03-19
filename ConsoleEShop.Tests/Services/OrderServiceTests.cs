using System;
using System.Collections.Generic;
using System.Text;
using ConsoleEShop.Data;
using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using ConsoleEShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleEShop.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private static StoreContext _context;
        private static IUnitOfWork _unitOfWork;
        private static Order _order;

        private static StoreContext GetStoreContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StoreContext(options);

            _unitOfWork = new UnitOfWork(context);

            var correctAdminPassword = "1";
            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(correctAdminPassword));
            }

            var userAdmin = new User
            {
                Id = 1,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = "admin",
                UserRole = UserRoles.Administrator
            };

            var userToCreate = new User
            {
                Id = 2,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = "user",
                UserRole = UserRoles.Administrator
            };

            context.Users.Add(userAdmin);
            context.Users.Add(userToCreate);

            var productOrange = new Product { Id = 1, Name = "Orange", Price = 100, Category = "Fruits" };
            context.Products.Add(productOrange);
            context.Products.Add(new Product { Id = 2, Name = "Melon", Price = 150, Category = "Fruits" });

            var orderItems = new List<OrderItem>();
            orderItems.Add(new OrderItem(productOrange.Id, productOrange.Price, 1));

            _order = new Order 
            { 
                Id = 1,
                ClientId = userToCreate.Id,
                OrderItems = orderItems
            };

            context.Orders.Add(_order);
            
            context.SaveChanges();

            return context;
        }

        [ClassInitialize]
        public static void OrderServiceTestsInitialize(TestContext testContext)
        {
            _context = GetStoreContext();
        }

        [TestMethod]
        public void GetOrderById_WithExistId_ReturnCorrectValue()
        {
            //arrange
            var expected = _order;

            //act
            var actual = new OrderService(_unitOfWork).GetOrderById(_order.Id);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOrderById_WithNonExistentId_ReturnNull()
        {
            //arrange
            Product expected = null;

            //act
            var actual = new OrderService(_unitOfWork).GetOrderById(int.MinValue);

            //assert
            Assert.AreEqual(expected, actual);
        }

    }
}
