using Common.Helpers;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OnlineFoodOrderingService.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	[RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        UserManager userManager;
        Response<UserDto> response;

        public UserController(IUserRepository repository)
        {
            userManager = new UserManager(repository);
            response = new Response<UserDto>();
        }

        [Route("SignUp")]
        [HttpPost]
        public Response<UserDto> SignUp(Request<UserDto> request)
        {
            response = userManager.SignUp(request);
            return response;
        }

        [Route("LogIn")]
        [HttpPost]
        public Response<UserDto> LogIn(Request<UserDto> request)
        {
            response = userManager.LogIn(request);
            return response;
        }

		[Route("GetUsersList")]
		[HttpPost]
		public Response<UserDto> GetUsersList(Request<UserDto> request)
		{
			response = userManager.GetUsersList(request);
			return response;
		}

		[Route("GetDetails")]
		[HttpPost]
		public Response<UserDto> GetDetails(string UserId)
		{
			response = userManager.GetDetails(UserId);
			return response;
		}

		[Route("UpdateDetails")]
		[HttpPost]
		public Response<UserDto> UpdateDetails(Request<UserDto> request)
		{
			response = userManager.UpdateProfile(request);
			return response;
		}

		[Route("ForgotPassword")]
		[HttpPost]
		public Response<UserDto> ForgotPassword(string UserName)
		{
			EMailHelper mailHelper = new EMailHelper();

			response = userManager.GetForgotPasswordDetails(UserName);
			if (response.Status == true)
			{
				mailHelper.RECIPIENT = response.ObjList[0].UserEmailAddress;
				mailHelper.SUBJECT = "Forgot Password";
				mailHelper.MESSAGE = "Your Password Is: " + response.ObjList[0].UserPwd;

				response = mailHelper.SendEMail(mailHelper.RECIPIENT, mailHelper.SUBJECT, mailHelper.MESSAGE);
				if (response.Status == true)
				{
					response.ErrMsg = "Your password has sent on your Email";
				}
			}
			else
			{
				response.ErrMsg = "Email Server Error";
			}
			return response; 
		}

		[Route("GetUsersExcel")]
		[HttpPost]
		public HttpResponseMessage GetUsersExcel(Request<UserDto> request)
		{
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			response = userManager.GetUsersList(request);
			if (response.Status == true)
			{
				DataTable orderDataTable = ConvertToDataTable(response.ObjList);
				string CSVdata = DataTableToCSV(orderDataTable, ',');
				var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(CSVdata);
				//var stream = new FileStream(path, FileMode.Open);
				MemoryStream stream = new MemoryStream(bytes);
				result.Content = new StreamContent(stream);
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
				result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
				{
					FileName = "UserList" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + ".xls"
				};
			}
			return result;
		}

		#region private methods
		private DataTable ConvertToDataTable(IList<UserDto> data)
		{

			DataTable table = new DataTable();

			table.Columns.Add("UserName");
			table.Columns.Add("PrimaryAddress");
			table.Columns.Add("UserPhoneNumber");
			table.Columns.Add("UserEmailAddress");
			table.Columns.Add("RegisterDate");

			foreach (UserDto item in data)
			{
				DataRow row = table.NewRow();
				row["UserName"] = item.UserName;
				row["PrimaryAddress"] = item.PrimaryAddress.Replace(",", "");
				row["UserPhoneNumber"] = item.UserPhoneNumber;
				row["UserEmailAddress"] = item.UserEmailAddress;
				row["RegisterDate"] = item.RegisterDate;

				table.Rows.Add(row);
			}
			return table;
		}

		private string DataTableToCSV(DataTable datatable, char seperator)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < datatable.Columns.Count; i++)
			{
				sb.Append(datatable.Columns[i]);
				if (i < datatable.Columns.Count - 1)
					sb.Append(seperator);
			}
			sb.AppendLine();
			foreach (DataRow dr in datatable.Rows)
			{
				for (int i = 0; i < datatable.Columns.Count; i++)
				{
					sb.Append(dr[i].ToString());

					if (i < datatable.Columns.Count - 1)
						sb.Append(seperator);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		#endregion
	}
}
