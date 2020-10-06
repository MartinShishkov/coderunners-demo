using System.ComponentModel.DataAnnotations;

namespace Logistics.Web.API.Models
{
    public class NodeCreatePostModel
    {
        [Required]
        public string Name { get; set; }
    }
}