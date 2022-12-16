using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShowroomCarIS220.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;
        private IConfiguration _config;
        public UserController(DataContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        //Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserResponse>>  Login([FromBody] UserLogin userLogin)
        {
            var userAuth = Authenticate(userLogin);

            if (userAuth != null)
            {
                var token = Generate(userAuth);
                var userResponse = new UserResponse
                {
                    user = userAuth,
                    token = token
                };
                return StatusCode(StatusCodes.Status200OK, userResponse);
            }

            return StatusCode(StatusCodes.Status400BadRequest,"");
        }
        private string Generate(GetUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(720),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private  GetUser? Authenticate(UserLogin userLogin)
        {
            try
            {
                var currentUser = _db.User.FirstOrDefault(u => u.email == userLogin.email && u.password==userLogin.password);

                
                if (currentUser != null)
                {
                    var getUser = new GetUser
                    {
                        id = currentUser.id,
                        name = currentUser.name,
                        email = currentUser.email,
                        role = currentUser.role,
                        mauser = currentUser.mauser,
                        gioitinh = currentUser.gioitinh,
                        ngaysinh = currentUser.ngaysinh,
                        sdt=currentUser.sdt,
                        diachi = currentUser.diachi,
                        chucvu = currentUser.chucvu,
                        createdAt = currentUser.createdAt,
                        updatedAt = currentUser.updatedAt,

                    };
                        return getUser;
                }

                return null ;
            }
            catch
            {
                return null;
            }
           
        }
    }
}
