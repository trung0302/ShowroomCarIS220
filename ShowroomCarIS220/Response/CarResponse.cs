using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Response
{
    public class CarResponse<T>
    {
        public int totalCars { get; set; }
        public int totalCarsFilter { get; set; }
        public T? cars { get; set; }
    }
}
