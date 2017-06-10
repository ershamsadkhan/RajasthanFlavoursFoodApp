using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Models.Enums
{
	public enum OrderStatus
	{
		[Description("P")]
		Placed = 1,

		[Description("D")]
		Delivered = 2,

		[Description("C")]
		Cancelled = 3,

		[Description("O")]
		OutForDelivery = 4
	}
}