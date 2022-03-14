using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PlanetariumService.Controllers;
using PlanetariumService.Models;
using PlanetariumServices;
using System;
using System.Collections.Generic;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class TicketControllerTests
    {
        private ITicketService ticketService;
        private IMapper mapper;
        private ILogger<TicketController> log;
        private TicketController ticketController;

        [SetUp]
        public void SetUp()
        {
            ticketService = Substitute.For<ITicketService>();
            mapper = Substitute.For<IMapper>();
            log = Substitute.For<ILogger<TicketController>>();

            ticketController = new TicketController(ticketService, mapper, log);
        }

        [Test]
        public void OrderThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => ticketController.Order(null));
        }

        [Test]
        public void OrderTest()
        {
            var response = ticketController.Order(2);

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<List<TicketUI>>), response.GetType());
        }

        [Test]
        public void BuyNull()
        {
            var response = ticketController.Buy(Array.Empty<int>());
            Assert.AreEqual(0, response.Value);
        }

        [Test]
        public void BuyTest()
        {
            var response = ticketController.Buy(new int[] {55,56});
            Assert.AreEqual(2, response.Value);
        }

        /*[Test]
        public void Order_ThrowsException()
        {
            Assert.AreEqual(null, ticketController.Order(55));
        }*/      
    }
}