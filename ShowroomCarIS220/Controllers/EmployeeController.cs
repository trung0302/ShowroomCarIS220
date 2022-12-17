using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO;
using ShowroomCarIS220.DTO.Employee;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;

namespace ShowroomCarIS220.Controllers
{
    [Route("users/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _db;
        public EmployeeController(DataContext db)
        {
            _db = db;
        }

        //getEmployee
        [HttpGet]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> getEmployee([FromQuery] string? ten, [FromQuery] string? manv, [FromQuery] string? email, [FromQuery] string? search, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            var EmployeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                if (ten != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.ten.ToLower().Contains(ten.ToLower())
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         ten = employee.ten,
                                         ngaysinh= employee.ngaysinh,
                                         sodienthoai = employee.sodienthoai,
                                         email = employee.email,
                                         diachi = employee.diachi,
                                         cccd = employee.cccd,
                                         chucvu= employee.chucvu,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                                .Skip(skip)
                                .Take((int)pageResults);
                    EmployeeResponse.Employee = employees.ToList();
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = EmployeeResponse.Employee.Count();
                }
                else if (search != null)
                {
                    var employees = (from employee in _db.Employee
                                     where (employee.ten.ToLower().Contains(search.ToLower()) || employee.manv.Contains(search))
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         ten = employee.ten,
                                         ngaysinh = employee.ngaysinh,
                                         sodienthoai = employee.sodienthoai,
                                         email = employee.email,
                                         diachi = employee.diachi,
                                         cccd = employee.cccd,
                                         chucvu = employee.chucvu,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .Skip(skip)
                               .Take((int)pageResults);
                    EmployeeResponse.Employee = employees.ToList();
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = EmployeeResponse.Employee.Count();
                }
                else if (email != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.email.Contains(email)
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         ten = employee.ten,
                                         ngaysinh = employee.ngaysinh,
                                         sodienthoai = employee.sodienthoai,
                                         email = employee.email,
                                         diachi = employee.diachi,
                                         cccd = employee.cccd,
                                         chucvu = employee.chucvu,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .Skip(skip)
                               .Take((int)pageResults);
                    EmployeeResponse.Employee = employees.ToList();
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = EmployeeResponse.Employee.Count();
                }
                else if (manv != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.manv.Contains(manv)
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         ten = employee.ten,
                                         ngaysinh = employee.ngaysinh,
                                         sodienthoai = employee.sodienthoai,
                                         email = employee.email,
                                         diachi = employee.diachi,
                                         cccd = employee.cccd,
                                         chucvu = employee.chucvu,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .Skip(skip)
                               .Take((int)pageResults);
                    EmployeeResponse.Employee = employees.ToList();
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = EmployeeResponse.Employee.Count();
                }
                else if (pageIndex != null)
                {
                    var employees = await _db.Employee
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    EmployeeResponse.Employee = employees;
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = employees.Count();
                }
                else
                {
                    var employees = await _db.Employee
                       .Skip(skip)
                       .Take(pageResults)
                       .ToListAsync();
                    EmployeeResponse.Employee = employees;
                    EmployeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    EmployeeResponse.totalEmployeesFilter = employees.ToList().Count();
                }
                return StatusCode(StatusCodes.Status200OK, EmployeeResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        // GetEmployeeById
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse<Employee>>> getEmployeeById([FromRoute] Guid id)
        {
            var employeeResponse = new EmployeeResponse<Employee>();
            try
            {
                var employee = await _db.Employee.FindAsync(id);
                if (employee != null)
                {
                    employeeResponse.Employee = employee;
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = 1;

                    return StatusCode(StatusCodes.Status200OK, employeeResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        // RemoveEmployeeById
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> getCarByNameOrCode([FromRoute] Guid id)
        {
            var employeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                var employee = await _db.Employee.FindAsync(id);
                if (employee != null)
                {
                    _db.Employee.Remove(employee);
                    await _db.SaveChangesAsync();
                    employeeResponse.Employee = _db.Employee.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.Employee.Count();

                    return StatusCode(StatusCodes.Status200OK, employeeResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }



        //Add Employee
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> addEmployee(AddEmployeeDTO employeeDTO)
        {
            var employeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                var employee = new Employee()
                {
                    id = Guid.NewGuid(),
                    manv = employeeDTO.manv,
                    ten = employeeDTO.ten,
                    ngaysinh=employeeDTO.ngaysinh,
                    gioitinh=employeeDTO.gioitinh,
                    diachi = employeeDTO.diachi,
                    email = employeeDTO.email,
                    sodienthoai = employeeDTO.sodienthoai,
                    cccd=employeeDTO.cccd,
                    chucvu=employeeDTO.chucvu,
                    password = employeeDTO.password,
                    confirmpassword = employeeDTO.confirmpassword,
                };

                await _db.Employee.AddAsync(employee);
                await _db.SaveChangesAsync();
                employeeResponse.Employee = _db.Employee.ToList();
                employeeResponse.totalEmployees = employeeResponse.Employee.Count();
                employeeResponse.totalEmployeesFilter = employeeResponse.Employee.Count();
                return StatusCode(StatusCodes.Status200OK, employeeResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }



        // UpdateEmployeeById
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> updateCustomer([FromRoute] Guid id, UpdateEmployeeDTO employeeDTO)
        {
            try
            {   
                var employee = await _db.Employee.FindAsync(id);
                if (employee != null)
                {
                    employee.ten = employeeDTO.ten;
                    employee.ngaysinh = employee.ngaysinh;
                    employee.diachi = employeeDTO.diachi;
                    employee.sodienthoai = employeeDTO.sodienthoai;
                    employee.chucvu = employee.chucvu;
                    employee.cccd = employeeDTO.cccd;
                }
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}
