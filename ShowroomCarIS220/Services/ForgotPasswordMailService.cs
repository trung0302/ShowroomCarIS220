using System.Net.Mail;
using System.Net;

namespace ShowroomCarIS220.Services
{
    public class ForgotPasswordMailService
    {
        public bool SendEmail(string emailReceiver, string token)
        {
            try
            {
                string fromMail = "kingspeedmail2@gmail.com";
                string fromPassword = "vogavwrmmldzftvp";
                string urlClient = "http://localhost:3000/resetpass";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "King Speed: Quên mật khẩu";
                message.To.Add(new MailAddress($"{emailReceiver}"));
                message.Body = "<h2>King Speed xin kính chào quý khách!</h2>" +
                    "<p>Để đổi lại mật khẩu cho tài khoản của quý khách. Vùi lòng nhấn vào nút bên dưới để thực hiện đổi mật khẩu!</p>" +
                    $"<a href=\"{urlClient}/{token}\" style=\"display:block; width: max-content; padding: 8px;  color: rgb(255, 255, 255); background: #078989; border-radius: 8px;margin-left: 150px;text-decoration: none;\">Đặt lại mật khẩu</a>" +
                    "<h6 style=\"margin-left: 100px ; color: red;\">Email này chỉ có hiệu lực trong vòng 20 phút!</h6>" +
                    "<p>Nếu quý khách không thực hiện yêu cầu này! Xin quý khách vui lòng bỏ qua email này.</p>" +
                    "<h3>Cảm ơn quý khách đã tin tưởng và sử dụng dịch vụ của King Speed!</h3>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
                return true;

            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}
