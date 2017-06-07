using OnlineFoodOrderingService.DTO.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Order
{
    public class OrderLineItemDto
    {
        public long OrderNo { get; set; }
        public long OrderLineItemId { get; set; }
        public string ItemHeader { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public short PriceType { get; set; }
        public int ItemId { get; set; }
		public string ImageUrl { get; set; }

		public ItemDto item {get;set;}
    }
}