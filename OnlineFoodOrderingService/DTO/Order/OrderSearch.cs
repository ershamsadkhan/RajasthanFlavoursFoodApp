using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Order
{
    public class OrderSearch
    {
        public long OrderNo { get; set; }
        public long UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
		public char Type { get; set; }
		public int CityCode { get; set; }
    }
}