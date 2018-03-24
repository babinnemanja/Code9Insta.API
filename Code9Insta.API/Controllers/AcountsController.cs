using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Code9Insta.API.Controllers
{
    [Route("api/[controller]")]
    public class AcountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AcountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateAccount(string userName, string password)
        {
            //TODO implement application user from DTO
            _accountRepository.CreateAccount(new ApplicationUser());

            if(!_accountRepository.Save())
            {
                return StatusCode(500, "There was a problem while handling your request.");
            }

            return CreatedAtRoute("", new ApplicationUser());
        }
    }
}
