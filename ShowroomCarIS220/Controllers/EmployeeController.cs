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
        public async Task<ActionResult<EmployeeResponse<List<User>>>> getEmployee([FromQuery] string? name, [FromQuery] string? mauser, [FromQuery] string? search, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            pageIndex = 0;
            pageSize = 10;
            int pageResults = (pageSize != null) ? (int)pageSize : 2;                                                                                                       
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;

            var employeeResponse = new EmployeeResponse<List<User>>();
            try
            {
                if (name != null)
                {
                    var employees = (from employee in _db.User
                                     where employee.name.ToLower().Contains(name.ToLower())
                                     select new User
                                     {
                                         id = employee.id,
                                         mauser = employee.mauser,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh= employee.ngaysinh,
                                         chucvu= employee.chucvu,
                                         gioitinh=employee.gioitinh,
                                         email = employee.email,
                                         sdt = employee.sdt,
                                         role = employee.role,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                                .Skip(skip)
                                .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (mauser != null)
                {
                    var employees = (from employee in _db.User
                                     where employee.mauser.Contains(mauser)
                                     select new User
                                     {
                                         id = employee.id,
                                         mauser = employee.mauser,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh = employee.ngaysinh,
                                         chucvu = employee.chucvu,
                                         gioitinh = employee.gioitinh,
                                         email = employee.email,
                                         sdt = employee.sdt,
                                         role=employee.role,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .Skip(skip)
                               .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }

                else if (search != null)
                {
                    var employees = (from employee in _db.User
                                     where (employee.name.ToLower().Contains(search.ToLower()) || employee.mauser.Contains(search))
                                     select new User
                                     {
                                         id = employee.id,
                                         mauser = employee.mauser,
                                         name = employee.name,
                                         diachi = employee.diachi,
                                         ngaysinh = employee.ngaysinh,
                                         chucvu = employee.chucvu,
                                         gioitinh = employee.gioitinh,
                                         email = employee.email,
                                         sdt = employee.sdt,
                                         role = employee.role,
                                         createdAt = employee.createdAt,
                                         updatedAt = employee.updatedAt,
                                     })
                               .Skip(skip)
                               .Take((int)pageResults);
                    employeeResponse.employees = employees.ToList();
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                
                else if (pageIndex != null)
                {
                    var employees = await _db.User
                        .OrderBy(c => c.mauser)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    employeeResponse.employees = employees;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employees.Count();
                }
                else
                {
                    var employees = await _db.User
                       .OrderBy(c => c.mauser)
                       .Skip(skip)
                       .Take(pageResults)
                       .ToListAsync();
                    employeeResponse.employees = employees;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = _db.User.ToList().Count();
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
        public async Task<ActionResult<EmployeeResponse<User>>> getEmployeeById([FromRoute] Guid id)
        {
            var employeeResponse = new EmployeeResponse<User>();
            try
            {
                var employee = await _db.User.FindAsync(id);
                if (employee != null)
                {
                    employeeResponse.employees = employee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
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
        public async Task<ActionResult<EmployeeResponse<List<User>>>> removeEmployeeByID([FromRoute] Guid id)
        {
            var employeeResponse = new EmployeeResponse<List<User>>();
            try
            {
                var employee = await _db.User.FindAsync(id);
                if (employee != null)
                {
                    _db.User.Remove(employee);
                    await _db.SaveChangesAsync();
                    employeeResponse.employees = _db.User.ToList();
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
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
        public async Task<ActionResult<EmployeeResponse<List<User>>>> addEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            var employeeResponse = new EmployeeResponse<List<User>>();
            try
            {
                var lastEmployee = _db.User.OrderByDescending(e => e.createdAt).FirstOrDefault();
                var maEmployee = "NV0";
                if (lastEmployee != null)
                {
                    var numberEmployee = lastEmployee.mauser.Substring(2);
                    maEmployee = $"NV{int.Parse(numberEmployee) + 1}";
                }

                var newEmployee = new User()
                {
                    id = Guid.NewGuid(),
                    mauser = maEmployee,
                    name = addEmployeeDTO.name,
                    diachi = addEmployeeDTO.diachi,
                    ngaysinh = addEmployeeDTO.ngaysinh,
                    chucvu = addEmployeeDTO.chucvu,
                    gioitinh = addEmployeeDTO.gioitinh,
                    email = addEmployeeDTO.email,
                    sdt = addEmployeeDTO.sdt,
                    password = addEmployeeDTO.password,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                    role = "employee",
                };
                //Mã hóa password
                newEmployee.password = BCrypt.Net.BCrypt.HashPassword(newEmployee.password);

                await _db.User.AddAsync(newEmployee);
                await _db.SaveChangesAsync();
                employeeResponse.employees = _db.User.ToList();
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
        public async Task<ActionResult<EmployeeResponse<List<User>>>> updateCustomer([FromRoute] Guid id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            try
            {   
                var employee = await _db.User.FindAsync(id);
                if (employee != null)
                {
                    employee.name = updateEmployeeDTO.name;
                    employee.diachi = updateEmployeeDTO.diachi;
                    employee.ngaysinh = updateEmployeeDTO.ngaysinh;
                    employee.chucvu = updateEmployeeDTO.chucvu;
                    employee.sdt = updateEmployeeDTO.sdt;
                    employee.updatedAt = DateTime.Now;

                    await _db.SaveChangesAsync();

                    var getEmployee = new GetEmployeeDTO()
                    {
                        id = employee.id,
                        mauser = employee.mauser,
                        name = employee.name,
                        diachi = employee.diachi,
                        ngaysinh = employee.ngaysinh,
                        chucvu = employee.chucvu,
                        email = employee.email,
                        sdt = employee.sdt,
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
