using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Offer;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrderingService.IRepository
{
    public interface IOfferRepository 
    {
        Response<OfferDto> GetOffers(Request<OfferDto> request);
        Response<OfferDto> ApplicableOffers(Request<OfferDto> request);


    }
}