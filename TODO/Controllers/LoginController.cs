using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TODO.DBContext;
using TODO.Helpers;
using Constants = TODO.Helpers.Constants;

namespace TODO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        private TODODBContext dbContext;

        private IPasswordHasher m_hasher { get; set; }

        public LoginController(IConfiguration config, TODODBContext context)
        {
            _config = config;
            dbContext = context;
            m_hasher = new PasswordHasher();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo login)
        {
            
            IActionResult response = Unauthorized();
            var userId = AuthenticateUser(login);

            if (userId != null)
            {
                var tokenString = GenerateJSONWebToken((int)userId);
                response = Ok(new { token = tokenString });
            }
            
            return response;
        }

        private string GenerateJSONWebToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var isAdmin = dbContext.Admins.Any(x => x.Id == userId);
            var claims = new[] {
                new Claim(Constants.UserType, isAdmin ? "Admin" : "User"),
                new Claim(Constants.UserId, userId.ToString()),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int? AuthenticateUser(LoginInfo login)
        {
            var user = dbContext.Users.Where(x => x.UserName == login.UserName).SingleOrDefault();
            var result = m_hasher.VerifyHashedPassword(user?.PassWord, login.PassWord);
            if (result != PasswordVerificationResult.Failed)
            {
                return user.Id;
            }
            return null;
        }
    }
}
