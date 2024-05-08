using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
            
        }

        [Authorize]
        [HttpGet("auth")] // return 401 unathorized exception when user is not authenticating against /auth
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")] // find user that doesnt exist
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);

            if(thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")] // Test generating server error (example - null exception)
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
         
        }

        [HttpGet("bad-request")] // return 400 // test validation error for string le
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}