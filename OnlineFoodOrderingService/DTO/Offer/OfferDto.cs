using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrderingService.DTO.Offer
{
    public class OfferDto 
    {
        public int OfferId { get; set; }
        public string OfferHeader { get; set; }
        public string OfferDescription { get; set; }
        public string OfferCode { get; set; }
        public bool IsActive { get; set; }
        public int PercentOffer { get; set; }
        public int RsOffer { get; set; }
		public string UserId { get; set; }
	}
}