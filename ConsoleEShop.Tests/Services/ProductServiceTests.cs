using ConsoleEShop.Data;
using ConsoleEShop.Entities;
using ConsoleEShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Tests.Services
{
    [TestClass]
    public  class ProductServiceTests
    {
        private static StoreContext _context;
        private static IUnitOfWork _unitOfWork;
        private static Product _productOrange;

        private static StoreContext GetStoreContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StoreContext(options);

            _unitOfWork = new UnitOfWork(context);

            _productOrange = new Product { Id = 1, Name = "Orange", Price = 100, Category = "Fruits" };

            context.Products.Add(_productOrange);
            context.Products.Add(new Product { Id = 2, Name = "Melon", Price = 150, Category = "Fruits" });

            context.SaveChanges();

            return context;
        }

        [ClassInitialize]
        public static void ProductServiceTestsInitialize(TestContext testContext)
        {
            _context = GetStoreContext();
        }

        [TestMethod]
        public void GetProductById_WithExistId_ReturnCorrectValue()
        {
            //arrange
            var expected = _productOrange;

            //act
            var actual = new ProductService(_unitOfWork).GetProductById(_productOrange.Id);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetProductById_WithNonExistentId_ReturnNull()
        {
            //arrange
            Product expected = null;

            //act
            var actual = new ProductService(_unitOfWork).GetProductById(int.MinValue);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetProductByName_WithExistName_ReturnCorrectValue()
        {
            //arrange
            var expected = _productOrange;

            //act
            var actual = new ProductService(_unitOfWork).GetProductByName(_productOrange.Name);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetProductByName_WithNonExistentName_ReturnNull()
        {
            //arrange
            Product expected = null;

            //act
            var actual = new ProductService(_unitOfWork).GetProductByName("___");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty product`s name was inappropriately allowed.")]
        public void GetProductByName_EmptyName_ShouldThrowArgumentException()
        {
            //act
            new ProductService(_unitOfWork).GetProductByName("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A product`s name of null was inappropriately allowed.")]
        public void GetProductByName_NullName_ShouldThrowArgumentNullException()
        {
            //act
            new ProductService(_unitOfWork).GetProductByName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty product`s name was inappropriately allowed.")]
        public void CreateProduct_EmptyProductname_ShouldThrowArgumentException()
        {
            //act
            new ProductService(_unitOfWork).CreateProduct("", "", "", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A product`s name of null was inappropriately allowed.")]
        public void CreateProduct_NullProductname_ShouldThrowArgumentNullException()
        {
            //act
            new ProductService(_unitOfWork).CreateProduct(null, "", "", 0);
        }

    }
}
