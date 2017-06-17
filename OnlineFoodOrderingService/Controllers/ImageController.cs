using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OnlineFoodOrderingService.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class ImageController : ApiController
    {
		[HttpGet]
		public HttpResponseMessage GetImage(String ImageName)
		{

			String path = HttpContext.Current.Server.MapPath("~/images/")+ImageName;
			var fileStream = new FileStream(path, FileMode.Open,FileAccess.Read);

			var resp = new HttpResponseMessage()
			{
				Content = new StreamContent(fileStream)
			};

			// Find the MIME type
			//string mimeType = _extensions[Path.GetExtension(path)];
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

			return resp;
		}
    }
}