using OnlineFoodOrderingService.Authorization;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Offer;
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

        public OfferController(IOfferRepository repository)
        {
            offerManager = new OfferManager(repository);
            response = new Response<OfferDto>();
        }
        [Route("GetOffers")]
        [HttpPost]
        public Response<OfferDto> GetOffers(Request<OfferDto> request)
        {
            response = offerManager.GetOffers(request);
            return response;
        }



    }
}