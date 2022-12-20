using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.Employee;
using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using System.Data;

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
        public async Task<ActionResult<EmployeeResponse>> getEmployee([FromQuery] string? name, [FromQuery] string? mauser, [FromQuery] string? search, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 10;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;

            var employeeResponse = new EmployeeResponse();
            try
            {
                if (search != null)
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    var listUserEmployee = _db.User.Where(i => i.name.ToLower().Contains(search.ToLower()) || i.mauser.Contains(search)).ToList();
                    foreach (var item in listUserEmployee)
                    {
                        listGetEmployee.Add(new GetEmployeeDTO
                        {
                            id = item.id,
                            mauser = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            chucvu = item.chucvu,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sdt = item.sdt,
                            role = item.role,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    employeeResponse.employees = listGetEmployee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (name != null)
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    var listUserEmployee = _db.User.Where(i => i.name.ToLower().Contains(name.ToLower())).ToList();
                    foreach (var item in listUserEmployee)
                    {
                        listGetEmployee.Add(new GetEmployeeDTO
                        {
                            id = item.id,
                            mauser = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            chucvu = item.chucvu,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sdt = item.sdt,
                            role = item.role,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    employeeResponse.employees = listGetEmployee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (mauser != null)
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    var listUserEmployee = _db.User.Where(i => i.mauser.ToLower().Contains(mauser.ToLower())).ToList();
                    foreach (var item in listUserEmployee)
                    {
                        listGetEmployee.Add(new GetEmployeeDTO
                        {
                            id = item.id,
                            mauser = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            chucvu = item.chucvu,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sdt = item.sdt,
                            role = item.role,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    employeeResponse.employees = listGetEmployee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else if (pageIndex != null)
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    var listUserEmployee = _db.User.Where(i => i.role == "customer")
                        .OrderBy(c => c.mauser)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToList();
                    foreach (var item in listUserEmployee)
                    {
                        listGetEmployee.Add(new GetEmployeeDTO
                        {
                            id = item.id,
                            mauser = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            chucvu = item.chucvu,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sdt = item.sdt,
                            role = item.role,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    employeeResponse.employees = listGetEmployee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                else
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    var listUserEmployee = _db.User.Where(i => i.role == "employee")
                        .OrderBy(c => c.mauser)
                        .Skip(skip)
                        .Take(pageResults)
                        .ToList();
                    foreach (var item in listUserEmployee)
                    {
                        listGetEmployee.Add(new GetEmployeeDTO
                        {
                            id = item.id,
                            mauser = item.mauser,
                            name = item.name,
                            diachi = item.diachi,
                            ngaysinh = item.ngaysinh,
                            chucvu = item.chucvu,
                            cccd = item.cccd,
                            gioitinh = item.gioitinh,
                            email = item.email,
                            sdt = item.sdt,
                            role = item.role,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt,
                        });
                    }
                    employeeResponse.employees = listGetEmployee;
                    employeeResponse.totalEmployees = _db.User.ToList().Count();
                    employeeResponse.totalEmployeesFilter = employeeResponse.employees.Count();
                }
                return StatusCode(StatusCodes.Status200OK, employeeResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // GetEmployeeById
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse>> getEmployeeById([FromRoute] Guid id)
        {
            try
            {
                var employee = await _db.User.FindAsync(id);
                if (employee != null)
                {
                    var listGetEmployee = new List<GetEmployeeDTO>();
                    listGetEmployee.Add(new GetEmployeeDTO
                    {
                        id = id,
                        mauser = employee.mauser,
                        name = employee.name,
                        diachi = employee.diachi,
                        ngaysinh = employee.ngaysinh,
                        chucvu = employee.chucvu,
                        cccd = employee.cccd,
                        gioitinh = employee.gioitinh,
                        email = employee.email,
                        sdt = employee.sdt,
                        role = employee.role,
                        createdAt = DateTime.Now,
                        updatedAt = DateTime.Now,
                    });
                    return StatusCode(StatusCodes.Status200OK, listGetEmployee);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // RemoveEmployeeById
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse>> removeEmployeeByID([FromRoute] Guid id)
        {
            try
            {
                var employee = await _db.User.FindAsync(id);
                if (employee != null)
                {
                    _db.User.Remove(employee);
                    await _db.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, "Deleted employee");
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Xoá thất bại!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // AddEmployee
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> addEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            var employeeResponse = new EmployeeResponse();
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
                    cccd = addEmployeeDTO.cccd,
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

                var listGetEmployee = new List<GetEmployeeDTO>();
                    listGetEmployee.Add(new GetEmployeeDTO
                    {
                        id = newEmployee.id,
                        mauser = newEmployee.mauser,
                        name = newEmployee.name,
                        diachi = newEmployee.diachi,
                        ngaysinh = newEmployee.ngaysinh,
                        chucvu = newEmployee.chucvu,
                        cccd = newEmployee.cccd,
                        gioitinh = newEmployee.gioitinh,
                        email = newEmployee.email,
                        sdt = newEmployee.sdt,
                        role = newEmployee.role,
                        createdAt = newEmployee.createdAt,
                        updatedAt = newEmployee.updatedAt,
                    });
                
                employeeResponse.employees = listGetEmployee;
                return StatusCode(StatusCodes.Status200OK, listGetEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        // UpdateEmployeeById
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse>> updateCustomer([FromRoute] Guid id, UpdateEmployeeDTO updateEmployeeDTO)
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
                        cccd=employee.cccd,
                        email = employee.email,
                        sdt = employee.sdt,
                        createdAt = employee.createdAt,
                        updatedAt = employee.updatedAt,
                    };
                    return StatusCode(StatusCodes.Status200OK, getEmployee);
                }
                else
                     return StatusCode(StatusCodes.Status200OK, "Không tìm thấy ID");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
