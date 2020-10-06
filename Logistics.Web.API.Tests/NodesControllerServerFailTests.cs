using System.Threading.Tasks;
using Logistics.Web.API.Controllers;
using Logistics.Web.API.Models;
using Logistics.Web.API.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Logistics.Web.API.Tests
{
    [TestFixture]
    public class NodesControllerServerFailTests
    {
        private NodesController controller;

        [SetUp]
        public void Initialize()
        {
            this.controller = new NodesController(
                new FailingGetNodesQuery(), 
                new FailingCreateNodeCommand(), 
                new FailingGetNodeByIdQuery(), 
                new FailingUpdateNodeCommand(), 
                new FailingDeleteNodeCommand(), 
                new FailingGetLogisticCenterByIdQuery()
            );
        }

        [Test]
        public async Task TestGetAllServerExceptionShouldReturnErrorMessage()
        {
            var result = await controller.Get();
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Could not fetch settlements", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestCreateServerExceptionShouldReturnErrorMessage()
        {
            var result = await controller.Create(new NodeCreatePostModel(){Name = "Sofia"});
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Something happened when creating new settlement", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestUpdateServerExceptionShouldReturnErrorMessage()
        {
            var result = await controller.Update(new NodeUpdatePostModel{Id = 1, Name = "Sofia"});
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Something happened when updating settlement", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestDeleteServerExceptionShouldReturnErrorMessage()
        {
            var result = await controller.Delete(new NodeDeletePostModel{NodeId = 1});
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to delete settlement", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestGetByIdServerExceptionShouldReturnErrorMessage()
        {
            var id = 1;
            var result = await controller.GetById(id);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual($"Could not get settlement with id: {id}", badRequestResult.Value as string);
        }

        [Test]
        public async Task TestGetLogisticCenterIdServerExceptionShouldReturnErrorMessage()
        {
            var result = await controller.GetLogisticCenterId();
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Something went wrong when fetching logistic center id.", badRequestResult.Value as string);
        }
    }
}