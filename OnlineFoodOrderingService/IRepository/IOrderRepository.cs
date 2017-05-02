using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
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
    }
}
