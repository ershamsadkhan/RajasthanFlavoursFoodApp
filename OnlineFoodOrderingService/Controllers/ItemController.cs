using OnlineFoodOrderingService.Authorization;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Item;
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
    [RoutePrefix("api/Item")]
    public class ItemController : ApiController
    {
        ItemManager itemManager;
        Response<CategoryDto> response;

        #region public methods
        public ItemController(IItemRepository repository)
        {
            itemManager = new ItemManager(repository);
            response = new Response<CategoryDto>();
        }
        #endregion

        [Route("GetItems")]
        [HttpPost]
        public Response<CategoryDto> GetItems(Request<CategoryDto> request)
        {
            response = itemManager.GetItems(request);
            return response;
        }

        [Route("AddItems")]
        [HttpPost]
        public Response<CategoryDto> AddItems(Request<CategoryDto> request)
        {
            response = itemManager.AddItems(request);
            return response;
        }

        [AuthorizeUser]
        [Route("AddCategory")]
        [HttpPost]
        public Response<CategoryDto> AddCategory(Request<CategoryDto> request)
        {
            response = itemManager.AddCategory(request);
            return response;
        }

    }
}
