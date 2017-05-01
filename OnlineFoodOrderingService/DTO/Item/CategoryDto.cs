using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Item
{
    public class CategoryDto
    {
        public long Categoryid { get; set; }
        public string CategoryHeader { get; set; }
        public string CategoryDescription { get; set; }

        public IList<ItemDto> itemDtoList { get; set; }

        public CategoryDto()
        {
            this.itemDtoList = new List<ItemDto>();
        }
    }
}