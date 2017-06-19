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
        double DateDifference;

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
                if (ds.Tables.Count>0)
                {
                    DateTime LastOfferDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["OfferDate"]);
                    DateDifference = (DateTime.Now - LastOfferDate).TotalDays;

                }


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


        public Response<OrderDto> ApplicableOffers(Request<OrderDto> request)
        {

            {
                
                    List<OfferDto> Offerlist = new List<OfferDto>();
                    int status2000 = OfferAppliedInLast30Days(request, "C2000");
                    int status4000 = OfferAppliedInLast30Days(request, "C4000");
                    int Total = GrandTotal(request);
                    if (Total > 2000 && Total < 4000 && status2000 == 1)
                    {
                        Offerlist.Add(new OfferDto
                        {
                            OfferCode = "C2000",
                            OfferDescription = "C2000",
                            OfferHeader = "offer on more then 2000"

                        });
                    }
                    else if (Total > 4000 && status4000 == 1)
                    {
                        Offerlist.Add(new OfferDto
                        {
                            OfferCode = "C4000",
                            OfferDescription = "C4000",
                            OfferHeader = "offer on more then 4000"

                        });
                    }
                    OrderResponse.Obj.OfferList = Offerlist;
                
                return OrderResponse;
            }
        }

        //private Methods
        #region
        private int OfferAppliedInLast30Days(Request<OrderDto> request, string OfferCode)
        {
            int status=0;
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
                    command.Parameters.Add("@OfferCode", SqlDbType.Int);
                    //substitute value

                    command.Parameters["@Userid"].Value = request.Obj.UserId;
                    command.Parameters["@OfferCode"].Value = OfferCode;
                    //con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                        status = 1;             
                    else
                      status = 0;

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
            return status;
        }
        private int GrandTotal(Request<OrderDto> request)
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

    }

    }
            #endregion