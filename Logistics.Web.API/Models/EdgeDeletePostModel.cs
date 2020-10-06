using Logistics.Web.API.Attributes;

namespace Logistics.Web.API.Models
{
    public class EdgeDeletePostModel
    {
        [IdValidation]
        public int From { get; set; }

        [IdValidation]
        public int To { get; set; }
    }
}