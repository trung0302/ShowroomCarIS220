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
        public async Task<ActionResult<CustomerResponse<List<User>>>> getCustomer([FromQuery] string? name, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var customerResponse = new CustomerResponse<List<User>>();
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
                    customerResponse.customer = customers.ToList();
                    var result = from s in _db.User.ToList() where s.role == "customer" select s;
                    customerResponse.totalCustomers = result.Count();
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
                        customerResponse.customer = customers;
                        customerResponse.totalCustomers = _db.User.ToList().Count();
                        customerResponse.totalCustomersFilter = customers.Count();
                    }
                    else
                    {
                        var customers = await _db.User
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                        customerResponse.customer = customers;
                        customerResponse.totalCustomers = _db.User.ToList().Count();
                        customerResponse.totalCustomersFilter = _db.User.ToList().Count();
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
        public async Task<ActionResult<CustomerResponse<User>>> getCustomerById([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse<User>();
            try
            {
                var customer = await _db.User.FindAsync(id);
                if (customer != null)
                {
                    customerResponse.customer = customer;
                    var result = from s in _db.User.ToList() where s.role == "customer" select s;
                    customerResponse.totalCustomers = result.Count();
                    customerResponse.totalCustomersFilter = 1;

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
        public async Task<ActionResult<CustomerResponse<List<User>>>> removeCarById([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse<List<User>>();
            try
            {
                var customer = await _db.User.FindAsync(id);
                if (customer != null)
                {
                    _db.User.Remove(customer);
                    await _db.SaveChangesAsync();
                    customerResponse.customer = _db.User.ToList();
                    var result = from s in _db.User.ToList() where s.role == "customer" select s;
                    customerResponse.totalCustomers = result.Count();
                    customerResponse.totalCustomersFilter = 0;

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
        public async Task<ActionResult<CustomerResponse<List<User>>>> addCustomer(AddCustomerDTO customerDTO)
        {
            var customerResponse = new CustomerResponse<List<User>>();
            try
            {
                var lastCustomer = _db.Car.OrderByDescending(c => c.createdAt).FirstOrDefault();
                var maCustomer = "KH0";
                if (lastCustomer != null)
                {
                    var numberCar = lastCustomer.macar.Substring(2);
                    maCustomer = $"KH{int.Parse(numberCar) + 1}";
                }
                var newCustomer = new User()
                {
                    id = Guid.NewGuid(),
                    mauser = maCustomer,
                    name = customerDTO.makh,
                    diachi = customerDTO.diachi,
                    email = customerDTO.email,
                    sdt = customerDTO.sodienthoai,
                    password= customerDTO.password,
                };
                newCustomer.password = BCrypt.Net.BCrypt.HashPassword(newCustomer.password);
                await _db.User.AddAsync(newCustomer);
                await _db.SaveChangesAsync();
                customerResponse.customer = _db.User.ToList();
                var result = from s in _db.User.ToList() where s.role == "customer" select s;
                customerResponse.totalCustomers = result.Count();
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

