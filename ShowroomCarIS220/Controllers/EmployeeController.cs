using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Data;
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

        // getEmployee
        [HttpGet]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> getEmployee([FromQuery] string? name, [FromQuery] string? manv, [FromQuery] string? email, [FromQuery] string? search, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {

            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;

            var employeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                if (name != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.name.ToLower().Contains(name.ToLower())
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh= employee.ngaysinh,
                                         chucvu= employee.chucvu,
                                         gioitinh=employee.gioitinh,
                                         email = employee.email,
                                         sodienthoai = employee.sodienthoai,
                                         cccd = employee.cccd,
                                         //password = employee.password,
                                         //confirmpassword= employee.confirmpassword,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                                .OrderBy(c => c.manv)
                                .Skip(skip)
                                .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (manv != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.manv.Contains(manv)
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh = employee.ngaysinh,
                                         chucvu = employee.chucvu,
                                         gioitinh = employee.gioitinh,
                                         email = employee.email,
                                         sodienthoai = employee.sodienthoai,
                                         cccd = employee.cccd,
                                         //password = employee.password,
                                         //confirmpassword= employee.confirmpassword,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .OrderBy(c => c.manv)
                               .Skip(skip)
                               .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (email != null)
                {
                    var employees = (from employee in _db.Employee
                                     where employee.email.Contains(email)
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh = employee.ngaysinh,
                                         chucvu = employee.chucvu,
                                         gioitinh = employee.gioitinh,
                                         email = employee.email,
                                         sodienthoai = employee.sodienthoai,
                                         cccd = employee.cccd,
                                         //password = employee.password,
                                         //confirmpassword= employee.confirmpassword,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .OrderBy(c => c.manv)
                               .Skip(skip)
                               .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }

                else if (search != null)
                {
                    var employees = (from employee in _db.Employee
                                     where (employee.name.ToLower().Contains(search.ToLower()) || employee.manv.Contains(search))
                                     select new Employee
                                     {
                                         id = employee.id,
                                         manv = employee.manv,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh = employee.ngaysinh,
                                         chucvu = employee.chucvu,
                                         gioitinh = employee.gioitinh,
                                         email = employee.email,
                                         sodienthoai = employee.sodienthoai,
                                         cccd = employee.cccd,
                                         //password = employee.password,
                                         //confirmpassword= employee.confirmpassword,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .OrderBy(c => c.manv)
                               .Skip(skip)
                               .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                
                else if (pageIndex != null)
                {
                    var employees = await _db.Employee
                        .OrderBy(c => c.manv)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    employeeResponse.employees = employees;
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employees.Count();
                }
                else
                {
                    var employees = await _db.Employee
                       .OrderBy(c => c.manv)
                       .Skip(skip)
                       .Take(pageResults)
                       .ToListAsync();
                    employeeResponse.employees = employees;
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = _db.Employee.ToList().Count();
                }
                return StatusCode(StatusCodes.Status200OK, employeeResponse);
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
                    employeeResponse.employees = employee;
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
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> removeEmployeeByID([FromRoute] Guid id)
        {
            var employeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                var employee = await _db.Employee.FindAsync(id);
                if (employee != null)
                {
                    _db.Employee.Remove(employee);
                    await _db.SaveChangesAsync();
                    employeeResponse.employees = _db.Employee.ToList();
                    employeeResponse.totalEmployees = _db.Employee.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();

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

        // AddEmployee
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> addEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            var employeeResponse = new EmployeeResponse<List<Employee>>();
            try
            {
                var lastEmployee = _db.Employee.OrderByDescending(e => e.createdAt).FirstOrDefault();
                var maEmployee = "NV0";
                if (lastEmployee != null)
                {
                    var numberEmployee = lastEmployee.manv.Substring(2);
                    maEmployee = $"NV{int.Parse(numberEmployee) + 1}";
                }

                var newEmployee = new Employee()
                {
                    id = Guid.NewGuid(),
                    manv = maEmployee,
                    name = addEmployeeDTO.name,
                    diachi = addEmployeeDTO.diachi,
                    ngaysinh= addEmployeeDTO.ngaysinh,
                    chucvu= addEmployeeDTO.chucvu,
                    gioitinh= addEmployeeDTO.gioitinh,
                    email = addEmployeeDTO.email,
                    sodienthoai = addEmployeeDTO.sodienthoai,
                    cccd= addEmployeeDTO.cccd,
                    password = addEmployeeDTO.password,
                    confirmpassword = addEmployeeDTO.confirmpassword,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };
                //Mã hóa password
                newEmployee.password = BCrypt.Net.BCrypt.HashPassword(newEmployee.password);

                await _db.Employee.AddAsync(newEmployee);
                await _db.SaveChangesAsync();
                employeeResponse.employees = _db.Employee.ToList();
                employeeResponse.totalEmployees = employeeResponse.employees.Count();
                employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();

                return StatusCode(StatusCodes.Status200OK, employeeResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        // UpdateEmployeeById
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse<List<Employee>>>> updateCustomer([FromRoute] Guid id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            try
            {   
                var employee = await _db.Employee.FindAsync(id);
                if (employee != null)
                {
                    employee.name = updateEmployeeDTO.name;
                    employee.diachi = updateEmployeeDTO.diachi;
                    employee.ngaysinh = updateEmployeeDTO.ngaysinh;
                    employee.chucvu = updateEmployeeDTO.chucvu;
                    employee.sodienthoai = updateEmployeeDTO.sodienthoai;
                    employee.cccd = updateEmployeeDTO.cccd;

                    await _db.SaveChangesAsync();

                    var getEmployee = new GetEmployeeDTO()
                    {
                        id = employee.id,
                        manv = employee.manv,
                        name = employee.name,
                        diachi = employee.diachi,
                        ngaysinh = employee.ngaysinh,
                        chucvu = employee.chucvu,
                        gioitinh = employee.gioitinh,
                        email = employee.email,
                        sodienthoai = employee.sodienthoai,
                        cccd = employee.cccd,
                        createdAt = employee.createdAt,
                        updatedAt = employee.updatedAt,
                    };
                    return StatusCode(StatusCodes.Status200OK, getEmployee);
                }
                else
                     return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}
