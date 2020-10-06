using System;
using System.Threading.Tasks;
using Logistics.Services.Edges.Commands;
using Logistics.Services.Edges.Queries;
using Logistics.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Logistics.Web.API.Controllers
{
    [ApiController]
    [EnableCors("AppPolicy")]
    [Route("api/[controller]")]
    public class EdgesController : ControllerBase
    {
        private readonly IGetEdgesQuery getEdges;
        private readonly IDeleteEdgeCommand deleteEdge;
        private readonly ICreateEdgeCommand createEdge;

        public EdgesController(
            IGetEdgesQuery getEdges, 
            IDeleteEdgeCommand deleteEdge, 
            ICreateEdgeCommand createEdge)
        {
            this.getEdges = getEdges 
                            ?? throw new ArgumentNullException(nameof(getEdges));
            this.deleteEdge = deleteEdge 
                            ?? throw new ArgumentNullException(nameof(deleteEdge));
            this.createEdge = createEdge 
                            ?? throw new ArgumentNullException(nameof(createEdge));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var edges = await getEdges.ExecuteAsync();
                return new JsonResult(edges);
            }
            catch (Exception e)
            {
                return BadRequest("Could not fetch edges");
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] EdgeCreatePostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model for creating an edge");

            try
            {
                var res = await createEdge.ExecuteAsync(model.From, model.To);
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                return BadRequest("Create edge failed");
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] EdgeDeletePostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest($"Invalid model for deleting an edge");

            try
            {
                var res = await deleteEdge.ExecuteAsync(model.From, model.To);
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                return BadRequest("Edge delete failed");
            }
        }
    }
}
