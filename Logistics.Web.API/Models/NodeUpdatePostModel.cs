using System.ComponentModel.DataAnnotations;
using Logistics.Web.API.Attributes;

namespace Logistics.Web.API.Models
{
    public class NodeUpdatePostModel
    {
        [IdValidation]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}