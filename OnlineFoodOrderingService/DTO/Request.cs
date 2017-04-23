using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO
{
    public int test{ get; set; }
    public class Request<T> where T :class
    {
        public T Obj;
    }
}