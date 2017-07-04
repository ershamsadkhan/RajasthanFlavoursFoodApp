using OnlineFoodOrderingService.Authorization;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Manager;
using OnlineFoodOrderingService.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OnlineFoodOrderingService.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	[RoutePrefix("api/Order")]
	public class OrderController : ApiController
	{
		OrderManager orderManager;
		Response<OrderDto> response;

		public OrderController(IOrderRepository repository)
		{
			orderManager = new OrderManager(repository);
			response = new Response<OrderDto>();
		}

		[AuthorizeUser]
		[Route("PlaceOrder")]
		[HttpPost]
		public Response<OrderDto> PlaceOrder(Request<OrderDto> request)
		{
			response = orderManager.PlaceOrder(request);
			return response;
		}

		[AuthorizeUser]
		[Route("OrderDelivered")]
		[HttpPost]
		public Response<OrderDto> OrderDelivered(Request<OrderDto> request)
		{
			response = orderManager.UpdateOrderStatus(request, OrderStatus.Delivered);
			return response;
		}

		[AuthorizeUser]
		[Route("OrderOutForDelivery")]
		[HttpPost]
		public Response<OrderDto> OrderOutForDelivery(Request<OrderDto> request)
		{
			response = orderManager.UpdateOrderStatus(request, OrderStatus.OutForDelivery);
			return response;
		}

		[AuthorizeUser]
		[Route("CancelOrder")]
		[HttpPost]
		public Response<OrderDto> OrderCancelled(Request<OrderDto> request)
		{
			response = orderManager.UpdateOrderStatus(request, OrderStatus.Cancelled);
			return response;
		}

		[Route("GetOrders")]
		[HttpPost]
		public Response<OrderDto> GetOrders(Request<OrderSearch> request)
		{
			response = orderManager.GetOrders(request);
			return response;
		}

		[Route("GetOrderDetails")]
		[HttpPost]
		public Response<OrderDto> GetOrderDetails(Request<OrderDto> request)
		{
			response = orderManager.GetOrderDetails(request);
			return response;
		}

		[Route("GetOrdersExcel")]
		[HttpPost]
		public HttpResponseMessage GetOrdersExcel(Request<OrderSearch> request)
		{
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			response = orderManager.GetOrders(request);
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
					FileName = "OrderList" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + ".xls"
				};
			}
			return result;
		}

		#region private methods
		private DataTable ConvertToDataTable(IList<OrderDto> data)
		{

			DataTable table = new DataTable();

			table.Columns.Add("OrderNo");
			table.Columns.Add("OrderDate");
			table.Columns.Add("UserName");
			table.Columns.Add("PhoneNumber");
			table.Columns.Add("DeliveryAddress");
			table.Columns.Add("CityCode");
			table.Columns.Add("OrderStatus");
			table.Columns.Add("GrandTotal");
			table.Columns.Add("AppliedOfferCode");
			table.Columns.Add("GrandTotalAfterDiscount");

			foreach (OrderDto item in data)
			{
				DataRow row = table.NewRow();
				row["OrderNo"] = item.OrderNo;
				row["OrderDate"] = item.OrderDate;
				row["UserName"] = item.UserName;
				row["PhoneNumber"] = item.PhoneNumber;
				row["DeliveryAddress"] = item.DeliveryAddress.Replace(",","");
				row["CityCode"] = GetCity(item.CityCode);
				row["OrderStatus"] = GetStatus(item.OrderStatus);
				row["GrandTotal"] = item.GrandTotal;
				row["AppliedOfferCode"] = item.AppliedOfferCode;
				row["GrandTotalAfterDiscount"] = item.GrandTotalAfterDiscount;

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

		private string GetCity(int cityCode)
		{
			string city = "";
			switch (cityCode)
			{
				case 1:
					city = "Gomtinagar";
					break;
				case 2:
					city = "Mahanagar";
					break;
				case 3:
					city = "Indranagar";
					break;
				case 4:
					city = "Gomtinagar Extension";
					break;
			}
			return city;
		}

		private string GetStatus(string statusCode)
		{
			string status = "";
			switch (statusCode)
			{
				case "P":
					status = "Placed";
					break;
				case "O":
					status = "Out For Delivery";
					break;
				case "C":
					status = "Cancelled";
					break;
				case "D":
					status = "Delivered";
					break;
			}
			return status;
		}
		#endregion
	}
}
