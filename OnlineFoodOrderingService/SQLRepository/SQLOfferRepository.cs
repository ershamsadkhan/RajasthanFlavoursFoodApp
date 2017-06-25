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
	public class SQLOfferRepository : IOfferRepository
	{
		string connection;
		Response<OfferDto> response;
		Response<OrderDto> OrderResponse;

		public SQLOfferRepository()
		{
			response = new Response<OfferDto>();
			OrderResponse = new Response<OrderDto>();
			connection = Settings.ConnectionString;

		}

		public Response<OfferDto> GetOffers(Request<OfferDto> request)
		{
			IList<OfferDto> Offers = new List<OfferDto>();
			DataSet ds = new DataSet("GetOffers");
			SqlConnection con = new SqlConnection(connection);

			SqlCommand command = new SqlCommand("Select OfferHeader ,OfferDescription, OfferCode, PercentOffer, RsOffer from Offer where IsActive=1", con);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = command;

				da.Fill(ds);
				//if (ds.Tables.Count>0)
				//{
				//    DateTime LastOfferDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["OfferDate"]);
				//    DateDifference = (DateTime.Now - LastOfferDate).TotalDays;

				//}


				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					Offers.Add(new OfferDto
					{
						OfferHeader = ds.Tables[0].Rows[i]["OfferHeader"].ToString(),
						OfferDescription = ds.Tables[0].Rows[i]["OfferDescription"].ToString(),
						OfferCode = ds.Tables[0].Rows[i]["OfferCode"].ToString(),
						PercentOffer = int.Parse(ds.Tables[0].Rows[i]["PercentOffer"].ToString()),
						RsOffer = int.Parse(ds.Tables[0].Rows[i]["RsOffer"].ToString())

					});
				}
				if (Offers.Count == 0)
				{

					response.ErrMsg = "There are no Offers available currently";
					response.Status = false;

				}
				else
				{
					response.ObjList = Offers;
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
			return response;
		}

		public Response<OfferDto> GetAppliedOffers(long OrderNo)
		{
			IList<OfferDto> offerDtoList = new List<OfferDto>();

			DataSet ds = new DataSet("AppliedOffers");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_GetAppliedOffers", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@OrderNo", SqlDbType.Int);

					//substitute value

					command.Parameters["@OrderNo"].Value = OrderNo;

					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					if (ds.Tables.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							offerDtoList.Add(new OfferDto
							{
								OfferHeader = ds.Tables[0].Rows[i]["OfferHeader"].ToString(),
								OfferDescription = ds.Tables[0].Rows[i]["OfferDescription"].ToString(),
								OfferCode = ds.Tables[0].Rows[i]["OfferCode"].ToString(),
								PercentOffer = int.Parse(ds.Tables[0].Rows[i]["PercentOffer"].ToString()),
								RsOffer = int.Parse(ds.Tables[0].Rows[i]["RsOffer"].ToString())

							});
						}
					}
					if (offerDtoList.Count == 0)
					{

						response.ErrMsg = "No Applied Offers";
						response.Status = false;

					}
					else
					{
						response.ObjList = offerDtoList;
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
				return response;
			}
		}

		public Response<OfferDto> ApplicableOffers(Request<OfferDto> request)
		{

			List<OfferDto> Offerlist = new List<OfferDto>();
			OfferDto c20OfferDto; OfferDto c40OfferDto;

			c20OfferDto = OfferAppliedInLast30Days(request, "C200");
			c40OfferDto = OfferAppliedInLast30Days(request, "C400");

			int Total = GrandTotal(request);

			if (Total > 2000 && Total < 4000 && c20OfferDto != null)
			{
				Offerlist.Add(c20OfferDto);
			}
			else if (Total > 4000 && c40OfferDto != null)
			{
				Offerlist.Add(c40OfferDto);
			}
			response.ObjList = Offerlist;

			return response;

		}


		#region private Methods
		private OfferDto OfferAppliedInLast30Days(Request<OfferDto> request, string OfferCode)
		{
			OfferDto offerDto = null;
			DataSet ds = new DataSet("OfferAppliedInLast30Days");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_OfferAppliedInLast30Days", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Userid", SqlDbType.Int);
					command.Parameters.Add("@OfferCode", SqlDbType.VarChar);
					//substitute value

					command.Parameters["@Userid"].Value = request.Obj.UserId;
					command.Parameters["@OfferCode"].Value = OfferCode;
					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);
					if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						offerDto = new OfferDto();
						offerDto.OfferCode = ds.Tables[0].Rows[0]["OfferCode"].ToString();
						offerDto.OfferHeader = ds.Tables[0].Rows[0]["OfferHeader"].ToString();
						offerDto.OfferDescription = ds.Tables[0].Rows[0]["OfferDescription"].ToString();
						offerDto.PercentOffer = Int32.Parse(ds.Tables[0].Rows[0]["PercentOffer"].ToString());
						offerDto.RsOffer = Int32.Parse(ds.Tables[0].Rows[0]["RsOffer"].ToString());
					}

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
			return offerDto;
		}
		private int GrandTotal(Request<OfferDto> request)
		{
			int GrandTotal = 0;
			DataSet ds = new DataSet("GrandTotal");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_GrandTotal", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Userid", SqlDbType.Int);

					//substitute value

					command.Parameters["@Userid"].Value = request.Obj.UserId;

					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);

					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{

						GrandTotal = GrandTotal + int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString());
					}

				}

				catch (Exception ex)
				{
					return 0;
				}
				finally
				{
					if (con.State == ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
			return GrandTotal;
		}
		#endregion

	}

}
