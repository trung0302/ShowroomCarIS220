using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MimeKit;
using MimeKit.Text;
using ShowroomCarIS220.Auth;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using ShowroomCarIS220.Services;
using System.Security.Cryptography;

namespace ShowroomCarIS220.Controllers
{
    [Route("hoadons")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IEmailInvoiceService _email;
        private Authentication _auth = new Authentication();

        public HoaDonController(DataContext db, IEmailInvoiceService email)
        {
            _db = db;
            _email = email;
        }

        //Get Hoa Don
        [HttpGet]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult<InvoiceResponse<List<GetInvoice>>>> getHoaDon([FromQuery] int? pageIndex, [FromQuery] int? pageSize, [FromHeader] string Authorization)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 10;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
            if (checkToken == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
            }

            var invoiceResponse = new InvoiceResponse<List<GetInvoice>>();
            var listInvoice = new List<GetInvoice>();
            try
            {
                if (pageIndex != null)
                {
                    var hoadons = await _db.HoaDon
                        .OrderBy(hd => hd.mahd)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();

                    foreach (var item in hoadons)
                    {
                        var getInvoice = new GetInvoice
                        {
                            id = item.id,
                            mahd = item.mahd,
                            manv = item.manv,
                            makh = item.makh,
                            tenkh = item.tenkh,
                            ngayhd = item.ngayhd,
                            tinhtrang = item.tinhtrang,
                            trigia = item.trigia,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt
                        };
                        listInvoice.Add(getInvoice);
                    }
                    invoiceResponse.hoadons = listInvoice.ToList();
                    invoiceResponse.totalHoaDon = _db.HoaDon.ToList().Count();
                }
                else
                {
                    var hoadons = await _db.HoaDon
                        .OrderBy(hd => hd.mahd)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();

                    foreach (var item in hoadons)
                    {
                        var getInvoice = new GetInvoice
                        {
                            id = item.id,
                            mahd = item.mahd,
                            manv = item.manv,
                            makh = item.makh,
                            tenkh = item.tenkh,
                            ngayhd = item.ngayhd,
                            tinhtrang = item.tinhtrang,
                            trigia = item.trigia,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt
                        };
                        listInvoice.Add(getInvoice);
                    }
                    invoiceResponse.hoadons = listInvoice.ToList();
                    invoiceResponse.totalHoaDon = _db.HoaDon.ToList().Count();
                }
                return StatusCode(StatusCodes.Status200OK, invoiceResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Get Hoa Don By ID
        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<InvoiceIdResponse>> getHoaDonById([FromRoute] Guid id, [FromHeader] string Authorization)
        {
            var invoiceResponse = new InvoiceIdResponse();
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var hoadon = _db.HoaDon.FirstOrDefault(i => i.id == id);
                if (hoadon != null)
                {
                    invoiceResponse.hoadon = new GetInvoice
                    {
                        id = hoadon.id,
                        mahd = hoadon.mahd,
                        manv = hoadon.manv,
                        makh = hoadon.makh,
                        tenkh = hoadon.tenkh,
                        ngayhd = hoadon.ngayhd,
                        tinhtrang = hoadon.tinhtrang,
                        trigia = hoadon.trigia,
                        createdAt = hoadon.createdAt,
                        updatedAt = hoadon.updatedAt
                    };
                    //var cthd = _db.CTHD.Where(c => c.mahd == hoadon.mahd).ToList();
                    //foreach (var ct in cthd)
                    //{
                    //    var car = await _db.Car.FirstOrDefaultAsync(i => i.macar == ct.macar);
                    //    var newCthd = new GetCthdDTO
                    //    {
                    //        soluong = ct.soluong,
                    //        macar = ct.macar,
                    //        tenxe = car.ten
                    //    };
                    //    invoiceResponse.cthds.Add(newCthd);
                    //}
                    invoiceResponse.cthds = _db.CTHD.Where(c => c.mahd == hoadon.mahd).ToList();
                    return StatusCode(StatusCodes.Status200OK, invoiceResponse);
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Remove Hoa Don By ID
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult<GetInvoice>> RemoveHoaDonById([FromRoute] Guid id, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var hoadon = _db.HoaDon.FirstOrDefault(i => i.id == id);
                if (hoadon != null)
                {
                    if (hoadon.tinhtrang != "Đã thanh toán")
                    {
                        _db.HoaDon.Remove(hoadon);
                        await _db.SaveChangesAsync();

                        var hd = new GetInvoice
                        {
                            id = hoadon.id,
                            mahd = hoadon.mahd,
                            manv = hoadon.manv,
                            makh = hoadon.makh,
                            tenkh = hoadon.tenkh,
                            ngayhd = hoadon.ngayhd,
                            tinhtrang = hoadon.tinhtrang,
                            trigia = hoadon.trigia,
                            createdAt = hoadon.createdAt,
                            updatedAt = hoadon.updatedAt
                        };

                        return StatusCode(StatusCodes.Status200OK, hd);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "Không thể xóa hóa đơn này!");
                    }
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Add Hoa Don
        [HttpPost]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult<InvoiceIdResponse>> addHoaDon([FromBody] AddInvoice invoice, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var invoiceIdResponse = new InvoiceIdResponse();
                var lastInvoice = _db.HoaDon.OrderByDescending(c => c.createdAt).FirstOrDefault();
                var maInvoice = "HD0";
                if (lastInvoice != null)
                {
                    var numberCar = lastInvoice.mahd.Substring(2);
                    maInvoice = $"HD{int.Parse(numberCar) + 1}";
                }

                var hd = new HoaDon()
                {
                    id = Guid.NewGuid(),
                    mahd = maInvoice,
                    makh = invoice.hoadon.makh,
                    manv = invoice.hoadon.manv,
                    ngayhd = invoice.hoadon.ngayhd,
                    tinhtrang = invoice.hoadon.tinhtrang,
                    trigia = invoice.hoadon.trigia,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };

                var khachhang = _db.User.FirstOrDefault(u => u.mauser == hd.makh);

                if (khachhang == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Không tìm thấy khách hàng!");
                }

                foreach(var item in invoice.cthd) {
                    var car = _db.Car.FirstOrDefault(c => c.macar == item.macar);
                    if (car != null)
                    {
                        var cthds = new CTHD
                        {
                            id = Guid.NewGuid(),
                            macar = item.macar,
                            mahd = hd.mahd,
                            soluong = item.soluong,
                            gia = car.gia,
                            createdAt = DateTime.Now,
                            updatedAt = DateTime.Now,
                        };
                        car.soluong = car.soluong - item.soluong;

                        _db.SaveChanges();
                        _db.CTHD.Add(cthds);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Not found!");
                    }
                }
                _db.HoaDon.Add(hd);
                await _db.SaveChangesAsync();

                var hoadon = _db.HoaDon.FirstOrDefault(i => i.mahd == hd.mahd);
                invoiceIdResponse.hoadon = new GetInvoice
                {
                    id = hoadon.id,
                    mahd = hoadon.mahd,
                    makh = hoadon.makh,
                    tenkh = hoadon.tenkh,
                    manv = hoadon.manv,
                    ngayhd = hoadon.ngayhd,
                    tinhtrang = hoadon.tinhtrang,
                    trigia = hoadon.trigia,
                    createdAt = hoadon.createdAt,
                    updatedAt = hoadon.updatedAt
                };
                invoiceIdResponse.cthds = _db.CTHD.Where(c => c.mahd == hd.mahd).ToList();
                if (hd.tinhtrang == "Đã thanh toán")
                {
                    _email.SendInvoiceEmail(khachhang.email, hd, invoiceIdResponse.cthds);
                } 
                else
                {
                    _email.SendOrderEmail(khachhang.email, hd, invoiceIdResponse.cthds);
                }

                return StatusCode(StatusCodes.Status200OK, invoiceIdResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Update Hoa Don
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> updateCar([FromRoute] Guid id, UpdateInvoiceDTO hoadonDTO, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var hoadon = _db.HoaDon.FirstOrDefault(i => i.id == id);

                if (hoadon != null)
                {
                    if (hoadon.tinhtrang != "Đã thanh toán")
                    {
                        hoadon.tinhtrang = hoadonDTO.tinhtrang;
                        hoadon.updatedAt = DateTime.Now;

                        await _db.SaveChangesAsync();
                        var getInvoice = new GetInvoice
                        {
                            id = hoadon.id,
                            mahd = hoadon.mahd,
                            manv = hoadon.manv,
                            makh = hoadon.makh,
                            tenkh = hoadon.tenkh,
                            ngayhd = hoadon.ngayhd,
                            tinhtrang = hoadon.tinhtrang,
                            trigia = hoadon.trigia,
                            createdAt = hoadon.createdAt,
                            updatedAt = hoadon.updatedAt
                        };
                        var kh = await _db.User.FirstOrDefaultAsync(item => item.mauser == getInvoice.makh);
                        var cthds = _db.CTHD.Where(c => c.mahd == hoadon.mahd).ToList();
                        _email.SendInvoiceEmail(kh.email, hoadon, cthds);

                        return StatusCode(StatusCodes.Status200OK, getInvoice);
                    }
                    else
                        return StatusCode(StatusCodes.Status403Forbidden, "Không thể cập nhật tình trạng hóa đơn này!");
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, "Không tìm thấy ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}
