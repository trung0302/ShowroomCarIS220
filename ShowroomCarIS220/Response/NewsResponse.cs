namespace ShowroomCarIS220.Response
{
    public class NewsResponse<T>
    {
        public int totalNews { get; set; }
        public T? News { get; set; }
    }
}
