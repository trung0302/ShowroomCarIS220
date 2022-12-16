namespace ShowroomCarIS220.Response
{
    public class InvoiceResponse<T>
    {
        public int totalHoaDon { get; set; }
        public T? hoadons { get; set; }
    }
}
