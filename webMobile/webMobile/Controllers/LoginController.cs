using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webMobile.Models;
using webMobile.ModelView;
using BC = BCrypt.Net.BCrypt;


namespace webMobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly string _appsetting;
        public LoginController(MyDbContext db, IConfiguration configuration)
        {
            _db = db;
            _appsetting = configuration["Appsettings:SecretKey"];
        }
        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {

            var user = _db.Users.SingleOrDefault(tbl => tbl.UserName == model.UserName);
            if(user== null)
            {
                return BadRequest(new ApiRespone
                {
                    Success = false,
                    Message = "Invalid UserName/Password"
                });
            }
            if (BC.Verify(model.PassWord, user.Password) == false)
            {
                return BadRequest(new ApiRespone
                {
                    Success = false,
                    Message = "Invalid Password"
                });
            }
            //cấp token
            var token = GenerateToken(user);
            return Ok(new ApiRespone
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = token

            });
        }
        private string GenerateToken(UsersRecord record)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretketByte = Encoding.UTF8.GetBytes(_appsetting);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, record.FullName),
                    new Claim(ClaimTypes.Email, record.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", record.UserName),
                    new Claim("Id", record.ID.ToString())
                }),
                Expires=DateTime.UtcNow.AddSeconds(30),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(secretketByte), SecurityAlgorithms.HmacSha256Signature)
            };
            //tao token
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            //ghi token
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}
