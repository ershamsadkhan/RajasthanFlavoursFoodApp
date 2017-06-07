using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace OnlineFoodOrderingService.Controllers
{
    public class ImageController : ApiController
    {
		[HttpGet]
		public HttpResponseMessage GetImage(String ImageName)
		{

			String path = "E:\\images\\"+ImageName;
			var fileStream = new FileStream(path, FileMode.Open);

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
