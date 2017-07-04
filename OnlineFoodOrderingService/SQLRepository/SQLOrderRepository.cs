using OnlineFoodOrderingService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Order;
using System.Data;
using System.Data.SqlClient;
using OnlineFoodOrderingService.Models;
using System.Xml.Linq;
using OnlineFoodOrderingService.Models.Enums;
using OnlineFoodOrderingService.DTO.Offer;

namespace OnlineFoodOrderingService.SQLRepository
{
	public class SQLOrderRepository : IOrderRepository
	{
		string connection;
		Response<OrderDto> response;

		public SQLOrderRepository()
		{
			response = new Response<OrderDto>();
			connection = Settings.ConnectionString;
		}

		#region public method
		public Response<OrderDto> PlaceOrder(Request<OrderDto> request)
		{
			long OrderNo = 0;
			DataSet ds = new DataSet("PlaceOrderResponse");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					var lineItemXmlString = XmlParseToString(request);
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_AddOrder", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@lineItemXml", SqlDbType.VarChar);
					command.Parameters.Add("@UserId", SqlDbType.VarChar);
					command.Parameters.Add("@DeliveryAddress", SqlDbType.VarChar);
					command.Parameters.Add("@CityCode", SqlDbType.Int);

					//substitute value
					command.Parameters["@lineItemXml"].Value = lineItemXmlString;
					command.Parameters["@UserId"].Value = request.Obj.UserId;
					command.Parameters["@DeliveryAddress"].Value = request.Obj.DeliveryAddress;
					command.Parameters["@CityCode"].Value = request.Obj.CityCode;
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					response.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);
					response.ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
					response.OrderNo = ds.Tables[0].Rows[0]["OrderNo"].ToString();

