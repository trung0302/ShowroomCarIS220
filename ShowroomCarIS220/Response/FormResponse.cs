using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Response
{
    public class FormResponse<T>
    {
        public int totalForms { get; set; }
        public T? Forms { get; set; }
    }
}
