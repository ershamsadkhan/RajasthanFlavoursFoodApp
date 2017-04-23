using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO
{
    public class Response<T> where T:class
    {
        public T Obj;
        public IList<T> ObjList;

        public string OrderNo { get; set; }
        public string ErrMsg { get; set; }
        public bool Status { get; set; }

    }
}