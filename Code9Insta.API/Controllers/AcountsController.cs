using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Code9Insta.API.Controllers
{
    [Route("api/[controller]")]
    public class AcountsController : Controller
    {

        public AcountsController()
        {            
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateAccount(string userName, string password)
        {

            return BadRequest("Unable to create account");
        }
    }
}
