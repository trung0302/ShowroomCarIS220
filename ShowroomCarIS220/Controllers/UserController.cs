using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using ShowroomCarIS220.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using ShowroomCarIS220.Services;
using ShowroomCarIS220.DTO.HoaDon;

namespace ShowroomCarIS220.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;
        private IConfiguration _config;
        private Authentication _auth=new Authentication();
        private ForgotPasswordMailService _sendEmail=new ForgotPasswordMailService();
        public UserController(DataContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        //Get Profile User
        [HttpGet]
        [Route("me")]
        [Authorize()]
        public async Task<ActionResult<UserMeResponse>> GetMyProfile([FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7),_db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var currentUser = GetCurrentUser();
                if(currentUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized,"Please authenticate");
                }
                UserMeResponse userMeResponse = new UserMeResponse();
                userMeResponse.user = currentUser;
                userMeResponse.hoadons = new List<GetInvoice>();
                var listInvoice = _db.HoaDon.Where(i => i.makh == currentUser.mauser).ToList();
                if(listInvoice.Count!= 0)
                {
                    foreach(var item in listInvoice)
                    {
                        userMeResponse.hoadons.Add(new GetInvoice
                        {
                            id = item.id,
                            mahd = item.mahd,
                            makh=item.makh,
                            manv=item.manv,
                            ngayhd=item.ngayhd,
                            tinhtrang=item.tinhtrang,
                            trigia=item.trigia,
                            createdAt=item.createdAt,
                            updatedAt=item.updatedAt,
                        });
                    }    
                }    
                return StatusCode(StatusCodes.Status200OK, userMeResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //Update User
        [HttpPut]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<GetUser>> UpdateUser([FromBody] UpdateUser userUpdate, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken =_auth.CheckTokenLogout(Authorization.Substring(7),_db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var currentUser = GetCurrentUser();

                if (currentUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var user = _db.User.FirstOrDefault(u => u.email == currentUser.email);
                if(user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User not exist");
                }
                if (userUpdate.name == null || userUpdate.name=="")
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Name of User must have a value");

                }
                user.name = userUpdate.name;
                user.gioitinh = userUpdate.gioitinh;
                user.ngaysinh= userUpdate.ngaysinh;
                user.sdt= userUpdate.sdt;
                user.diachi = userUpdate.diachi;
                user.chucvu = userUpdate.chucvu;
                user.updatedAt = DateTime.Now;
                await _db.SaveChangesAsync();
                var responeUser = new GetUser
                {
                    id = user.id,
                    name = user.name,
                    email = user.email,
                    mauser = user.mauser,
                    gioitinh = user.gioitinh,
                    diachi = user.diachi,
                    ngaysinh = user.ngaysinh,
                    sdt = user.sdt,
                    chucvu = user.chucvu,
                    cccd = user.cccd,
                    createdAt = user.createdAt,
                    updatedAt = user.createdAt,
                    role = user.role
                };
                return StatusCode(StatusCodes.Status200OK, responeUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
        //Sign up
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Signup([FromBody] AddUser userAdd)
        {
            try
            {
                var currentUser = _db.User.FirstOrDefault(u => u.email == userAdd.email);
                if(currentUser != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Email existed");
                }
                var idUser = Guid.NewGuid();
                var lastUser = _db.User.OrderByDescending(o=>o.createdAt).FirstOrDefault();
                var maKH = "KH0";
                if(lastUser != null)
                {
                    var numberMaKH = lastUser.mauser.Substring(2);
                    maKH = $"KH{int.Parse(numberMaKH) + 1}";
                }
                var newUser = new User
                {
                    id = idUser,
                    name = userAdd.name,
                    email = userAdd.email,
                    password = userAdd.password,
                    mauser = maKH,
                    gioitinh=userAdd.gioitinh,
                    diachi=userAdd.diachi,
                    ngaysinh=userAdd.ngaysinh,
                    sdt = userAdd.sdt,
                    chucvu=userAdd.chucvu,
                    cccd = userAdd.cccd,
                    createdAt = DateTime.Now,
                    updatedAt=DateTime.Now,
                    role="customer"
                };
                newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
                var token = _auth.GenerateToken(newUser.email, newUser.role, _config);
                var userToken = new Token
                {
                    id = new Guid(),
                    token = token,
                    userId =  idUser
                };
                _db.User.Add(newUser);
                _db.Token.Add(userToken);
                await _db.SaveChangesAsync();
                var userResponse = new UserResponse
                {
                    user = new GetUser
                    {
                        id=newUser.id,
                        name = newUser.name,
                        email = newUser.email,
                        mauser=maKH,
                        gioitinh = newUser.gioitinh,
                        diachi = newUser.diachi,
                        ngaysinh = newUser.ngaysinh,
                        sdt = newUser.sdt,
                        chucvu =newUser.chucvu,
                        cccd = newUser.cccd,
                        createdAt = newUser.createdAt,
                        updatedAt = newUser.createdAt,
                        role = newUser.role
                    },
                    token = token
                };
                return StatusCode(StatusCodes.Status201Created, userResponse);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
        }
        //Login
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserResponse>>  Login([FromBody] UserLogin userLogin)
        {
            var userAuth =_auth.Authenticate(userLogin,_db);
            if (userAuth != null)
            {
                var token =_auth.GenerateToken(userAuth.email, userAuth.role,_config);
                var userResponse = new UserResponse
                {
                    user = userAuth,
                    token = token
                };
                var userToken = new Token
                {
                    id = new Guid(),
                    token = token,
                    userId = userAuth.id,
                };
                _db.Token.Add(userToken);
                await _db.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, userResponse);
            }

            return StatusCode(StatusCodes.Status400BadRequest,"Error Email Or Password");
        }
        //Logout
        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<ActionResult<GetUser>> Logout([FromHeader] string Authorization)
        {
            try
            {
                var checkToken =_auth.CheckTokenLogout(Authorization.Substring(7),_db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var currentUser = GetCurrentUser();
                

                _db.Token.Remove(checkToken);
                await _db.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK,"Ok");

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //ForgetPassword
        [AllowAnonymous]
        [HttpPost]
        [Route("forgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] Email emailPW)
        {
            try
            {
                var user = _db.User.FirstOrDefault(i => i.email == emailPW.email);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Email not exists");
                }
                var token=_auth.GenerateTokenRSPW(user.email, user.role,_config);
                
                bool sendEmailSucces=_sendEmail.SendEmail(emailPW.email,token);
                if (!sendEmailSucces)
                {
                    return StatusCode(StatusCodes.Status200OK, "Faily send email");
                }
                user.verifyToken = token;
                await _db.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Successly send email");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //ResetPassword
        [AllowAnonymous]
        [HttpPost]
        [Route("resetPassword")]
        public async Task<ActionResult<GetUser>> ResetPassword([FromBody] Password passwordReset, [FromHeader] string Authorization)
        {
            try
            {
                var resetToken =Authorization.Substring(7);
                var user=_db.User.FirstOrDefault(i=>i.verifyToken==resetToken);
                if(user==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User not exits or bad token");
                }
                user.password = BCrypt.Net.BCrypt.HashPassword(passwordReset.password);
                user.verifyToken = "";
                await _db.SaveChangesAsync();
                var resetPwUser = new GetUser
                {
                    id = user.id,
                    name = user.name,
                    email = user.email,
                    gioitinh = user.gioitinh,
                    sdt = user.sdt,
                    chucvu = user.chucvu,
                    diachi = user.diachi,
                    cccd = user.cccd,
                    role=user.role,
                    createdAt=user.createdAt,
                    updatedAt=user.updatedAt,

                };
                return StatusCode(StatusCodes.Status200OK, resetPwUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        private GetUser? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var emailUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
                var currentUser=_db.User.SingleOrDefault(u=>u.email==emailUser);
                if(currentUser == null)
                {
                    return null;
                }
                return new GetUser
                {
                    id = currentUser.id,
                    name = currentUser.name,
                    email = currentUser.email,
                    role = currentUser.role,
                    mauser = currentUser.mauser,
                    gioitinh = currentUser.gioitinh,
                    ngaysinh = currentUser.ngaysinh,
                    sdt = currentUser.sdt,
                    diachi = currentUser.diachi,
                    chucvu = currentUser.chucvu,
                    createdAt = currentUser.createdAt,
                    updatedAt = currentUser.updatedAt,

                };
            }
            return null;
        }
    }
}
