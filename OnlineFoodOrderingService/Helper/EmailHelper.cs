using OnlineFoodOrderingService.DTO;
using OnlineFoodOrderingService.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Temboo.Core;
using Temboo.Library.Google.Gmail;
namespace Common.Helpers
{

	public class EMailHelper
	{
		Response<UserDto> response = new Response<UserDto>();
		// Note: To send email you need to add actual email id and credential so that it will work as expected  
		public static readonly string EMAIL_SENDER = "flavofrajasthan@gmail.com"; // change it to actual sender email id or get it from UI input  
		public static readonly string EMAIL_CREDENTIALS = "Shammyk@123456"; // Provide credentials   
		public static readonly string SMTP_CLIENT = "relay-hosting.secureserver.net"; // as we are using outlook so we have provided smtp-mail.outlook.com 
		public static readonly Int32 SMTP_PORT = 567;

		public string RECIPIENT = "";
		public string SUBJECT = "";
		public string MESSAGE = "";

		public EMailHelper()
		{
		}
		public Response<UserDto> SendEMail(string recipient, string subject, string message)
		{

			try
			{
				//MailAddress to = new MailAddress(recipient);
				//MailAddress from = new MailAddress(EMAIL_SENDER);
				//MailMessage mail = new MailMessage(from, to);

				//mail.Subject = subject;
				//mail.Body = message;

				//SmtpClient smtp = new SmtpClient();
				//smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				//smtp.Host = SMTP_CLIENT;
				//smtp.Port = SMTP_PORT;
				//smtp.UseDefaultCredentials = false;
				//smtp.Credentials = new NetworkCredential(
				//	EMAIL_SENDER, EMAIL_CREDENTIALS);

				//smtp.EnableSsl = true;
				//smtp.Send(mail);
				TembooSession session = new TembooSession("flavoursofrajasthan", "myFirstApp", "d0XPIC2zUt49k2WAEYEFAQxYcIsW94VB");
				SendEmail sendEmailChoreo = new SendEmail(session);

				// Set inputs
				sendEmailChoreo.setFromAddress("flavofrajasthan@gmail.com");
				sendEmailChoreo.setUsername("flavofrajasthan@gmail.com");
				sendEmailChoreo.setSubject(subject);
				sendEmailChoreo.setToAddress(recipient);
				sendEmailChoreo.setMessageBody(message);
				sendEmailChoreo.setPassword("wssuuusiysusppny");

				// Execute Choreo
				SendEmailResultSet sendEmailResults = sendEmailChoreo.execute();

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