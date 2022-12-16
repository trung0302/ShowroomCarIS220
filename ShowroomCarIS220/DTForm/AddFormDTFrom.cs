using System.ComponentModel.DataAnnotations;
namespace ShowroomCarIS220.DTForm
{
    public class AddFormDTFrom
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string email { get; set; }
        public string message { get; set; }
    }
}
