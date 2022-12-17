using ShowroomCarIS220.Models;
namespace ShowroomCarIS220.Response
{
    public class EmployeeResponse<T>
    {
        public int totalEmployees { get; set; }
        public int totalEmployeesFilter { get; set; }
        public T? employees { get; set; }
    }
}
