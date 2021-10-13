using CodingExercise.Controllers;
using CodingExercise.DAL;
using CodingExercise.Entities;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
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
            //manual mocking
            _userManager = new UserManagerMock();
            _userService = new UserServiceMock(_userManager);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            //using Moq
            var userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(s => s.GetUsers()).Returns(new List<AppUser>());

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUsers()).Returns(userManagerMock.Object.GetUsers);

            UserController controller = new UserController(userServiceMock.Object);

            ViewResult result = controller.Index(It.IsAny<int>()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(PagedList.PagedList<UserVM>), result.Model.GetType());
        }

        [TestMethod]
        public void GetUsersTestException()
        {
            //using Moq
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUsers()).Throws(new Exception());

            UserController controller = new UserController(userServiceMock.Object);

            ViewResult result = controller.Index(It.IsAny<int>()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(PagedList.PagedList<UserVM>), result.Model.GetType());
        }

        [TestMethod]
        public void GetUsersTestExceptionManual()
        {
            //manually mock the exception in the DAL

            UserController controller = new UserController(_userService);

            ViewResult result = controller.Index(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(PagedList.PagedList<UserVM>), result.Model.GetType());
        }

        [TestMethod]
        public void InvalidPageParameterTest()
        {
            //using Moq
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUsers()).Returns(new List<AppUser>());

            UserController controller = new UserController(userServiceMock.Object);

            ViewResult result = controller.Index(-100) as ViewResult;       // page value has to be >= 1

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(PagedList.PagedList<UserVM>), result.Model.GetType());
        }
    }
}
