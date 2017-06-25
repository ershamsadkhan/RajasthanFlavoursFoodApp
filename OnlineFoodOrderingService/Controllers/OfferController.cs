using OnlineFoodOrderingService.Authorization;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Offer;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Manager;
using OnlineFoodOrderingService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineFoodOrderingService.Controllers
{
    [RoutePrefix("api/Offer")]
    public class OfferController : ApiController
    {
        OfferManager offerManager;
        Response<OfferDto> response;
        Response<OrderDto> OrderResponse;

        public OfferController(IOfferRepository repository)
        {
            offerManager = new OfferManager(repository);
            response = new Response<OfferDto>();
            OrderResponse = new Response<OrderDto>();
        }
        [Route("GetOffers")]
        [HttpPost]
        public Response<OfferDto> GetOffers(Request<OfferDto> request)
        {
            response = offerManager.GetOffers(request);
            return response;
        }
        [AuthorizeUser]
        [Route("Applicableoffers")]
        [HttpPost]
        public Response<OfferDto> ApplicableOffers(Request<OfferDto> request)
        {
            response = offerManager.ApplicableOffers(request);
            return response;
        }

		[AuthorizeUser]
		[Route("GetAppliedOffers")]
		[HttpPost]
		public Response<OfferDto> GetAppliedOffers(long OrderNo)
		{
			response = offerManager.GetAppliedOffers(OrderNo);
			return response;
		}



	}
}