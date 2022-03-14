using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PlanetariumModels;
using PlanetariumService.Controllers;
using PlanetariumService.Models;
using PlanetariumServices;
using System;
using System.Collections.Generic;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class PostersControllerTests
    {
        private IPosterService posterService;
        private IHallService hallService;
        private IPerformanceService performanceService;
        private ITicketService ticketService;
        private IMapper mapper;
        private ILogger<PostersController> log;
        private PostersController posterController;

        [SetUp]
        public void SetUp()
        {
            posterService = Substitute.For<IPosterService>();
            hallService = Substitute.For<IHallService>();
            performanceService = Substitute.For<IPerformanceService>();
            ticketService = Substitute.For<ITicketService>();
            mapper = Substitute.For<IMapper>();
            log = Substitute.For<ILogger<PostersController>>();

            posterController = new PostersController(posterService, hallService, ticketService, performanceService, mapper, log);
        }
        [Test]
        public void PostersDetailsTest()
        {
            var guid = new Poster { Id = 2};
            posterService.GetById(2).Returns(guid);

            Poster result = posterService.GetById(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result);
            Assert.AreEqual(2, result.Id);
        }


        [Test]
        public void PostersNullTest()
        {
            var response = posterController.Posters(null, null);

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<List<PosterUI>>), response.GetType());
        }


        [Test]
        public void PostersNotNullTest()
        {
            var response = posterController.Posters(new DateTime(10000000000), new DateTime(20000000000000));

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<List<PosterUI>>), response.GetType());
        }

        [Test]
        public void AddPostersTest()
        {
            var response = posterController.AddPosters();

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<List<PosterUI>>), response.GetType());
        }

        [Test]
        public void CreateTest()
        {
            var guid = new Poster { Id = 2 };
            posterService.Add(guid).Returns(guid);

            Poster result = posterService.Add(guid).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result);
            Assert.AreEqual(2, result.Id);
        }
        [Test]
        public void CreateTicketsTest()
        {
            var guid = new Ticket { Id = 2 };
            ticketService.Add(guid).Returns(guid);

            Ticket result = ticketService.Add(guid).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void EditNullTest()
        {
            var guid = new Poster { Id = 2 };
            var result = posterController.Edit(1, guid).Result;

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void EditTest()
        {
            var guid = new Poster { Id = 2 };
            posterService.Update(guid).Returns(guid);

            var result = posterController.Edit(2, guid).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void DeleteTest()
        {
            int id = 2;
            int result = posterController.Delete(id).Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result);
        }
    }

}
