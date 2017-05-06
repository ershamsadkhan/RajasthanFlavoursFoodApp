using OnlineFoodOrderingService.Authorization;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineFoodOrderingService.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        OrderManager orderManager;
        Response<OrderDto> response;

        public OrderController(IOrderRepository repository)
        {
            orderManager = new OrderManager(repository);
            response = new Response<OrderDto>();
        }

        //[AuthorizeUser]
        [Route("PlaceOrder")]
        [HttpPost]
        public Response<OrderDto> PlaceOrder(Request<OrderDto> request)
        {
            response=orderManager.PlaceOrder(request);
            return response;
        }

        [Route("GetOrderList")]
        [HttpPost]
        public Response<OrderDto> GetOrderList(Request<OrderDto> request)
        {
            return response;
        }

        [Route("GetOrderDetails")]
        [HttpPost]
        public Response<OrderDto> GetOrderDetails(Request<OrderDto> request)
        {
            return response;
        }
    }
}
