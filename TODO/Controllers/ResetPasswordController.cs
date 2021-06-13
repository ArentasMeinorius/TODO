using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Helpers;
using Constants = TODO.Helpers.Constants;

namespace TODO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResetPasswordController : Controller
    {
        private IResetPasswordHelper m_helper;

        public ResetPasswordController(IResetPasswordHelper helper)
        {
            m_helper = helper;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Reset(string email)
        {
            if (m_helper.SendEmail(email))
                return Ok();
            return NotFound(Constants.WrongEmail);
        }
    }
}