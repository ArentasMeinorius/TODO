using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TodoTaskController : ControllerBase
    {
        private TODODBContext dbContext;

        public TodoTaskController(TODODBContext context)
        {
            dbContext = context;
        }
        
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userId = GetClaimValue(Constants.UserId);
            var task = dbContext.Tasks.Where(x => x.Id == id).Single();
            if (userId != task.UserId.ToString())
            {
                var userType = GetClaimValue(Constants.UserType);
                if (userType != UserTypes.Admin.ToString())
                {
                    return Forbid();
                }
            }
            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();
            return Ok();
        }
        
        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] TaskInfo task)
        {
            var userId = GetClaimValue(Constants.UserId);
            var realTask = dbContext.Tasks.Where(x => x.Id == task.Id).Single();
            if (userId != realTask.UserId.ToString())
            {
                var userType = GetClaimValue(Constants.UserType);
                if (userType != UserTypes.Admin.ToString())
                {
                    return Forbid ();
                }
            }
            realTask.Name = task.Name ?? realTask.Name;
            realTask.Description = task.Description ?? realTask.Description;
            realTask.Completed = task.Completed;
            dbContext.Tasks.Update(realTask);
            dbContext.SaveChanges();
            return Ok ();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] TaskCreate task)
        {
            var userId = GetClaimValue(Constants.UserId);
            var listId = dbContext.Lists.Where(x => x.UserId.ToString() == userId).Single().Id;
            var newTask = new TodoTask(task.Name, task.Description, task.Completed, listId, int.Parse(userId));
            dbContext.Tasks.Add(newTask);
            dbContext.SaveChanges();
            return Ok ();
        }
        
        private string GetClaimValue(string claimType)
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            return claims.Where(c => c.Type == claimType)
                .Select(c => c.Value).SingleOrDefault();
        }
    }
}
