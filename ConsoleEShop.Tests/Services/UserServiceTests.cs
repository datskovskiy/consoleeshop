using ConsoleEShop.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using ConsoleEShop.Interfaces;

namespace ConsoleEShop.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private static StoreContext _context;
        private static IUnitOfWork _unitOfWork;
        private static User _userAdmin;
        private static string _correctAdminPassword;
        private static string _nonExistentUsername;

        private static StoreContext GetStoreContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new StoreContext(options);

            _unitOfWork = new UnitOfWork(context);

            _correctAdminPassword = "1";
            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_correctAdminPassword));
            }

            _userAdmin = new User 
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

            context.Users.Add(_userAdmin);
            context.Users.Add(userToCreate);

            context.SaveChanges();

            return context;
        }

        [ClassInitialize]
        public static void UserServiceTestsInitialize(TestContext testContext)
        {
            _context = GetStoreContext();

            _nonExistentUsername = "user1";
        }

        [TestMethod]
        public void Login_WithExistUser_ReturnCorrectUser()
        {
            //arrange
            User expected = _userAdmin;

            //act
            var actual = new UserService(_unitOfWork).Login(_userAdmin.UserName, _correctAdminPassword);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Login_WithWrongPassword_ReturnNull()
        {
            //arrange
            User expected = null;

            //act
            var actual = new UserService(_unitOfWork).Login(_userAdmin.UserName, "2");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Login_WithNonExistentUsername_ReturnNull()
        {
            //arrange
            User expected = null;

            //act
            var actual = new UserService(_unitOfWork).Login(_nonExistentUsername, _correctAdminPassword);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty username was inappropriately allowed.")]
        public void Login_EmptyUsername_ShouldThrowArgumentException()
        {
            //act
            new UserService(_unitOfWork).Login("", _correctAdminPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty password was inappropriately allowed.")]
        public void Login_EmptyPassword_ShouldThrowArgumentException()
        {
            //act
            new UserService(_unitOfWork).Login(_userAdmin.UserName, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A username of null was inappropriately allowed.")]
        public void Login_NullUsername_ShouldThrowArgumentNullException()
        {
            //act
            new UserService(_unitOfWork).Login(null, _correctAdminPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A password of null was inappropriately allowed.")]
        public void Login_NullPassword_ShouldThrowArgumentNullException()
        {
            //act
            new UserService(_unitOfWork).Login(_userAdmin.UserName, null);
        }

        [TestMethod]
        public void Register_WithCorrectUserAndPassword_ReturnUserWithNewId()
        {
            //arrange
            int expected = 3;

            //act
            var userDto = new User();
            userDto.UserName = "newuser";
            var actual = new UserService(_unitOfWork).Register(userDto, _correctAdminPassword).Id;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty password was inappropriately allowed.")]
        public void Register_EmptyPassword_ShouldThrowArgumentException()
        {
            //act
            new UserService(_unitOfWork).Register(_userAdmin, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A password of null was inappropriately allowed.")]
        public void Register_NullPassword_ShouldThrowArgumentNullException()
        {
            //act
            new UserService(_unitOfWork).Register(_userAdmin, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A user of null was inappropriately allowed.")]
        public void Register_NullUser_ShouldThrowArgumentNullException()
        {
            //act
            new UserService(_unitOfWork).Register(null, "1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A password of null was inappropriately allowed.")]
        public void CreatePasswordHash_NullPassword_ShouldThrowArgumentNullException()
        {
            //act
            new UserService(_unitOfWork).CreatePasswordHash(null, out _, out _);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty password was inappropriately allowed.")]
        public void CreatePasswordHash_EmptyPassword_ShouldThrowArgumentException()
        {
            //act
            new UserService(_unitOfWork).CreatePasswordHash("", out _, out _);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Empty username was inappropriately allowed.")]
        public void UserExists_EmptyUsername_ShouldThrowArgumentException()
        {
            //act
            new UserService(_unitOfWork).UserExists("");
        }

        [TestMethod]
        public void UserExists_WithExistUsername_ReturnsTrue()
        {
            // act
            var actual = new UserService(_unitOfWork).UserExists(_userAdmin.UserName);

            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void UserExists_WithNonExistentUsername_ReturnsFalse()
        {
            // act
            var actual = new UserService(_unitOfWork).UserExists(_nonExistentUsername);

            //assert
            Assert.IsFalse(actual);
        }

    }
}
