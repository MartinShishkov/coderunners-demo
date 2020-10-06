using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Web.API.Controllers;
using Logistics.Web.API.Models;
using Logistics.Web.API.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Logistics.Web.API.Tests
{
    [TestFixture]
    public class EdgesControllerTests
    {
        private EdgesController controller;

        [SetUp]
        public void Initialize()
        {
            var db = new MockDb(new[]
            {
                new Node(1, "Varna"), 
                new Node(2, "Sofia"), 
                new Node(3, "Plovdiv"), 
            }, new[]
            {
                new Edge(1, 2), 
                new Edge(2, 3), 
                new Edge(3, 1), 
            });

            this.controller = new EdgesController(
                new MockGetEdgesQuery(db), 
                new MockDeleteEdgeCommand(db), 
                new MockCreateEdgeCommand(db)
            );
        }

        [Test]
        public async Task TestGetShouldReturnAllEdges()
        {
            var result = await controller.Get();
            var jsonResult = result as JsonResult;

            var edges = jsonResult?.Value as Edge[];

            Assert.IsNotNull(edges);
            Assert.AreEqual(3, edges.Length);
        }

        [Test]
        public async Task TestCreateValidModelShouldReturnTrue()
        {
            var model = new EdgeCreatePostModel()
            {
                From = 1, To = 2
            };

            var result = await controller.Create(model);
            var jsonResult = result as JsonResult;

            var created = jsonResult?.Value as bool?;

            Assert.IsTrue(created);
        }

        [Test]
        public async Task TestCreateInvalidModelShouldReturnErrorMessage()
        {
            controller.ModelState.AddModelError("test", "test");

            var model = new EdgeCreatePostModel
            {
                From = -1, To = 2
            };

            var result = await controller.Create(model);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid model for creating an edge", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestDeleteValidModelShouldReturnTrue()
        {
            var model = new EdgeDeletePostModel
            {
                From = 1, To = 2
            };

            var result = await controller.Delete(model);
            var jsonResult = result as JsonResult;

            var deleted = jsonResult?.Value as bool?;

            Assert.IsTrue(deleted);
        }

        [Test]
        public async Task TestDeleteInvalidModelShouldReturnErrorMessage()
        {
            controller.ModelState.AddModelError("test", "test");

            var model = new EdgeDeletePostModel
            {
                From = -1, To = 2
            };

            var result = await controller.Delete(model);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid model for deleting an edge", badRequestResult.Value as string);
        }
    }
}