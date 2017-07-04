using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace Common.Helpers
{

	public class EMailHelper
	{
		Response<UserDto> response=new Response<UserDto>();
		// Note: To send email you need to add actual email id and credential so that it will work as expected  
		public static readonly string EMAIL_SENDER = "shammyk123@gmail.com"; // change it to actual sender email id or get it from UI input  
		public static readonly string EMAIL_CREDENTIALS = "zoyarand"; // Provide credentials   
		public static readonly string SMTP_CLIENT = "smtp.gmail.com"; // as we are using outlook so we have provided smtp-mail.outlook.com   
		public static readonly string EMAIL_BODY = "Reset your Password ";
		public static readonly string EMAIL_SUBJECT = "Reset your Password ";
		private string senderAddress;
		private string clientAddress;
		private string netPassword;
		public EMailHelper(string sender, string Password, string client)
		{
			senderAddress = sender;
			netPassword = Password;
			clientAddress = client;
		}
		public Response<UserDto> SendEMail(string recipient, string subject, string message)
		{
			//Intialise Parameters  
			//System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(clientAddress);
			//client.Port = 587;
			//client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
			//client.UseDefaultCredentials = false;
			//System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(senderAddress, netPassword);
			//client.EnableSsl = true;
			//client.Credentials = credentials;
			//try
			//{
			//	var mail = new System.Net.Mail.MailMessage(senderAddress.Trim(), recipient.Trim());
			//	mail.Subject = subject;
			//	mail.Body = message;
			//	//System.Net.Mail.Attachment attachment;  
			//	//attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");  
			//	//mail.Attachments.Add(attachment);  
			//	client.Send(mail);

			//	response.Status = true;
			//}
			//catch (Exception ex)
			//{
			//	response.Status = false;
			//	response.ErrMsg = ex.InnerException.ToString();
			//}
			try
			{
				MailMessage mail = new MailMessage();
				mail.To.Add("shammyk123@gmail.com");
				mail.From = new MailAddress("ershamsadkhan@gmail.com");
				mail.Subject ="hello";
				mail.Body = "Hello";
				mail.IsBodyHtml = true;

				var client = new SmtpClient("smtp.gmail.com", 587)
				{
					UseDefaultCredentials = false,
				EnableSsl = false
				};
				client.Credentials = new System.Net.NetworkCredential
				("ershamsadkhan@gmail.com", "Shammyk@123567");

				client.Send(mail);
				response.Status = true;
			}
			catch (Exception ex)
			{
				response.Status = false;
				response.ErrMsg = ex.InnerException.ToString();
			}
			return response;
		}
	}
}