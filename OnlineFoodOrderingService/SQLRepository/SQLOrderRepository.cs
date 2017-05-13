﻿using OnlineFoodOrderingService.IRepository;
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

                    //substitute value
                    command.Parameters["@lineItemXml"].Value = lineItemXmlString;
                    command.Parameters["@UserId"].Value = request.Obj.UserId;
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
                    command.Parameters["@OrderStatus"].Value =  orderStatus.GetDescription();
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

                    //substitute value
                    command.Parameters["@Userid"].Value = request.Obj.UserId;
                    command.Parameters["@FromDate"].Value = request.Obj.FromDate ;
                    command.Parameters["@ToDate"].Value = request.Obj.ToDate;
                    //con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Orders.Add(new OrderDto
                        {
                            OrderNo = int.Parse(ds.Tables[0].Rows[i]["Orderid"].ToString()),
                            OrderDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["OrderDate"].ToString()),
                            GrandTotal = int.Parse(ds.Tables[0].Rows[i]["GrandTotal"].ToString())
                        });
                    }
                    if (Orders.Count == 0)
                    {

                        response.ErrMsg = "No Records Found";
                    }
                    else
                    {
                        response.ErrMsg = "Total "+Orders.Count.ToString()+" Records Found";
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
                         OrderLineItemId= int.Parse(ds.Tables[0].Rows[i]["OrderLineItemId"].ToString()),
                         ItemId = int.Parse(ds.Tables[0].Rows[i]["Itemid"].ToString()),
                         ItemHeader =ds.Tables[0].Rows[i]["ItemHeader"].ToString(),
                         Quantity = int.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString()),
                         PriceType=Convert.ToInt16(ds.Tables[0].Rows[i]["PriceType"].ToString()),
                         Price=int.Parse(ds.Tables[0].Rows[i]["Price"].ToString())
                        });
                    }
                    if (Items.Count == 0)
                    {

                        response.ErrMsg = "No Records Found";
                    }
                    else
                    {
                        response.Obj.OrderLineItemList=Items;
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
        #endregion
    }
}