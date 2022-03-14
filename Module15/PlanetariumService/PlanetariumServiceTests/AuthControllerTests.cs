using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using PlanetariumService.Controllers;
using PlanetariumService.Models;
using PlanetariumServices;
using System;
using System.Collections.Generic;
using PlanetariumModels;
using System.Threading.Tasks;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private IConfiguration configuration;
        private IUserService userService;
        private ILogger<AuthController> log;
        private AuthController authController;

        [SetUp]
        public void SetUp()
        {
            configuration = Substitute.For<IConfiguration>();
            userService = Substitute.For<IUserService>();
            log = Substitute.For<ILogger<AuthController>>();

            authController = new AuthController(configuration, userService, log);
        }

        [Test]
        public void GetMeTest()
        {
            var response = authController.GetMe();

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<string>), response.GetType());
        }

        [Test]
        public void RegisterTest()
        {
            var guid = new Users { Id = 2 };
            userService.Add(guid).Returns(guid);

            Users result = userService.Add(guid).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void LoginTest()
        {
            var guid = new Users { Id = 2 };
            var response = authController.Login(guid);

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(Task<ActionResult<string>>), response.GetType());
        }

    }
}
