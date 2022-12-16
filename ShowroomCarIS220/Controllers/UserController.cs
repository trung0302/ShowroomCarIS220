﻿using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;
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
                    createdAt=DateTime.Now,
                    updatedAt=DateTime.Now,
                    role="customer"
                };
                newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
                var token = Generate(newUser.email, newUser.role);
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
            var userAuth = Authenticate(userLogin);

            if (userAuth != null)
            {
                var token = Generate(userAuth.email, userAuth.role);
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
        private string Generate(string email, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
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
                var currentUser = _db.User.FirstOrDefault(u => u.email == userLogin.email);
                
                if (currentUser != null)
                {

                    if (!BCrypt.Net.BCrypt.Verify(userLogin.password, currentUser.password))
                    {
                        return null;
                    }
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
