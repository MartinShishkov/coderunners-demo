using Logistics.Web.API.Attributes;

namespace Logistics.Web.API.Models
{
    public class EdgeCreatePostModel
    {
        [IdValidation]
        public int From { get; set; }

        [IdValidation]
        public int To { get; set; }
    }
}