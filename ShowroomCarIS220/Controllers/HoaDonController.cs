using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Controllers
{
    [Route("hoadons")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly DataContext _db;
        public HoaDonController(DataContext db)
        {
            _db = db;
        }

        //Get Hoa Don
        [HttpGet]
        public async Task<ActionResult<InvoiceResponse<List<HoaDon>>>> getHoaDon([FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            //var pageCounts = Math.Ceiling(_db.HoaDon.Count() / pageResults);

            var invoiceResponse = new InvoiceResponse<List<HoaDon>>();
            try
            {
                if (pageIndex != null)
                {
                    var hoadons = await _db.HoaDon
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    invoiceResponse.hoadons = hoadons;
                    invoiceResponse.totalHoaDon = _db.HoaDon.ToList().Count();
                }
                else
                {
                    var hoadons = await _db.HoaDon
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    invoiceResponse.hoadons = hoadons;
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
        public async Task<ActionResult<InvoiceResponse<HoaDon>>> getHoaDonById([FromRoute] Guid id)
        {
            var invoiceResponse = new InvoiceResponse<HoaDon>();
            try
            {
                var hoadon = await _db.HoaDon.FindAsync(id);
                if (hoadon != null)
                {
                    invoiceResponse.hoadons = hoadon;
                    invoiceResponse.totalHoaDon = _db.HoaDon.ToList().Count();

                    return StatusCode(StatusCodes.Status200OK, invoiceResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Remove Hoa Don By ID
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<InvoiceResponse<List<HoaDon>>>> RemoveHoaDonById([FromRoute] Guid id)
        {
            var invoiceResponse = new InvoiceResponse<List<HoaDon>>();
            try
            {
                var hoadon = await _db.HoaDon.FindAsync(id);
                if (hoadon != null)
                {
                    _db.HoaDon.Remove(hoadon);
                    await _db.SaveChangesAsync();
                    invoiceResponse.hoadons = _db.HoaDon.ToList();
                    invoiceResponse.totalHoaDon = _db.HoaDon.ToList().Count();

                    return StatusCode(StatusCodes.Status200OK, invoiceResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Add Hoa Don
        [HttpPost]
        public async Task<ActionResult<InvoiceIdResponse>> addHoaDon([FromBody] AddInvoice invoice)
        {
            try
            {
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

                return StatusCode(StatusCodes.Status200OK, invoiceIdResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Update Hoa Don
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<InvoiceResponse<List<HoaDon>>>> updateCar([FromRoute] Guid id, UpdateInvoiceDTO hoadonDTO)
        {
            try
            {
                var hoadon = await _db.HoaDon.FindAsync(id);
                if (hoadon != null)
                {
                    hoadon.tinhtrang = hoadonDTO.tinhtrang;
                    hoadon.updatedAt = DateTime.Now;
                }
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, hoadon);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}
