
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Item
{
    public class ItemDto
    {
        public long Itemid { get; set; }
        public long Categoryid { get; set; }
        public string ItemHeader { get; set; }
        public string ItemDescription { get; set; }
        public long QuaterPrice { get; set; }
        public long HalfPrice { get; set; }
        public long FullPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}