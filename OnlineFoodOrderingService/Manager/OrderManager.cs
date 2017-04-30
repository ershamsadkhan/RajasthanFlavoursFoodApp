﻿using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Manager
{
    public class OrderManager
    {
        Response<OrderDto> response;
        IOrderRepository repository;

        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
            response = new Response<OrderDto>();
        }

        #region public methods
        public Response<OrderDto> PlaceOrder(Request<OrderDto> request)
        {
            response = ValidateOrder(request);
            if (response.Status == true)
            {
                response = repository.PlaceOrder(request);
            }
            return response;
        }
        #endregion

        #region private methods
        private Response<OrderDto> ValidateOrder(Request<OrderDto> request)
        {
            response.Status = true;
            return response;
        }
        #endregion
    }
}