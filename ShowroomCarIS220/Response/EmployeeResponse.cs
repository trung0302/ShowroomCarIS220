using ShowroomCarIS220.Models;
using ShowroomCarIS220.DTO.Employee;
using ShowroomCarIS220.DTO.User;

namespace ShowroomCarIS220.Response
{
    public class EmployeeResponse
    {
        public int totalEmployees { get; set; }
        public int totalEmployeesFilter { get; set; }
        public List<GetEmployeeDTO>?employees { get; set; }
    }


    public class EmployeeResponseUser
    {
        public GetEmployeeDTO? user { get; set; }
    }
}
