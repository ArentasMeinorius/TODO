using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TODO.DBContext;
using TODO.DBSchemas;
using TODO.Helpers;
using Constants = TODO.Helpers.Constants;

namespace TODO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TODODBContext dbContext;

        public TodoListController(TODODBContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Authorize]
        public IList<TodoList> Get()
        {
            var userType = GetClaimValue(Constants.UserType);
            List<TodoList> list;
            if (userType == UserTypes.Admin.ToString())
            {
                list = dbContext.Lists.ToList();
            }
            else
            {
                var userId = GetClaimValue(Constants.UserId);
                list = dbContext.Lists.Where(x => x.Id.ToString() == userId).ToList();
            }
            foreach (var item in list)
                item.Tasks = dbContext.Tasks.Where(x => x.ListId == item.Id).ToList();
            return list;
        }

        private string GetClaimValue(string claimType)
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            return claims.Where(c => c.Type == claimType)
                .Select(c => c.Value).SingleOrDefault();
        }
    }
}