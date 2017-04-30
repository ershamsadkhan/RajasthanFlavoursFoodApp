using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.Item;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.SQLRepository
{
    public class SQLItemRepository : IItemRepository
    {
        string connection;
        Response<CategoryDto> response;

        public SQLItemRepository()
        {
            response = new Response<CategoryDto>();
            connection = Settings.ConnectionString;
        }

        public Response<CategoryDto> GetItems(Request<CategoryDto> request)
        {
            DataSet ds = new DataSet("ItemListResponse");
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    //create parameterized query
                    SqlCommand command = new SqlCommand("Usp_GetItemsList", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //register
                    command.Parameters.Add("@Categoryid", SqlDbType.Int);
                    command.Parameters.Add("@Itemid", SqlDbType.Int);

                    //substitute value
                    command.Parameters["@Categoryid"].Value = 0;
                    command.Parameters["@Itemid"].Value = 0;

                    //con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(ds);

                    foreach (var items in ds.Tables[0].Rows)
                    {
                        //response.ObjList.
                        response.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);
                        response.ErrMsg = ds.Tables[0].Rows[1]["ErrMsg"].ToString();
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

        public Response<CategoryDto> AddItems(Request<CategoryDto> request)
        {
            DataSet ds = new DataSet("ItemAddResponse");
            using (SqlConnection con = new SqlConnection(connection))
            {
                foreach (var item in request.Obj.itemDtoList) {
                    try
                    {
                        //create parameterized query
                        SqlCommand command = new SqlCommand("Usp_AddItem", con);
                        command.CommandType = CommandType.StoredProcedure;
                        //register
                        command.Parameters.Add("@Categoryid", SqlDbType.Int);
                        command.Parameters.Add("@ItemHeader", SqlDbType.VarChar);
                        command.Parameters.Add("@ItemDescription", SqlDbType.VarChar);
                        command.Parameters.Add("@QuaterPrice", SqlDbType.Int);
                        command.Parameters.Add("@HalfPrice", SqlDbType.Int);
                        command.Parameters.Add("@FullPrice", SqlDbType.Int);
                        command.Parameters.Add("@ImageUrl", SqlDbType.VarChar);

                        //substitute value
                        command.Parameters["@Categoryid"].Value = request.Obj.Categoryid;
                        command.Parameters["@ItemHeader"].Value = item.ItemHeader;
                        command.Parameters["@ItemDescription"].Value = item.ItemDescription;
                        command.Parameters["@QuaterPrice"].Value = item.QuaterPrice;
                        command.Parameters["@HalfPrice"].Value = item.HalfPrice;
                        command.Parameters["@FullPrice"].Value = item.FullPrice;
                        command.Parameters["@ImageUrl"].Value = item.ImageUrl;
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
            }
            return response;
        }

        public Response<CategoryDto> AddCategory(Request<CategoryDto> request)
        {
            DataSet ds = new DataSet("CategoryAddResponse");
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    //create parameterized query
                    SqlCommand command = new SqlCommand("Usp_AddCategory", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //register
                    command.Parameters.Add("@CategoryHeader", SqlDbType.VarChar);
                    command.Parameters.Add("@CategoryDescription", SqlDbType.VarChar);

                    //substitute value
                    command.Parameters["@CategoryHeader"].Value = request.Obj.CategoryHeader;
                    command.Parameters["@CategoryDescription"].Value = request.Obj.CategoryDescription;

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
    }
}