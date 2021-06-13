using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using System.Text;

namespace TODO.Helpers
{
    public class ResetPasswordHelper : IResetPasswordHelper
    {
        private IConfiguration m_config;

        public ResetPasswordHelper(IConfiguration config)
        {
            m_config = config;
        }
        public bool SendEmail(string email)
        {
            ///Should have used built in User model...
            ///Then use UserManager to GeneratePasswordResetToken
            if (!IsValidEmail(email))
            {
                return false;
            }
            GenerateAndSendEmail(email);
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void GenerateAndSendEmail(string email)
        {
            string to = email;
            string from = m_config[Constants.Email];
            MailMessage message = new MailMessage(from, to);

            message.Subject = Constants.Subject;
            message.Body = Constants.MailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(Constants.Smtp, Constants.SmtpPort);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(m_config[Constants.Email], m_config[Constants.PassWord]);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
