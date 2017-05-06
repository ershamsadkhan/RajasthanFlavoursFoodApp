using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrderingService.IRepository
{
    public interface IOrderRepository
    {
        Response<OrderDto> PlaceOrder(Request<OrderDto> request);
        Response<OrderDto> UpdateOrderStatus(Request<OrderDto> request, OrderStatus orderStatus);
    }
}
