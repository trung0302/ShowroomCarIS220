using ShowroomCarIS220.DTO.User;

namespace ShowroomCarIS220.Response
{
    public class UserResponse
    {
        public GetUser? user { get; set; }
        public string? token { get; set; }
    }
}