					OrderNo = Int64.Parse(response.OrderNo);
					if (response.Status == true && OrderNo > 0 && request.Obj.AppliedOfferCode!="")
					{
						ApplyOffer(OrderNo, request.Obj.AppliedOfferCode);
					}
				}
				catch (Exception ex)
				{
					response.Status = false;
					response.ErrMsg = ex.Message;
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
			return response;
		}

		public Response<OrderDto> UpdateOrderStatus(Request<OrderDto> request, OrderStatus orderStatus)
		{
			DataSet ds = new DataSet("UpdateOrderResponse");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_UpdateOrderStatus", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@OrderNo", SqlDbType.Int);
					command.Parameters.Add("@OrderStatus", SqlDbType.VarChar);

					//substitute value
					command.Parameters["@OrderNo"].Value = request.Obj.OrderNo;
					command.Parameters["@OrderStatus"].Value = orderStatus.GetDescription();
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					response.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);
					response.ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
				}
				catch (Exception ex)
				{
					response.Status = false;
					response.ErrMsg = ex.Message;
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
			return response;
		}

		public Response<OrderDto> GetOrders(Request<OrderSearch> request)
		{
			IList<OrderDto> Orders = new List<OrderDto>();
			IOfferRepository offerRepository = new SQLOfferRepository();
			OfferDto offerDto=null;

			DataSet ds = new DataSet("GetOrders");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_GetOrders", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Userid", SqlDbType.Int);
					command.Parameters.Add("@FromDate", SqlDbType.DateTime);
					command.Parameters.Add("@ToDate", SqlDbType.DateTime);
					command.Parameters.Add("@Status", SqlDbType.Char);
					command.Parameters.Add("@CityCode", SqlDbType.Int);

					//substitute value
					command.Parameters["@Userid"].Value = request.Obj.UserId;
					command.Parameters["@FromDate"].Value = request.Obj.FromDate;
					command.Parameters["@ToDate"].Value = request.Obj.ToDate;
					command.Parameters["@Status"].Value = request.Obj.Type;
					command.Parameters["@CityCode"].Value = request.Obj.CityCode;
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						long grandTotakAfterDiscount = 0;
						Response<OfferDto> offerResponse = offerRepository.GetAppliedOffers(int.Parse(ds.Tables[0].Rows[i]["OrderId"].ToString()));
						if (offerResponse.ObjList != null && offerResponse.ObjList.Count > 0)
						{
							offerDto = offerResponse.ObjList[0];
							grandTotakAfterDiscount = int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString()) - int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString()) * offerDto.PercentOffer / 100;
						}
						else
						{
							offerDto = null;
							grandTotakAfterDiscount = int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString());
						}

						Orders.Add(new OrderDto
						{
							OrderNo = int.Parse(ds.Tables[0].Rows[i]["OrderId"].ToString()),
							OrderDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["OrderDate"].ToString()),
							GrandTotal = int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString()),
							UserId = long.Parse(ds.Tables[0].Rows[i]["Userid"].ToString()),
							PhoneNumber = ds.Tables[0].Rows[i]["UserPhoneNumber"].ToString(),
							DeliveryAddress = ds.Tables[0].Rows[i]["DeliveryAddress"].ToString(),
							UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
							CityCode = int.Parse(ds.Tables[0].Rows[i]["CityCode"].ToString()),
							OrderStatus = ds.Tables[0].Rows[i]["OrderStatus"].ToString(),
							offerDto = offerDto,
							GrandTotalAfterDiscount= grandTotakAfterDiscount
						});
					}
					if (Orders.Count == 0)
					{
						response.Status = false;
						response.ErrMsg = "No Records Found";
					}
					else
					{
						response.Status = true;
						response.ErrMsg = "Total " + Orders.Count.ToString() + " Records Found";
						response.ObjList = Orders;
					}

				}
				catch (Exception ex)
				{
					response.Status = false;
					response.ErrMsg = ex.Message;
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
			return response;
		}

		public Response<OrderDto> GetOrderDetails(Request<OrderDto> request)
		{
			IList<OrderLineItemDto> Items = new List<OrderLineItemDto>();
			DataSet ds = new DataSet("GetOrderDetails");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_GetOrderDetails", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Orderid", SqlDbType.Int);

					//substitute value
					command.Parameters["@Orderid"].Value = request.Obj.OrderNo;
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Items.Add(new OrderLineItemDto
						{
							OrderLineItemId = int.Parse(ds.Tables[0].Rows[i]["OrderLineItemId"].ToString()),
							ItemId = int.Parse(ds.Tables[0].Rows[i]["Itemid"].ToString()),
							ItemHeader = ds.Tables[0].Rows[i]["ItemHeader"].ToString(),
							Quantity = int.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString()),
							PriceType = Convert.ToInt16(ds.Tables[0].Rows[i]["PriceType"].ToString()),
							Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString()),
							ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"].ToString(),
						});
					}
					if (Items.Count == 0)
					{

						response.ErrMsg = "No Records Found";
						response.Status = false;

					}
					else
					{
						response.Obj = new OrderDto()
						{
							OrderLineItemList = Items
						};

						response.Status = true;
						response.ErrMsg = "";

					}

				}
				catch (Exception ex)
				{
					response.Status = false;
					response.ErrMsg = ex.Message;
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
			return response;
		}

		#endregion

		#region private method
		private string XmlParseToString(Request<OrderDto> request)
		{
			XElement xmlElements = new XElement("OrderLineItem",
				request.Obj.OrderLineItemList.Select(i => new XElement("lineItem",
											new XAttribute("Price", i.Price),
											new XAttribute("PriceType", i.PriceType),
											new XAttribute("Quantity", i.Quantity),
											new XAttribute("ItemId", i.ItemId)
											)));
			return xmlElements.ToString();
		}

		public void ApplyOffer(long OrderId, string OfferCode)
		{
			DataSet ds = new DataSet("ApplyOffer");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_ApplyOffer", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@OrderNo", SqlDbType.VarChar);
					command.Parameters.Add("@OfferCode", SqlDbType.VarChar);

					//substitute value
					command.Parameters["@OrderNo"].Value = OrderId;
					command.Parameters["@OfferCode"].Value = OfferCode;
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

				}
				catch (Exception ex)
				{
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}
		#endregion
	}
}