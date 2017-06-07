using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Order
{
	public class OrderDto
	{
		public long OrderNo { get; set; }
		public DateTime OrderDate { get; set; }
		public long GrandTotal { get; set; }
		public long UserId { get; set; }
		public string DeliveryAddress { get; set; }
		public int CityCode { get; set; }

		public IList<OrderLineItemDto> OrderLineItemList { get; set; }
	}
}