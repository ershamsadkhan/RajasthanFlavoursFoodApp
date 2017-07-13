using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.Email
{
	public class EmailDto
	{
		public string Redipient { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
	}
}