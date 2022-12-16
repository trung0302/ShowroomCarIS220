using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Response
{
    public class FormResponse<T>
    {
        public int totalForms { get; set; }
        public int totalFormsFilter { get; set; }
        public T? Form { get; set; }
    }
}
