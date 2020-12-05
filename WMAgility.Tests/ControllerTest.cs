using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WMAgility2.Controllers;
using WMAgility2.Data;
using WMAgility2.Models;

namespace WMAgility.Tests
{
    // all controller tests failing because I can not get the constructor to take in the necessary arguments

    [TestClass]
    public class ControllerTests
    {
        
        [TestMethod]
        public void HttpContext_Test()
        {
            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new[] {new
                Claim(ClaimTypes.Name, "username"), new Claim(ClaimTypes.Role, "Admin")}, "Cookies")));

            var context = mockContext.Object;
            var user = context.User;

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Identity.IsAuthenticated);
            Assert.IsTrue(user.HasClaim(ClaimTypes.Name, "username"));
        }

        //[TestMethod]
        //public void SkillsController_Does_Not_Return_Null()
        //{
        //var mockRepository = new Mock<ISkillRepository>();
        //mockRepository.Setup(x => x.AllSkills).Returns(new List<Skill>());

        //var db = new ApplicationDbContext();
        //var webHostEnvironment = new IWebHostEnvironment();
        ////var skillRepository = new ISkillRepository(); //using mock instead
        //var practiceRepository = new IPracticeRepository();
        //var logger = new ILogger();
        //var userManager = new UserManager();

        //var controller = new SkillsController(db, webHostEnvironment, mockRepository.Object, practiceRepository, logger, userManager);

        //Assert.IsNotNull(controller);
        //}

        //[TestMethod]
        //public void AccountController_LogOut_Should_Fail_With_InvalidOwner_Given_Wrong_User()
        //{
        //    var _accountController = new AccountController();

        //    //set by default
        //    var mockContext = MockContext("billy");

        //    // mock an authenticated user
        //    _accountController.ControllerContext = mockContext.Object;

        //    ViewResult result = _accountController.Logout() as ViewResult;
        //    Assert.AreEqual("InvalidOwner", result.ViewName);
        //}
    }
}

        //Mock<ControllerContext> MockContext(string userName)
            //var mockContext = new Mock<ControllerContext>();
            //// mock an authenticated user
            //mockContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            //mockContext.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            //return mockContext;