using CodingExercise.Controllers;
using CodingExercise.DAL;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace CodingExercise.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private IUserManager _userManager;
        private IUserService _userService;

        [TestInitialize]
        public void SetUp()
        {
            _userManager = new UserManagerMock();
            _userService = new UserServiceMock(_userManager);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            UserController controller = new UserController(_userService);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(UserVM), result.Model.GetType());
        }
    }
}
