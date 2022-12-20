using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.Customer;
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
        public async Task<ActionResult<CustomerResponse>> getCustomer([FromQuery] string? name, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var customerResponse = new CustomerResponse();
            try
            {
                if(name != null)
                {
                    var customers = (from customer in _db.User 
                                    where customer.name.ToLower().Contains(name.ToLower()) 
                                    select new User
                                {
                                    id = customer.id,
                                    mauser = customer.mauser,
                                    name = customer.name,
                                    email = customer.email,
                                    diachi = customer.diachi,
                                    cccd= customer.cccd,
                                    sdt = customer.sdt,
                                })
                                .Skip(skip)
                                .Take((int)pageResults);
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
                            cccd = item.cccd,
                            email = item.email

                        }

                        );
                    }
                    customerResponse.customer = listGetCustomer;

                    customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                }
                else
                {
                    if (pageIndex != null)
                    {
                        var customers = await _db.User
                       .Skip(skip)
                       .Take(pageResults)
                       .ToListAsync();
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
                                cccd = item.cccd,
                                email = item.email

                            }

                            );
                        }
                         listGetCustomer = new List<GetCustomerDTO>();
                         listCustomer = _db.User.Where(index => index.role == "customer").ToList();
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
                                cccd = item.cccd,
                                email = item.email

                            }

                            );
                        }
                        customerResponse.customer = listGetCustomer;

                        customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                        customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                    }
                    else
                    {
                        var customers = await _db.User
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
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
                                cccd = item.cccd,
                                email = item.email

                            }

                            );
                        }
                        customerResponse.customer = listGetCustomer;

                        customerResponse.totalCustomers = _db.User.Where(index => index.role == "customer").ToList().Count();
                        customerResponse.totalCustomersFilter = customerResponse.customer.Count();
                    }

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
                            cccd = item.cccd,
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
                var lastCustomer = _db.User.Where(index => index.role == "customer").OrderByDescending(c => c.createdAt).FirstOrDefault();
                var maCustomer = "KH0";
                if (lastCustomer != null)
                {
                    var numberCar = lastCustomer.mauser.Substring(2);
                    maCustomer = $"KH{int.Parse(numberCar) + 1}";
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
                return StatusCode(StatusCodes.Status200OK, customerResponse);
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

                return StatusCode(StatusCodes.Status200OK, customer);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}

