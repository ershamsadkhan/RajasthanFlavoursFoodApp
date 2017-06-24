using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineFoodOrderingService.DTO.Offer;

namespace OnlineFoodOrderingService.DTO.Order
{
	public class OrderDto
	{
		public long OrderNo { get; set; }
		public DateTime OrderDate { get; set; }
		public long GrandTotal { get; set; }
		public long UserId { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string DeliveryAddress { get; set; }
		public int CityCode { get; set; }
		public string OrderStatus { get; set; }

		public string AppliedOffer { get; set; }
        public IList<OfferDto> OfferList { get; set; }

        public IList<OrderLineItemDto> OrderLineItemList { get; set; }
	}
}