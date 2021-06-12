using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TODO.DBContext;
using TODO.DBSchemas;

namespace TODO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private TODODBContext dbContext;

        public UserController(TODODBContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IList<User> Get()
        {
            return (dbContext.Users.ToList());
        }
    }
}
