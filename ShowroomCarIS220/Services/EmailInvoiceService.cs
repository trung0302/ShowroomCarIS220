using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Data;
using Microsoft.EntityFrameworkCore;

namespace ShowroomCarIS220.Services
{
    public class EmailInvoiceService : IEmailInvoiceService
    {
        private readonly DataContext _db;
        public EmailInvoiceService(DataContext db)
        {
            _db = db;
        }
        //Gửi hóa đơn đã thanh toán
        public async void SendInvoiceEmail(string emailTo, HoaDon hd, List<CTHD> cthds)
        {
            var valueInvoiceString = "";
            foreach (var item in cthds)
            {
                var car =  _db.Car.FirstOrDefault(i => i.macar == item.macar);
                valueInvoiceString = valueInvoiceString + $"<tr style=\"text-align:center;\">" +
                    $"<td style=\" border: 1px solid #ddd;\" >{item.macar}</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{car.ten}</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{string.Format("{0:n0}", car.gia)} VNĐ</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{item.soluong}</td>" +
                    $"</tr>";
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("kingspeedmail2@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "KingSpeed: Hóa đơn";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h2>King Speed xin kính chào quý khách!</h2>" +
                $"<h3>Cảm ơn quý khách đã mua hàng tại King Speed!</h3>" +
                $"<h3>King Speed thông báo hóa đơn của quý khách như sau: </h3>" +
                $"<table style=\"margin-left: 30px;\">" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã đơn:</th>" +
                $"<td>{hd.mahd}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã khách hàng:</th>" +
                $"<td>{hd.makh}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Tên khách hàng:</th>" +
                $"<td>{hd.tenkh}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã nhân viên:</th>" +
                $"<td>{hd.manv}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Ngày đặt hàng:</th>" +
                $"<td>{hd.ngayhd}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Tình trạng:</th>" +
                $"<td>{hd.tinhtrang}</td>" +
                $"</tr>" +
                $"</table>" +
                $"<table style=\"border: 1px solid #ddd; border-collapse: collapse; margin-left: 30px;\">" +
                $"<thead style=\" background-color: #04AA6D; color: white;\" >" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"100px\">Mã xe</th>" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"200px\">Tên xe</th>" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"200px\">Giá</th>" +
                $"<th style=\" border: 1px solid #ddd;\"width=\"100px\">Số lượng</th>" +
                $"</thead>" +
                $"<tbody  style=\"background-color: #f2f2f2;\">{valueInvoiceString}</tbody>" +
                $"</table>" +
                $"<h4 style=\"color: #0b3c86;\">Tổng tiền: {string.Format("{0:n0}", hd.trigia)} VNĐ</h4>" +
                $"<h4 style=\"color: #035e21;\">Vui lòng liên hệ Hotline 0943415138 nếu quý khách có thắc mắc về hóa đơn.</h4>" +
                $"<h3>Cảm ơn quý khách đã tin tưởng và sử dụng dịch vụ của King Speed!</h3>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("kingspeedmail2@gmail.com", "vogavwrmmldzftvp");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        //Gửi hóa đơn chưa thanh toán
        public async void SendOrderEmail(string emailTo, HoaDon hd, List<CTHD> cthds)
        {
            var valueInvoiceString = "";
            foreach (var item in cthds)
            {
                var car = _db.Car.FirstOrDefault(i => i.macar == item.macar);
                valueInvoiceString = valueInvoiceString + $"<tr style=\"text-align:center;\">" +
                    $"<td style=\" border: 1px solid #ddd;\" >{item.macar}</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{car?.ten}</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{string.Format("{0:n0}", car?.gia)} VNĐ</td>" +
                    $"<td style=\" border: 1px solid #ddd;\" >{item.soluong}</td>" +
                    $"</tr>";
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("kingspeedmail2@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "KingSpeed: Đơn đặt hàng";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h2>King Speed xin kính chào quý khách!</h2>" +
                $"<h3>Cảm ơn quý khách đã mua hàng tại King Speed!</h3>" +
                $"<h3>King Speed thông báo đơn đặt hàng của quý khách như sau: </h3>" +
                $"<table style=\"margin-left: 30px;\">" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã đơn:</th>" +
                $"<td>{hd.mahd}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã khách hàng:</th>" +
                $"<td>{hd.makh}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Tên khách hàng:</th>" +
                $"<td>{hd.tenkh}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Mã nhân viên:</th>" +
                $"<td>{hd.manv}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Ngày đặt hàng:</th>" +
                $"<td>{hd.ngayhd}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th style=\"text-align:left\">Tình trạng:</th>" +
                $"<td>{hd.tinhtrang}</td>" +
                $"</tr>" +
                $"</table>" +
                $"<table style=\"border: 1px solid #ddd; border-collapse: collapse; margin-left: 30px;\">" +
                $"<thead style=\" background-color: #04AA6D; color: white;\" >" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"100px\">Mã xe</th>" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"200px\">Tên xe</th>" +
                $"<th style=\" border: 1px solid #ddd;\" width=\"200px\">Giá</th>" +
                $"<th style=\" border: 1px solid #ddd;\"width=\"100px\">Số lượng</th>" +
                $"</thead>" +
                $"<tbody  style=\"background-color: #f2f2f2;\">{valueInvoiceString}</tbody>" +
                $"</table>" +
                $"<h4 style=\"color: #0b3c86;\">Tổng tiền: {string.Format("{0:n0}", hd.trigia)} VNĐ</h4>" +
                $"<p style=\"color: red;\">Quý khách vui thanh toán đơn đặt hàng trong vòng 15 ngày kể từ ngày đặt hàng.</p>" +
                $"<h3>Cảm ơn quý khách đã tin tưởng và sử dụng dịch vụ của King Speed!</h3>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("kingspeedmail2@gmail.com", "vogavwrmmldzftvp");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
