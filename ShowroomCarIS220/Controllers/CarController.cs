using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ShowroomCarIS220.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly DataContext _db;
        public CarController(DataContext db)
        {
            _db = db;
        }

        //Get Car
        [HttpGet]
        public async Task<ActionResult<CarResponse<List<Car>>>> getCar([FromQuery] string? ten, [FromQuery] string? macar,
            [FromQuery] string? thuonghieu, [FromQuery] string? search, [FromQuery] bool? advice, [FromQuery] int? pageSize, [FromQuery] int? pageIndex)
        {
            //int limit = (pageSize != null) ? pageSize : 15;
            //int skip = (pageIndex != null) ? (pageIndex * limit) : 0;
            var carResponse = new CarResponse<List<Car>>();

            try
            {
                if (search != null)
                {
                    var cars = (from car in _db.Car
                                where (car.ten.ToLower().Contains(search.ToLower()) || car.macar.Contains(search))
                                select new Car
                                {
                                    id = car.id,
                                    macar = car.macar,
                                    ten = car.ten,
                                    thuonghieu = car.thuonghieu,
                                    dongco = car.dongco,
                                    socho = car.socho,
                                    kichthuoc = car.kichthuoc,
                                    nguongoc = car.nguongoc,
                                    vantoctoida = car.vantoctoida,
                                    dungtich = car.dungtich,
                                    tieuhaonhienlieu = car.tieuhaonhienlieu,
                                    congsuatcucdai = car.congsuatcucdai,
                                    mausac = car.mausac,
                                    gia = car.gia,
                                    hinhanh = car.hinhanh,
                                    mota = car.mota,
                                    namsanxuat = car.namsanxuat,
                                    soluong = car.soluong,
                                    advice = car.advice,
                                    createdAt = car.createdAt,
                                    updatedAt = car.updatedAt,
                                });
                    carResponse.Car = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                else if (ten != null)
                {
                    var cars = (from car in _db.Car
                                where car.ten.ToLower().Contains(ten.ToLower())
                                select new Car
                                {
                                    id = car.id,
                                    macar = car.macar,
                                    ten = car.ten,
                                    thuonghieu = car.thuonghieu,
                                    dongco = car.dongco,
                                    socho = car.socho,
                                    kichthuoc = car.kichthuoc,
                                    nguongoc = car.nguongoc,
                                    vantoctoida = car.vantoctoida,
                                    dungtich = car.dungtich,
                                    tieuhaonhienlieu = car.tieuhaonhienlieu,
                                    congsuatcucdai = car.congsuatcucdai,
                                    mausac = car.mausac,
                                    gia = car.gia,
                                    hinhanh = car.hinhanh,
                                    mota = car.mota,
                                    namsanxuat = car.namsanxuat,
                                    soluong = car.soluong,
                                    advice = car.advice,
                                    createdAt = car.createdAt,
                                    updatedAt = car.updatedAt,
                                });
                    carResponse.Car = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                else if (macar != null)
                {
                    var cars = (from car in _db.Car
                                where car.macar.Contains(macar)
                                select new Car
                                {
                                    id = car.id,
                                    macar = car.macar,
                                    ten = car.ten,
                                    thuonghieu = car.thuonghieu,
                                    dongco = car.dongco,
                                    socho = car.socho,
                                    kichthuoc = car.kichthuoc,
                                    nguongoc = car.nguongoc,
                                    vantoctoida = car.vantoctoida,
                                    dungtich = car.dungtich,
                                    tieuhaonhienlieu = car.tieuhaonhienlieu,
                                    congsuatcucdai = car.congsuatcucdai,
                                    mausac = car.mausac,
                                    gia = car.gia,
                                    hinhanh = car.hinhanh,
                                    mota = car.mota,
                                    namsanxuat = car.namsanxuat,
                                    soluong = car.soluong,
                                    advice = car.advice,
                                    createdAt = car.createdAt,
                                    updatedAt = car.updatedAt,
                                });
                    carResponse.Car = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                else if (thuonghieu != null)
                {
                    var cars = (from car in _db.Car
                                where car.thuonghieu.Contains(thuonghieu)
                                select new Car
                                {
                                    id = car.id,
                                    macar = car.macar,
                                    ten = car.ten,
                                    thuonghieu = car.thuonghieu,
                                    dongco = car.dongco,
                                    socho = car.socho,
                                    kichthuoc = car.kichthuoc,
                                    nguongoc = car.nguongoc,
                                    vantoctoida = car.vantoctoida,
                                    dungtich = car.dungtich,
                                    tieuhaonhienlieu = car.tieuhaonhienlieu,
                                    congsuatcucdai = car.congsuatcucdai,
                                    mausac = car.mausac,
                                    gia = car.gia,
                                    hinhanh = car.hinhanh,
                                    mota = car.mota,
                                    namsanxuat = car.namsanxuat,
                                    soluong = car.soluong,
                                    advice = car.advice,
                                    createdAt = car.createdAt,
                                    updatedAt = car.updatedAt,
                                });
                    carResponse.Car = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                else if (advice != null)
                {
                    var cars = (from car in _db.Car
                                where car.advice.Equals(advice)
                                select new Car
                                {
                                    id = car.id,
                                    macar = car.macar,
                                    ten = car.ten,
                                    thuonghieu = car.thuonghieu,
                                    dongco = car.dongco,
                                    socho = car.socho,
                                    kichthuoc = car.kichthuoc,
                                    nguongoc = car.nguongoc,
                                    vantoctoida = car.vantoctoida,
                                    dungtich = car.dungtich,
                                    tieuhaonhienlieu = car.tieuhaonhienlieu,
                                    congsuatcucdai = car.congsuatcucdai,
                                    mausac = car.mausac,
                                    gia = car.gia,
                                    hinhanh = car.hinhanh,
                                    mota = car.mota,
                                    namsanxuat = car.namsanxuat,
                                    soluong = car.soluong,
                                    advice = car.advice,
                                    createdAt = car.createdAt,
                                    updatedAt = car.updatedAt,
                                });
                    carResponse.Car = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                else
                {
                    carResponse.Car = _db.Car.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();
                }
                return StatusCode(StatusCodes.Status200OK, carResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Get Car By ID
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarResponse<Car>>> getCarById([FromRoute] Guid id) 
        {
            var carResponse = new CarResponse<Car>();
            try
            {
                var car = await _db.Car.FindAsync(id);
                if (car != null)
                {
                    carResponse.Car = car;
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = 1;
                 
                    return StatusCode(StatusCodes.Status200OK, carResponse);
                }
                else 
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }       
            catch(Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Remove Car By ID
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<CarResponse<List<Car>>>> getCarByNameOrCode([FromRoute] Guid id)
        {
            var carResponse = new CarResponse<List<Car>>();
            try
            {
                var car = await _db.Car.FindAsync(id);
                if (car != null)
                {
                    _db.Car.Remove(car);
                    await _db.SaveChangesAsync();
                    carResponse.Car = _db.Car.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.Car.Count();

                    return StatusCode(StatusCodes.Status200OK, carResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<CarResponse<List<Car>>>> addCar(AddCarDTO carDTO)
        {
            var carResponse = new CarResponse<List<Car>>();
            try
            {
                var car = new Car()
                {
                    id = Guid.NewGuid(),
                    macar = carDTO.macar,
                    ten = carDTO.ten,
                    thuonghieu = carDTO.thuonghieu,
                    dongco = carDTO.dongco,
                    socho = carDTO.socho,
                    kichthuoc = carDTO.kichthuoc,
                    nguongoc = carDTO.nguongoc,
                    vantoctoida = carDTO.vantoctoida,
                    dungtich = carDTO.dungtich,
                    tieuhaonhienlieu = carDTO.tieuhaonhienlieu,
                    congsuatcucdai = carDTO.congsuatcucdai,
                    mausac = carDTO.mausac,
                    gia = carDTO.gia,
                    hinhanh = carDTO.hinhanh,
                    mota = carDTO.mota,
                    namsanxuat = carDTO.namsanxuat,
                    soluong = carDTO.soluong,
                    advice = carDTO.advice,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };

                await _db.Car.AddAsync(car);
                await _db.SaveChangesAsync();
                carResponse.Car = _db.Car.ToList();
                carResponse.totalCars = carResponse.Car.Count();
                carResponse.totalCarsFilter = carResponse.Car.Count();
                return StatusCode(StatusCodes.Status200OK, carResponse);
            }
            catch(Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CarResponse<List<Car>>>> updateCar([FromRoute] Guid id, UpdateCarDTO carDTO)
        {
            try
            {
                var car = await _db.Car.FindAsync(id);
                if (car != null)
                {
                    car.ten = carDTO.ten;
                    car.thuonghieu = carDTO.thuonghieu;
                    car.dongco = carDTO.dongco;
                    car.socho = carDTO.socho;
                    car.kichthuoc = carDTO.kichthuoc;
                    car.nguongoc = carDTO.nguongoc;
                    car.vantoctoida = carDTO.vantoctoida;
                    car.dungtich = carDTO.dungtich;
                    car.tieuhaonhienlieu = carDTO.tieuhaonhienlieu;
                    car.congsuatcucdai = carDTO.congsuatcucdai;
                    car.mausac = carDTO.mausac;
                    car.gia = carDTO.gia;
                    car.hinhanh = carDTO.hinhanh;
                    car.mota = carDTO.mota;
                    car.namsanxuat = carDTO.namsanxuat;
                    car.soluong = carDTO.soluong;
                    car.advice = carDTO.advice;
                    car.updatedAt = DateTime.Now;
                }
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, car);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}
