using System.ComponentModel.DataAnnotations.Schema;

namespace ShowroomCarIS220.Models
{
    public class Token
    {
        public Guid id { get; set; }
        public string? token { get; set; }
        [ForeignKey("User")]
        public Guid? userId { get; set; }
    }
}
