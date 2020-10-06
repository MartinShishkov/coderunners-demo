using Logistics.Web.API.Attributes;

namespace Logistics.Web.API.Models
{
    public class NodeDeletePostModel
    {
        [IdValidation]
        public int NodeId { get; set; }
    }
}