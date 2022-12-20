﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.Customer;
using ShowroomCarIS220.DTO.Employee;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;

namespace ShowroomCarIS220.Controllers
{

    [Route("users/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _db;
        public CustomerController(DataContext db)
        {
            _db = db;
        }
        // GetCustomer
        [HttpGet]
        public async Task<ActionResult<CustomerResponse>> getCustomer([FromQuery] string? name, [FromQuery] string? search, [FromQuery] string? mauser, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var customerResponse = new CustomerResponse();
            try
            {
                if (search != null)
                {
                    var listGetCustomer = new List<GetCustomerDTO>();
                    var listUserCustomer = _db.User.Where(i => i.name.ToLower().Contains(search.ToLower()) || i.mauser.Contains(search)).ToList();
                    foreach (var item in listUserCustomer)
                    {
                        listGetCustomer.Add(new GetCustomerDTO
                        {
                            id = item.id,
                            makh = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sodienthoai = item.sdt,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    customerResponse.customer = listGetCustomer;
                    customerResponse.totalCustomers = _db.User.Where(i => i.role == "customer").ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                else if (name != null)
                {
                    var listGetCustomer = new List<GetCustomerDTO>();
                    var listUserCustomer = _db.User.Where(i => i.name.ToLower().Contains(name.ToLower())).ToList();
                    foreach (var item in listUserCustomer)
                    {
                        listGetCustomer.Add(new GetCustomerDTO
                        {
                            id = item.id,
                            makh = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sodienthoai = item.sdt,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    customerResponse.customer = listGetCustomer;
                    customerResponse.totalCustomers = _db.User.ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                else if (mauser != null)
                {
                    var listGetCustomer = new List<GetCustomerDTO>();
                    var listUserCustomer = _db.User.Where(i => i.mauser.ToLower().Contains(mauser.ToLower())).ToList();
                    foreach (var item in listUserCustomer)
                    {
                        listGetCustomer.Add(new GetCustomerDTO
                        {
                            id = item.id,
                            makh = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sodienthoai = item.sdt,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    customerResponse.customer = listGetCustomer;
                    customerResponse.totalCustomers = _db.User.ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                else if (pageIndex != null)
                {
                        
                      
                        var listGetCustomer = new List<GetCustomerDTO>();
                        var listCustomer = _db.User.Where(index => index.role == "customer").ToList()
                        .OrderBy(c => c.mauser)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToList();
                        foreach (var item in listCustomer)
                        {
                            listGetCustomer.Add(new GetCustomerDTO
                            {
                                id = item.id,
                                makh = item.mauser,
                                name = item.name,
                                diachi = item.diachi,
                                ngaysinh = item.ngaysinh,
                                cccd = item.cccd,
                                gioitinh = item.gioitinh,
                                email = item.email,
                                sodienthoai = item.sdt,
                                createdAt = item.createdAt,
                                updatedAt = item.updatedAt,

                            }

                            );
                        }
                        customerResponse.customer = listGetCustomer;

                        customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                        customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                else
                {
                        var listGetCustomer = new List<GetCustomerDTO>();
                        var listCustomer = _db.User.Where(index => index.role == "customer")
                        .OrderBy(c => c.mauser)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToList();
                        foreach (var item in listCustomer)
                        {
                            listGetCustomer.Add(new GetCustomerDTO
                            {
                                id = item.id,
                                makh = item.mauser,
                                name = item.name,
                                diachi = item.diachi,
                                ngaysinh = item.ngaysinh,
                                cccd = item.cccd,
                                gioitinh = item.gioitinh,
                                email = item.email,
                                sodienthoai = item.sdt,
                                createdAt = item.createdAt,
                                updatedAt = item.updatedAt,
                            }

                            );
                        }
                        customerResponse.customer = listGetCustomer;
                        customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                        customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                return StatusCode(StatusCodes.Status200OK, customerResponse);
            }
            catch(Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        // GetCustomerById
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomerResponse>> getCustomerById([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse();
            try
            {
                var customer = await _db.User.FindAsync(id);
                if (customer != null)
                {
                    var listGetCustomer = new List<GetCustomerDTO>();
                   
                        listGetCustomer.Add(new GetCustomerDTO
                        {
                            id = customer.id,
                            makh = customer.mauser,
                            name = customer.name,
                            diachi = customer.diachi,
                            ngaysinh = customer.ngaysinh,
                            gioitinh = customer.gioitinh,
                            cccd = customer.cccd,
                            email = customer.email

                        }

                        );
                    
                    //customerResponse.customer = listGetCustomer;

                    //customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                    //customerResponse.totalCustomersFilter = customerResponse.customer.Count();

                    return StatusCode(StatusCodes.Status200OK, listGetCustomer);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        // Remove customer by Id
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<CustomerResponse>> removeCarById([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse();
            try
            {
                var customer = await _db.User.FindAsync(id);
                if (customer != null)
                {
                    _db.User.Remove(customer);
                    await _db.SaveChangesAsync();
                    var listGetCustomer = new List<GetCustomerDTO>();
                    var listCustomer = _db.User.Where(index => index.role == "customer").ToList();
                    foreach (var item in listCustomer)
                    {
                        listGetCustomer.Add(new GetCustomerDTO
                        {
                            id = item.id,
                            makh = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            gioitinh = item.gioitinh,
                            cccd =  item.cccd,
                            email = item.email

                        }

                        );
                    }
                    customerResponse.customer = listGetCustomer;

                    customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.customer.Count();

                    return StatusCode(StatusCodes.Status200OK, customerResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        //Add Customer
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> addCustomer(AddCustomerDTO customerDTO)
        {
            var customerResponse = new CustomerResponse();
            try
            {
                var lastCustomer = _db.User.Where(index => index.role == "customer").OrderByDescending(c => c.mauser).FirstOrDefault();
                var maCustomer = "KH0";
                if (lastCustomer != null)
                {
                    var numberCustomer = lastCustomer.mauser.Substring(2);
                    maCustomer = $"KH{int.Parse(numberCustomer) + 1}";
                    //var x = int.Parse(lastCustomer.mauser.Substring(2));
                }
                var newCustomer = new User()
                {
                    id = Guid.NewGuid(),
                    mauser = maCustomer,
                    name = customerDTO.ten,
                    diachi = customerDTO.diachi,
                    email = customerDTO.email,
                    sdt = customerDTO.sodienthoai,
                    gioitinh = customerDTO.gioitinh,
                    ngaysinh= customerDTO.ngaysinh,
                    password= customerDTO.password,
                    role = "customer",
                };
                newCustomer.password = BCrypt.Net.BCrypt.HashPassword(newCustomer.password);
                await _db.User.AddAsync(newCustomer);
                await _db.SaveChangesAsync();
                // Response
                var listGetCustomer = new List<GetCustomerDTO>();
                var listCustomer = _db.User.Where(index => index.role == "customer").ToList();
                foreach(var item in listCustomer)
                {
                    listGetCustomer.Add(new GetCustomerDTO
                    {
                        id = item.id,
                        makh = item.mauser,
                        name = item.name,
                        diachi = item.diachi,
                        ngaysinh = item.ngaysinh,
                        gioitinh = item.gioitinh,
                        cccd = item.cccd,
                        email = item.email

                    }

                    ) ;
                }
                customerResponse.customer = listGetCustomer;
                
                customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                return StatusCode(StatusCodes.Status200OK, listGetCustomer);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        // Update customer by Id
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> updateCustomer([FromRoute] Guid id, UpdateCustomerDTO customerDTO)
        {
            try
            {
                var customer = await _db.User.FindAsync(id);
                if (customer != null)
                {
                    customer.name = customerDTO.ten;
                    customer.diachi = customerDTO.diachi;
                    customer.cccd = customerDTO.cccd;
                    customer.sdt = customerDTO.sodienthoai;

                }
                await _db.SaveChangesAsync();
                var getCustomer = new GetCustomerDTO()
                {
                    id = customer.id,
                    makh = customer.mauser,
                    name = customer.name,
                    diachi = customer.diachi,
                    cccd = customer.cccd,
                    sodienthoai = customer.sdt,
                    createdAt = customer.createdAt,
                    updatedAt = customer.updatedAt,
                };

                return StatusCode(StatusCodes.Status200OK, getCustomer);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}

