using OnlineFoodOrderingService.DTO.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Models.Order
{
    public class OrderLineItem
    {
        public long OrderLineItemId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public short PriceType { get; set; }
        public int ItemId { get; set; }

        //public ItemDto item {get;set;}
    }
}