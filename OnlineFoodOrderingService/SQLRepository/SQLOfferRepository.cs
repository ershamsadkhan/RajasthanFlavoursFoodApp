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
        double DateDifference;

        public SQLOfferRepository()
        {
            response = new Response<OfferDto>();
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
        //public Response<OrderDto> ApplicableOffer(Request<OrderDto> request)
        //{
   
        //    DataSet ds = new DataSet("ApplicableOffers");
        //    using (SqlConnection con = new SqlConnection(connection))
        //    {
        //        try
        //        {
        //            //create parameterized query
        //            SqlCommand command = new SqlCommand("Usp_ApplicableOffer4000", con);
        //            command.CommandType = CommandType.StoredProcedure;
        //            //register
        //            command.Parameters.Add("@Userid", SqlDbType.Int);
        //            //substitute value

        //            command.Parameters["@Userid"].Value = request.Obj.UserId;
                   
        //            //con.Open();
        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = command;

        //            da.Fill(ds);

        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                Orders.Add(new OrderDto
        //                {
        //                    OrderNo = int.Parse(ds.Tables[0].Rows[i]["OrderId"].ToString()),
        //                    OrderDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["OrderDate"].ToString()),
        //                    GrandTotal = int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString()),
        //                    UserId = long.Parse(ds.Tables[0].Rows[i]["Userid"].ToString()),
        //                    DeliveryAddress = ds.Tables[0].Rows[i]["DeliveryAddress"].ToString(),
        //                    UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
        //                    CityCode = int.Parse(ds.Tables[0].Rows[i]["CityCode"].ToString())
        //                });
        //            }
        //            if (Orders.Count == 0)
        //            {

        //                response.ErrMsg = "No Records Found";
        //            }
        //            else
        //            {
        //                response.ErrMsg = "Total " + Orders.Count.ToString() + " Records Found";
        //                response.ObjList = Orders;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            response.Status = false;
        //            response.ErrMsg = ex.Message;
        //        }
        //        finally
        //        {
        //            if (con.State == ConnectionState.Open)
        //            {
        //                con.Close();
        //            }
        //        }
        //    }
        //    return response;
        //}

    }

    }
