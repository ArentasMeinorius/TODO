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
        private IConfiguration m_config;

        private TODODBContext dbContext;

        private IPasswordHasher m_hasher { get; set; }

        public LoginController(IConfiguration config, TODODBContext context)
        {
            m_config = config;
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_config[Constants.JwtKey]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var isAdmin = dbContext.Admins.Any(x => x.Id == userId);
            var claims = new[] {
                new Claim(Constants.UserType, (isAdmin ? UserTypes.Admin : UserTypes.User).ToString()),
                new Claim(Constants.UserId, userId.ToString()),
            };

            var token = new JwtSecurityToken(m_config[Constants.JwtIssuer],
              m_config[Constants.JwtIssuer],
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
