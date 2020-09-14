using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBRTEST.DAL;
using HBRTEST.Entities;
using HBRTEST.Utilities;

namespace HBRTEST.UnitTest
{
    [TestClass]
    public class UsersTest
    {
        [TestMethod]
        public void CreationUserTestMethod()
        {
            //Arrange
            int creationWantedResult = 1;
            UsersDAL _usersRepository = new UsersDAL();

            UserEntity user = new UserEntity();
            user.FirstName = "Pedro";
            user.LastName = "Contreras";
            user.CellPhone = "8095900000";
            user.Genre = "M";
            user.Email = "pedro@mail.com";
            user.UserName = "Pedro123";
            user.Password = "pedro12";
            //Act
            int creationTestResult = _usersRepository.CreateUser(user);
            //Assert
            Assert.AreEqual(creationWantedResult, creationTestResult);
        }

        [TestMethod]
        public void SigInUserTestMethod()
        {
            //Arrange
            UsersDAL _userRepository = new UsersDAL();

            int userIdWanted = 1;
            string userFirstNameWanted = "Pedro";
            string userLastNameWanted = "Contreras";
            string userCellPhoneWanted = "8295923333";
            string userEmailWanted = "pedro@mail.com";
            string userGenreWanted = "M";
            string userUserNameWanted = "Pedro12";
            string userPasswordWanted = "pedro12";
            //Act
            UserEntity userResult = _userRepository.SignIn("Pedro12", "pedro12");

            //Assert
            Assert.AreEqual(userIdWanted, userResult.UserId);
            Assert.AreEqual(userFirstNameWanted, userResult.FirstName);
            Assert.AreEqual(userLastNameWanted, userResult.LastName);
            Assert.AreEqual(userCellPhoneWanted, userResult.CellPhone);
            Assert.AreEqual(userEmailWanted, userResult.Email);
            Assert.AreEqual(userGenreWanted, userResult.Genre);
            Assert.AreEqual(userUserNameWanted, userResult.UserName);
            Assert.AreEqual(userPasswordWanted, userResult.Password);
        }

        [TestMethod]
        public void UpdateProfileTestMethod()
        {
            //Arrange
            UsersDAL _userRepository = new UsersDAL();

            int updateResultWanted = 1;
            UserEntity user = new UserEntity();
            user.UserId = 1;
            user.FirstName = "Juan";
            user.LastName = "Contreras";
            user.CellPhone = "8294443333";
            user.Genre = "M";
            user.Email = "juan@mail.com";
            user.Password = "juan12";
            //Act
            int updateResult = _userRepository.UpdateProfile(user);

            //Assert
            Assert.AreEqual(updateResultWanted, updateResult);
        }
    }
}
