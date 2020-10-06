using System;
using System.Threading.Tasks;
using Logistics.Services.Nodes.Commands;
using Logistics.Services.Nodes.Queries;
using Logistics.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Logistics.Web.API.Controllers
{
    [ApiController]
    [EnableCors("AppPolicy")]
    [Route("api/[controller]")]
    public class NodesController : ControllerBase
    {
        private readonly IGetNodesQuery getNodes;
        private readonly ICreateNodeCommand createNode;
        private readonly IGetNodeByIdQuery getNodeById;
        private readonly IUpdateNodeCommand updateNode;
        private readonly IDeleteNodeCommand deleteNode;
        private readonly IGetLogisticCenterIdQuery getLogisticCenterId;

        public NodesController(
            IGetNodesQuery getNodes, 
            ICreateNodeCommand createNode, 
            IGetNodeByIdQuery getNodeById, 
            IUpdateNodeCommand updateNode, 
            IDeleteNodeCommand deleteNode, 
            IGetLogisticCenterIdQuery getLogisticCenterId)
        {
            this.getNodes = getNodes 
                ?? throw new ArgumentNullException(nameof(getNodes));
            this.createNode = createNode 
                ?? throw new ArgumentNullException(nameof(createNode));
            this.getNodeById = getNodeById 
                ?? throw new ArgumentNullException(nameof(getNodeById));
            this.updateNode = updateNode 
                ?? throw new ArgumentNullException(nameof(updateNode));
            this.deleteNode = deleteNode 
                ?? throw new ArgumentNullException(nameof(deleteNode));
            this.getLogisticCenterId = getLogisticCenterId 
                ?? throw new ArgumentNullException(nameof(getLogisticCenterId));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await getNodes.ExecuteAsync();
                return new JsonResult(data);
            }
            catch (Exception e)
            {
                return BadRequest("Could not fetch settlements");
            }
        }

        [HttpGet]
        [Route("getbyid/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await getNodeById.ExecuteAsync(id);
                return new JsonResult(data);
            }
            catch (Exception e)
            {
                return BadRequest($"Could not get settlement with id: {id}");
            }
        }

        [HttpGet]
        [Route("getlogisticcenterid")]
        public async Task<IActionResult> GetLogisticCenterId()
        {
            try
            {
                var logisticCenterId = await getLogisticCenterId.ExecuteAsync();
                return new JsonResult(logisticCenterId);
            }
            catch (Exception e)
            {
                // log error to db
                return BadRequest("Something went wrong when fetching logistic center id.");
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] NodeCreatePostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model was not valid");

            try
            {
                var result = await createNode.ExecuteAsync(model.Name);

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something happened when creating new settlement");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] NodeUpdatePostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model was not valid");

            try
            {
                var result = await updateNode.ExecuteAsync(model.Id, model.Name);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something happened when updating settlement");
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] NodeDeletePostModel model)
        {
            if (model == null || ModelState.IsValid == false) 
                return BadRequest("Invalid request.");

            try
            {
                var result = await deleteNode.ExecuteAsync(model.NodeId);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to delete settlement");
            }
        }
    }
}
