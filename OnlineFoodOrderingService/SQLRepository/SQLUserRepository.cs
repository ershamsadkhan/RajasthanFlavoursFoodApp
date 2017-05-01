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
                    command.Parameters.Add("@IsAdmin", SqlDbType.Bit);

                    //substitute value
                    command.Parameters["@Userid"].Value = request.Obj.Userid;
                    command.Parameters["@UserName"].Value = request.Obj.UserName;
                    command.Parameters["@UserPwd"].Value = request.Obj.UserPwd;
                    command.Parameters["@PrimaryAddress"].Value = request.Obj.PrimaryAddress;
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

        public Response<UserDto> GetUserDetails(Request<UserDto> request)
        {
            
            IList<UserDto> UserList = new List<UserDto>();
            DataSet ds = new DataSet("UserDetails");
            SqlConnection con = new SqlConnection(connection);

            SqlCommand command = new SqlCommand("select UserName,PrimaryAddress from dbo.Users where UserName=@UserName", con);
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
    }
}