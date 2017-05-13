using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Models.Order
{
    public class Order
    {
        public long OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public long GrandTotal { get; set; }

        public IList<OrderLineItem> OrderLineItemList { get; set; }
    }
}