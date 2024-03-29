﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using ShowroomCarIS220.DTO.Car;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using ShowroomCarIS220.Auth;

namespace ShowroomCarIS220.Controllers
{
    [Route("cars")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class CarController : ControllerBase
    {
        private readonly DataContext _db;
        private Authentication _auth = new Authentication();
        public CarController(DataContext db)
        {
            _db = db;
        }

        //Get Car
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<CarResponse<List<Car>>>> getCar([FromQuery] string? ten, [FromQuery] string? macar,
            [FromQuery] string? thuonghieu, [FromQuery] string? search, [FromQuery] bool? advice, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 15;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;

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
                                })
                                .OrderBy(c => c.createdAt)
                                .Skip(skip)
                                .Take(pageResults);
                    carResponse.cars = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.cars.Count();
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
                                })
                                .OrderBy(c => c.createdAt)
                                .Skip(skip)
                                .Take(pageResults);
                    carResponse.cars = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.cars.Count();
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
                                })
                                .OrderBy(c => c.createdAt)
                                .Skip(skip)
                                .Take(pageResults);
                    carResponse.cars = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.cars.Count();
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
                                })
                                .OrderBy(c => c.createdAt)
                                .Skip(skip)
                                .Take(pageResults);
                    carResponse.cars = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.cars.Count();
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
                                })
                                .OrderBy(c => c.createdAt)
                                .Skip(skip)
                                .Take(pageResults);
                    carResponse.cars = cars.ToList();
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = carResponse.cars.Count();
                }
                else if (pageIndex != null)
                {
                    var cars = await _db.Car
                        .OrderBy(c => c.createdAt)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    carResponse.cars = cars;
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = cars.Count();
                }
                else
                {
                    var cars = await _db.Car
                        .OrderBy(c => c.createdAt)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    carResponse.cars = cars;
                    carResponse.totalCars = _db.Car.ToList().Count();
                    carResponse.totalCarsFilter = _db.Car.ToList().Count();
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
        [AllowAnonymous]
        public async Task<ActionResult<Car>> getCarById([FromRoute] Guid id) 
        {
            try
            {
                var car = await _db.Car.FindAsync(id);
                if (car != null)
                {
                    return StatusCode(StatusCodes.Status200OK, car);
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
        public async Task<ActionResult<Car>> RemoveCarById([FromRoute] Guid id, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var car = await _db.Car.FindAsync(id);
                if (car != null)
                {
                    _db.Car.Remove(car);
                    await _db.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status200OK, car);
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        
        //Add Car
        [HttpPost]
        public async Task<ActionResult<Car>> addCar(AddCarDTO carDTO, [FromHeader] string Authorization)
        {
            var carResponse = new CarResponse<Car>();
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var lastCar = _db.Car.OrderByDescending(c => c.createdAt).FirstOrDefault();
                var maCar = "OT0";
                if (lastCar != null)
                {
                    var numberCar = lastCar.macar.Substring(2);
                    maCar = $"OT{int.Parse(numberCar) + 1}";
                }

                var car = new Car()
                {
                    id = Guid.NewGuid(),
                    macar = maCar,
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

                return StatusCode(StatusCodes.Status200OK, car);
            }
            catch(Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Update Car
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> updateCar([FromRoute] Guid id, UpdateCarDTO carDTO, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
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

                    await _db.SaveChangesAsync();

                    var getCar = new GetCarDTO {
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
                     };

                    return StatusCode(StatusCodes.Status200OK, getCar);
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
