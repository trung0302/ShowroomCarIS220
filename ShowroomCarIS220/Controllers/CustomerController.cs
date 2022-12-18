using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO;
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
        // GetCar
        [HttpGet]
        public async Task<ActionResult<CustomerResponse<List<Customer>>>> getCustomer([FromQuery] string? ten, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var customerResponse = new CustomerResponse<List<Customer>>();
            try
            {
                if(ten != null)
                {
                    var customers = (from customer in _db.Customer
                                where customer.ten.ToLower().Contains(ten.ToLower())
                                select new Customer
                                {
                                    id = customer.id,
                                    makh = customer.makh,
                                    ten = customer.ten,
                                    email = customer.email,
                                    diachi = customer.diachi,
                                    cccd= customer.cccd,
                                    sodienthoai = customer.sodienthoai,
                                })
                                .Skip(skip)
                                .Take((int)pageResults);
                    customerResponse.Customer = customers.ToList();
                    customerResponse.totalCustomers = _db.Customer.ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.Customer.Count();
                }
                else
                {
                    if (pageIndex != null)
                    {
                        var customers = await _db.Customer
                       .Skip(skip)
                       .Take(pageResults)
                       .ToListAsync();
                        customerResponse.Customer = customers;
                        customerResponse.totalCustomers = _db.Customer.ToList().Count();
                        customerResponse.totalCustomersFilter = customers.Count();
                    }
                    else
                    {
                        var customers = await _db.Customer
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                        customerResponse.Customer = customers;
                        customerResponse.totalCustomers = _db.Customer.ToList().Count();
                        customerResponse.totalCustomersFilter = _db.Customer.ToList().Count();
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
        public async Task<ActionResult<CustomerResponse<Customer>>> getCustomerById([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse<Customer>();
            try
            {
                var customer = await _db.Customer.FindAsync(id);
                if (customer != null)
                {
                    customerResponse.Customer = customer;
                    customerResponse.totalCustomers = _db.Customer.ToList().Count();
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
        // Remove car by Id
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<CustomerResponse<List<Customer>>>> getCustomerByNameOrCode([FromRoute] Guid id)
        {
            var customerResponse = new CustomerResponse<List<Customer>>();
            try
            {
                var customer = await _db.Customer.FindAsync(id);
                if (customer != null)
                {
                    _db.Customer.Remove(customer);
                    await _db.SaveChangesAsync();
                    customerResponse.Customer = _db.Customer.ToList();
                    customerResponse.totalCustomers = _db.Customer.ToList().Count();
                    customerResponse.totalCustomersFilter = customerResponse.Customer.Count();

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
        public async Task<ActionResult<CustomerResponse<List<Customer>>>> addCustomer(AddCustomerDTO customerDTO)
        {
            var customerResponse = new CustomerResponse<List<Customer>>();
            try
            {
                var customer = new Customer()
                {
                    id = Guid.NewGuid(),
                    makh = customerDTO.makh,
                    ten = customerDTO.makh,
                    diachi = customerDTO.diachi,
                    email = customerDTO.email,
                    sodienthoai = customerDTO.sodienthoai,
                    password= customerDTO.password,
                };

                await _db.Customer.AddAsync(customer);
                await _db.SaveChangesAsync();
                customerResponse.Customer = _db.Customer.ToList();
                customerResponse.totalCustomers = customerResponse.Customer.Count();
                customerResponse.totalCustomersFilter = customerResponse.Customer.Count();
                return StatusCode(StatusCodes.Status200OK, customerResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
        // Update car by Id
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CustomerResponse<List<Customer>>>> updateCustomer([FromRoute] Guid id, UpdateCustomerDTO customerDTO)
        {
            try
            {
                var customer = await _db.Customer.FindAsync(id);
                if (customer != null)
                {
                    customer.ten = customerDTO.ten;
                    customer.diachi = customerDTO.diachi;
                    customer.cccd = customerDTO.cccd;
                    customer.sodienthoai = customerDTO.sodienthoai;

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

