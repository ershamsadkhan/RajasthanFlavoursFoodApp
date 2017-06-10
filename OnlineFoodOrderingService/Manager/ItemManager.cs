using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Item;
using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.Manager
{
	public class ItemManager
	{
		Response<CategoryDto> response;
		IItemRepository repository;

		public ItemManager(IItemRepository repository)
		{
			this.repository = repository;
			response = new Response<CategoryDto>();
		}

		#region public methods
		public Response<CategoryDto> GetItems(Request<CategoryDto> request)
		{
			response = ValidateItem(request);
			if (response.Status == true)
			{
				response = repository.GetItems(request);
			}
			return response;
		}

		public Response<CategoryDto> GetCategory(Request<CategoryDto> request)
		{

			response = repository.GetCategory(request);

			return response;
		}

		public Response<CategoryDto> AddCategory(Request<CategoryDto> request)
		{
			response = ValidateItem(request);
			if (response.Status == true)
			{
				response = repository.AddCategory(request);
			}
			return response;
		}

		public Response<CategoryDto> AddItems(Request<CategoryDto> request)
		{
			response = ValidateItem(request);
			if (response.Status == true)
			{
				response = repository.AddItems(request);
			}
			return response;
		}

		#endregion

		#region private methods
		private Response<CategoryDto> ValidateItem(Request<CategoryDto> request)
		{
			response.Status = true;
			return response;
		}
		#endregion

	}
}