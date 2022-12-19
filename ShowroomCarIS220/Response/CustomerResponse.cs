namespace ShowroomCarIS220.Response
{
    public class CustomerResponse<T>
    {
        public int totalCustomers { get; set; }
        public int totalCustomersFilter { get; set; }
        public T? customer { get; set; }
    }
}
