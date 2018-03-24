using System;
using Code9Insta.API.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Code9Insta.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private IDataRepository _repository;

        public PostsController(IDataRepository repository)
        {
            _repository = repository;   
        }
        // GET: api/Posts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetPosts());
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
