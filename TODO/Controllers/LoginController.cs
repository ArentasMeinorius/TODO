using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Helpers;

namespace TODO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private ILoginHelper m_loginHelper;
        public LoginController(ILoginHelper loginHelper)
        {
            m_loginHelper = loginHelper;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo login)
        {
            var tokenString = m_loginHelper.GetToken(login);
            if (tokenString != null)
                return Ok(new { token = tokenString });
            return Unauthorized(Constants.WrongCredentials);
        }
    }
}
