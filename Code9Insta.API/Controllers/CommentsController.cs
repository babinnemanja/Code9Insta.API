using Code9Insta.API.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Code9Insta.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Comments")]
    public class CommentsController : Controller
    {
        private IDataRepository _repository;

        public CommentsController(IDataRepository repository)
        {
            _repository = repository;
        }
    }
}