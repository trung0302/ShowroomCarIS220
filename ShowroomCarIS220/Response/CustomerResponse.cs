using ShowroomCarIS220.DTO.Customer;

namespace ShowroomCarIS220.Response
{
    public class CustomerResponse
    {
        public int totalCustomers { get; set; }
        public int totalCustomersFilter { get; set; }
        public List<GetCustomerDTO> customers { get; set; }
    }
}
