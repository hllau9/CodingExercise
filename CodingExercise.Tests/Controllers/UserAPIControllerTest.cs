using CodingExercise.APIControllers;
using CodingExercise.DAL;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace CodingExercise.Tests.Controllers
{
    [TestClass]
    public class UserAPIControllerTest
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
        public void GetUsersTest()
        {
            // Arrange
            UserController controller = new UserController(_userService);

            // Act
            IHttpActionResult result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<List<UserVM>>), result.GetType());
        }
    }
}
