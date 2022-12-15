using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShowroomCarIS220.Data;

namespace ShowroomCarIS220.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly DataContext _db;
        public CarController(DataContext db)
        {
            _db = db;
        }

    }
}
