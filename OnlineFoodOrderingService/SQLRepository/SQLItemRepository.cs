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
        Response<ItemDto> itemResponse;

        public SQLItemRepository()
        {
            response = new Response<CategoryDto>();
            itemResponse = new Response<ItemDto>();
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
                    IList<CategoryDto> categoryDtoList = new List<CategoryDto>();

                    foreach (DataRow items in ds.Tables[0].Rows)
                    {
                        var categoriId = Convert.ToInt64(items["Categoryid"]);
                        var itemId = Convert.ToInt64(items["Itemid"]);
                        var CategoryHeader = items["CategoryHeader"].ToString();
                        var CategoryDescription = items["CategoryDescription"].ToString();
                        var ItemHeader = items["ItemHeader"].ToString();
                        var ItemDescription = items["ItemDescription"].ToString();
                        var QuaterPrice = Convert.ToInt64(items["QuaterPrice"]);
                        var HalfPrice = Convert.ToInt64(items["HalfPrice"]);
                        var FullPrice = Convert.ToInt64(items["FullPrice"]);
                        var ImageUrl = items["ImageUrl"].ToString();

                        var CategoryDto = categoryDtoList.Where(p => p.Categoryid == categoriId).FirstOrDefault();
                        if (CategoryDto == null)
                        {
                            CategoryDto = new CategoryDto();
                            CategoryDto.Categoryid = categoriId;
                            CategoryDto.CategoryHeader = CategoryHeader;
                            CategoryDto.CategoryDescription = CategoryDescription;

                            CategoryDto.itemDtoList.Add(new ItemDto()
                            {
                                Itemid=itemId,
                                ItemHeader=ItemHeader,
                                ItemDescription=ItemDescription,
                                QuaterPrice=QuaterPrice,
                                HalfPrice=HalfPrice,
                                FullPrice=FullPrice,
                                ImageUrl=ImageUrl
                            });

                            categoryDtoList.Add(CategoryDto);
                        }
                        else
                        {
                            CategoryDto.itemDtoList.Add(new ItemDto()
                            {
                                Itemid = itemId,
                                ItemHeader = ItemHeader,
                                ItemDescription = ItemDescription,
                                QuaterPrice = QuaterPrice,
                                HalfPrice = HalfPrice,
                                FullPrice = FullPrice,
                                ImageUrl = ImageUrl
                            });
                        }
                    }

                    if (categoryDtoList.Count() > 0)
                    {
                        response.ObjList = categoryDtoList;
                        response.Status = true;
                        response.ErrMsg = "";
                    }
                    else
                    {
                        response.Status = false;
                        response.ErrMsg = "No record found";
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

		public Response<CategoryDto> GetCategory(Request<CategoryDto> request)
		{
			DataSet ds = new DataSet("CategoryListResponse");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_GetCategoryList", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Categoryid", SqlDbType.Int);

					//substitute value
					command.Parameters["@Categoryid"].Value = request.Obj.Categoryid;

					//con.Open();
					SqlDataAdapter da = new SqlDataAdapter();
					da.SelectCommand = command;

					da.Fill(ds);
					IList<CategoryDto> categoryDtoList = new List<CategoryDto>();

					foreach (DataRow items in ds.Tables[0].Rows)
					{
						categoryDtoList.Add(new CategoryDto()
						{
							Categoryid= Convert.ToInt64(items["Categoryid"]),
							CategoryHeader= items["CategoryHeader"].ToString(),
							CategoryDescription= items["CategoryDescription"].ToString()

						});
					}

					if (categoryDtoList.Count() > 0)
					{
						response.ObjList = categoryDtoList;
						response.Status = true;
						response.ErrMsg = "";
					}
					else
					{
						response.Status = false;
						response.ErrMsg = "No record found";
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
                foreach (var item in request.Obj.itemDtoList)
                {
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

        public Response<ItemDto> UpdateItem(Request<ItemDto> request)
        {



            DataSet ds = new DataSet("UpdateItem");
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    //create parameterized query
                    SqlCommand command = new SqlCommand("Usp_UpdateItem", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //register
                    command.Parameters.Add("@Itemid", SqlDbType.Int);
                    command.Parameters.Add("@Categoryid", SqlDbType.Int);
                    command.Parameters.Add("@ItemHeader", SqlDbType.VarChar);
                    command.Parameters.Add("@ItemDescription", SqlDbType.VarChar);
                    command.Parameters.Add("@QuaterPrice", SqlDbType.Int);
                    command.Parameters.Add("@HalfPrice", SqlDbType.Int);
                    command.Parameters.Add("@FullPrice", SqlDbType.Int);
                    command.Parameters.Add("@ImageUrl", SqlDbType.VarChar);
                    command.Parameters.Add("@IsActive", SqlDbType.Bit);



                    //substitute value
                    command.Parameters["@Itemid"].Value = request.Obj.Itemid;
                    command.Parameters["@Categoryid"].Value = request.Obj.Categoryid;
                    command.Parameters["@ItemHeader"].Value = request.Obj.ItemHeader;
                    command.Parameters["@ItemDescription"].Value = request.Obj.ItemDescription;
                    command.Parameters["@QuaterPrice"].Value = request.Obj.QuaterPrice;
                    command.Parameters["@HalfPrice"].Value = request.Obj.HalfPrice;
                    command.Parameters["@FullPrice"].Value = request.Obj.FullPrice;
                    command.Parameters["@ImageUrl"].Value = request.Obj.ImageUrl;
                    command.Parameters["@IsActive"].Value = request.Obj.IsActive;


                    //con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(ds);
                    itemResponse.Status = true;
                    itemResponse.ErrMsg = "Updated successfully";

                }
                catch (Exception ex)
                {
                    itemResponse.Status = false;
                    itemResponse.ErrMsg = ex.Message;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                return itemResponse;
            }


        }
        public Response<CategoryDto> UpdateCategory(Request<CategoryDto> request)
        {
            DataSet ds = new DataSet("CategoryAddResponse");
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    //create parameterized query
                    SqlCommand command = new SqlCommand("Usp_UpdateCategory", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //register
                    command.Parameters.Add("@Categoryid", SqlDbType.Int);
                    command.Parameters.Add("@CategoryHeader", SqlDbType.VarChar);
                    command.Parameters.Add("@CategoryDescription", SqlDbType.VarChar);

                    //substitute value
                    command.Parameters["@Categoryid"].Value = request.Obj.Categoryid;
                    command.Parameters["@CategoryHeader"].Value = request.Obj.CategoryHeader;
                    command.Parameters["@CategoryDescription"].Value = request.Obj.CategoryDescription;

                    //con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(ds);
					response.Status = true;
					response.ErrMsg = "Updated successfully";

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