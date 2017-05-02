using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;

namespace OnlineFoodOrderingService.SQLRepository
{
    public class SQLOrderRepository : IOrderRepository
    {
        public Response<OrderDto> PlaceOrder(Request<OrderDto> request)
        {
            throw new NotImplementedException();
        }
    }
}