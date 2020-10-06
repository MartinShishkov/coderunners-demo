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
    public class NodesControllerTests
    {
        private NodesController controller;

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

            this.controller = new NodesController(
                new MockGetNodesQuery(db), 
                new MockCreateNodeCommand(), 
                new MockGetNodeByIdQuery(db), 
                new MockUpdateNodeCommand(), 
                new MockDeleteNodeCommand(), 
                new MockGetLogisticCenterIdQuery()
            );
        }

        [Test]
        public async Task TestGetShouldReturnAllNodes()
        {
            var result = await controller.Get();
            var jsonResult = result as JsonResult;

            var nodes = jsonResult?.Value as Node[];

            Assert.IsNotNull(nodes);
            Assert.AreEqual(3, nodes.Length);
        }

        [Test]
        public async Task TestCreateValidModelShouldReturnTrue()
        {
            var model = new NodeCreatePostModel()
            {
                Name = "Sofia"
            };

            var result = await controller.Create(model);
            var jsonResult = result as JsonResult;

            var node = jsonResult?.Value as Node;

            Assert.IsNotNull(node);
            Assert.AreEqual(1, node.Id);
            Assert.AreEqual("Sofia", node.Name);
        }

        [Test]
        public async Task TestCreateInvalidModelShouldReturnErrorMessage()
        {
            controller.ModelState.AddModelError("test", "test");

            var model = new NodeCreatePostModel
            {
                Name = ""
            };

            var result = await controller.Create(model);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("The model was not valid", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestDeleteValidModelShouldReturnTrue()
        {
            var model = new NodeDeletePostModel()
            {
                NodeId = 1
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

            var model = new NodeDeletePostModel()
            {
                NodeId = -1
            };

            var result = await controller.Delete(model);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid request.", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestUpdateValidModelShouldReturnTrue()
        {
            var model = new NodeUpdatePostModel()
            {
                Id = 1,
                Name = "Sofia"
            };

            var result = await controller.Update(model);
            var jsonResult = result as JsonResult;

            var updated = jsonResult?.Value as bool?;

            Assert.IsTrue(updated);
        }

        [Test]
        public async Task TestUpdateInvalidModelShouldReturnErrorMessage()
        {
            controller.ModelState.AddModelError("test", "test");

            var model = new NodeUpdatePostModel()
            {
                Id = 1,
                Name = "Sofia"
            };

            var result = await controller.Update(model);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("The model was not valid", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestGetLogisticCenterIdShouldReturnCorrectId()
        {
            var result = await controller.GetLogisticCenterId();
            var jsonResult = result as JsonResult;

            var id = jsonResult?.Value as int?;

            Assert.IsNotNull(id);
            Assert.AreEqual(1, id);
        }

        
    }
}