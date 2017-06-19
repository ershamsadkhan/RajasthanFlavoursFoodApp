using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Offer;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Manager
{
    public class OfferManager 
    {
        Response<OfferDto> response;
        IOfferRepository repository;
        Response<OrderDto> OrderResponse;

        public OfferManager(IOfferRepository repository)
        {
            this.repository = repository;
            response = new Response<OfferDto>();
            OrderResponse = new Response<OrderDto>();
        }
        #region public methods

        public Response<OfferDto> GetOffers(Request<OfferDto> request)
        {
            response = repository.GetOffers(request);
            return response;
        }

        public Response<OrderDto> ApplicableOffers(Request<OrderDto> request)
        {
            OrderResponse = repository.ApplicableOffers(request);
            return OrderResponse;
        }

        #endregion
    }
}