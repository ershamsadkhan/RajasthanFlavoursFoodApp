using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using OnlineFoodOrderingService.IRepository;
using OnlineFoodOrderingService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Globalization;

namespace OnlineFoodOrderingService.SQLRepository
{
    public class SQLUserRepository : IUserRepository
    {
        string connection;
        Response<UserDto> response;

        public SQLUserRepository()
        {
            response = new Response<UserDto>();
            connection = Settings.ConnectionString;
        }

        public Response<UserDto> SignUp(Request<UserDto> request)
        {



            DataSet ds = new DataSet("SignUpResponse");
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    //create parameterized query
                    SqlCommand command = new SqlCommand("Usp_UserSignUp", con);
                    command.CommandType = CommandType.StoredProcedure;
                    //register
                    command.Parameters.Add("@Userid", SqlDbType.Int);
                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters.Add("@UserPwd", SqlDbType.VarChar);
                    command.Parameters.Add("@PrimaryAddress", SqlDbType.VarChar);
					command.Parameters.Add("@UserPhoneNumber", SqlDbType.VarChar);
					command.Parameters.Add("@UserEmailAddress", SqlDbType.VarChar);
					command.Parameters.Add("@IsAdmin", SqlDbType.Bit);

                    //substitute value
                    command.Parameters["@Userid"].Value = request.Obj.Userid;
                    command.Parameters["@UserName"].Value = request.Obj.UserName;
                    command.Parameters["@UserPwd"].Value = request.Obj.UserPwd;
                    command.Parameters["@PrimaryAddress"].Value = request.Obj.PrimaryAddress;
					command.Parameters["@UserPhoneNumber"].Value = request.Obj.UserPhoneNumber;
					command.Parameters["@UserEmailAddress"].Value = request.Obj.UserEmailAddress;
					command.Parameters["@IsAdmin"].Value = request.Obj.IsAdmin;

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

        public Response<UserDto> GetUserDetails(string UserId)
        {
            
            IList<UserDto> UserList = new List<UserDto>();
            DataSet ds = new DataSet("UserDetails");
            SqlConnection con = new SqlConnection(connection);

            SqlCommand command = new SqlCommand("select UserName,PrimaryAddress,UserPhoneNumber,UserEmailAddress,UserPwd from Users where Userid=@Userid", con);
            try
            {
                command.Parameters.Add("@Userid", SqlDbType.VarChar);
                command.Parameters["@Userid"].Value = UserId;
              
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;

                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UserList.Add(new UserDto
                    {
                        UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
                        PrimaryAddress = ds.Tables[0].Rows[i]["PrimaryAddress"].ToString(),
						UserPhoneNumber = ds.Tables[0].Rows[i]["UserPhoneNumber"].ToString(),
						UserEmailAddress = ds.Tables[0].Rows[i]["UserEmailAddress"].ToString(),
						UserPwd= ds.Tables[0].Rows[i]["UserPwd"].ToString()
					});

                }
                if (UserList.Count > 0)
                {
                    response.Status = true;
                    response.ErrMsg = "";
                    response.ObjList = UserList;
                }
                else
                {
                    response.Status = false;
                    response.ErrMsg = "User does not exist";
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

		public Response<UserDto> GetUsersList(Request<UserDto> request)
		{

			IList<UserDto> UserList = new List<UserDto>();
			DataSet ds = new DataSet("UsersList");
			SqlConnection con = new SqlConnection(connection);
			CultureInfo provider = CultureInfo.InvariantCulture;

			SqlCommand command = new SqlCommand("select UserName,PrimaryAddress,UserPhoneNumber,UserEmailAddress,ISNULL(cast(RegisterDate AS date),GETDATE()) AS  RegisterDate from Users where UserName like '%"+request.Obj.UserName +"%'", con);
			try
			{
				//command.Parameters.Add("@Userid", SqlDbType.VarChar);
				//command.Parameters["@Userid"].Value = UserId;

				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = command;

				da.Fill(ds);

				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					UserList.Add(new UserDto
					{
						UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
						PrimaryAddress = ds.Tables[0].Rows[i]["PrimaryAddress"].ToString(),
						UserPhoneNumber = ds.Tables[0].Rows[i]["UserPhoneNumber"].ToString(),
						UserEmailAddress = ds.Tables[0].Rows[i]["UserEmailAddress"].ToString(),
						RegisterDate = ds.Tables[0].Rows[i]["RegisterDate"].ToString()
					});

				}
				if (UserList.Count > 0)
				{
					response.Status = true;
					response.ErrMsg = "";
					response.ObjList = UserList;
				}
				else
				{
					response.Status = false;
					response.ErrMsg = "No records found.";
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

		public Response<UserDto> GetUserDetailsFromUserName(Request<UserDto> request)
		{

			IList<UserDto> UserList = new List<UserDto>();
			DataSet ds = new DataSet("UserDetails");
			SqlConnection con = new SqlConnection(connection);

			SqlCommand command = new SqlCommand("select UserName,PrimaryAddress from Users where UserName=@UserName", con);
			try
			{
				command.Parameters.Add("@UserName", SqlDbType.VarChar);
				command.Parameters["@UserName"].Value = request.Obj.UserName;

				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = command;

				da.Fill(ds);

				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					UserList.Add(new UserDto
					{
						UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
						PrimaryAddress = ds.Tables[0].Rows[i]["PrimaryAddress"].ToString()
					});

				}
				if (UserList.Count > 0)
				{
					response.Status = true;
					response.ErrMsg = "";
					response.ObjList = UserList;
				}
				else
				{
					response.Status = false;
					response.ErrMsg = "User does not exist";
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

		public Response<UserDto> GetLogInDetails(Request<UserDto> request)
        {

            IList<UserDto> UserList = new List<UserDto>();
            DataSet ds = new DataSet("LoginDetails");
            SqlConnection con = new SqlConnection(connection);

            SqlCommand command = new SqlCommand("select UserName,PrimaryAddress,UserPwd,Userid,UserPhoneNumber,UserEmailAddress from Users where UserName=@UserName and UserPwd=@UserPwd", con);
            try
            {
                command.Parameters.Add("@UserName", SqlDbType.VarChar);
                command.Parameters.Add("@UserPwd", SqlDbType.VarChar);
                command.Parameters["@UserName"].Value = request.Obj.UserName;
                command.Parameters["@UserPwd"].Value = request.Obj.UserPwd;


                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;

                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UserList.Add(new UserDto
                    {
                        UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
                        PrimaryAddress = ds.Tables[0].Rows[i]["PrimaryAddress"].ToString(),
                        UserPwd = ds.Tables[0].Rows[i]["UserPwd"].ToString(),
                        Userid = Convert.ToInt64(ds.Tables[0].Rows[i]["Userid"]),
						UserPhoneNumber = ds.Tables[0].Rows[i]["UserPhoneNumber"].ToString(),
						UserEmailAddress = ds.Tables[0].Rows[i]["UserEmailAddress"].ToString()
					});

                }
                if (UserList.Count > 0)
                {

					response.Status = true;
					response.ErrMsg = "SuccessFull";
                    response.ObjList = UserList;
                }
                else
                {
                    response.Status = false;
                    response.ErrMsg = "Invalid Username And Password.";
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

		public Response<UserDto> UpdateProfile(Request<UserDto> request)
		{



			DataSet ds = new DataSet("UpdateResponse");
			using (SqlConnection con = new SqlConnection(connection))
			{
				try
				{
					//create parameterized query
					SqlCommand command = new SqlCommand("Usp_UpdateProfile", con);
					command.CommandType = CommandType.StoredProcedure;
					//register
					command.Parameters.Add("@Userid", SqlDbType.Int);
					command.Parameters.Add("@UserName", SqlDbType.VarChar);
					command.Parameters.Add("@Email", SqlDbType.VarChar);
					command.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
					command.Parameters.Add("@PrimaryAddress", SqlDbType.VarChar);

					//substitute value
					command.Parameters["@Userid"].Value = request.Obj.Userid;
					command.Parameters["@UserName"].Value = request.Obj.UserName;
					command.Parameters["@Email"].Value = request.Obj.UserEmailAddress;
					command.Parameters["@PhoneNo"].Value = request.Obj.UserPhoneNumber;
					command.Parameters["@PrimaryAddress"].Value = request.Obj.PrimaryAddress;


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
				return response;
			}


		}

		public Response<UserDto> GetForgotPasswordDetails(string UserName)
		{

			IList<UserDto> UserList = new List<UserDto>();
			DataSet ds = new DataSet("ForgotPasswordDetails");
			SqlConnection con = new SqlConnection(connection);

			SqlCommand command = new SqlCommand("select UserName,PrimaryAddress,UserPhoneNumber,UserEmailAddress,UserPwd from Users where UserName=@UserName", con);
			try
			{
				command.Parameters.Add("@UserName", SqlDbType.VarChar);
				command.Parameters["@UserName"].Value = UserName;

				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = command;

				da.Fill(ds);

				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					UserList.Add(new UserDto
					{
						UserName = ds.Tables[0].Rows[i]["UserName"].ToString(),
						PrimaryAddress = ds.Tables[0].Rows[i]["PrimaryAddress"].ToString(),
						UserPhoneNumber = ds.Tables[0].Rows[i]["UserPhoneNumber"].ToString(),
						UserEmailAddress = ds.Tables[0].Rows[i]["UserEmailAddress"].ToString(),
						UserPwd = ds.Tables[0].Rows[i]["UserPwd"].ToString()
					});

				}
				if (UserList.Count > 0)
				{
					response.Status = true;
					response.ErrMsg = "";
					response.ObjList = UserList;
				}
				else
				{
					response.Status = false;
					response.ErrMsg = "User does not exist";
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
}