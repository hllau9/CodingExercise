using CodingExercise.APIControllers;
using CodingExercise.DAL;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

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
        public void Create()
        {
            // Arrange
            UserController controller = new UserController(_userService);

            // Act
            IHttpActionResult result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<string>), result.GetType());
        }
    }
}
