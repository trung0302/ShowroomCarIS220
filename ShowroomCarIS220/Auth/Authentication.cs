using Microsoft.IdentityModel.Tokens;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace ShowroomCarIS220.Auth
{
    public class Authentication
    {
        public string GenerateToken(string email, string role, IConfiguration _config)
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

        public string GenerateTokenRSPW(string email, string role, IConfiguration _config)
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
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public GetUser? Authenticate(UserLogin userLogin, DataContext _db)
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
                        sdt = currentUser.sdt,
                        diachi = currentUser.diachi,
                        chucvu = currentUser.chucvu,
                        cccd = currentUser.chucvu,
                        createdAt = currentUser.createdAt,
                        updatedAt = currentUser.updatedAt,

                    };
                    return getUser;
                }

                return null;
            }
            catch
            {
                return null;
            }

        }
        public Token? CheckTokenLogout(string token, DataContext _db)
        {
            var tokenUser = _db.Token.SingleOrDefault(t => t.token == token);

            if (tokenUser == null)
            {
                return null;
            }
            return tokenUser;
        }
    }
}
