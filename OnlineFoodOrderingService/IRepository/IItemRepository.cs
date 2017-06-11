using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrderingService.IRepository
{
    public interface IItemRepository
    {
        Response<CategoryDto> GetItems(Request<CategoryDto> request);
		Response<CategoryDto> GetCategory(Request<CategoryDto> request);
		Response<CategoryDto> AddCategory(Request<CategoryDto> request);
        Response<CategoryDto> AddItems(Request<CategoryDto> request);
        Response<ItemDto> UpdateItem(Request<ItemDto> request);
        Response<CategoryDto> UpdateCategory(Request<CategoryDto> request);
    }
}
