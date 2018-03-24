using System;
using Code9Insta.API.Infrastructure.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Repository;
using Code9Insta.API.ViewModels;
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
        public string Get(Guid id)
        {
            return "value";
        }
        
        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> Post(CreatePostViewModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }                

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.UserExists(model.UserId))
            {
                return NotFound();
            }

            var post = new PostDto
            {
                UserId = model.UserId
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.Image.CopyToAsync(memoryStream);
                post.ImageData = memoryStream.ToArray();
            }

            _repository.AddPostForUser(model.UserId, post);

            if(!_repository.Save())
            {
                return StatusCode(500, "A problem happened with handling your request.");
            }


            return CreatedAtRoute("GetPostForUser",
                new { userId = model.UserId, id = post.Id },
                post);

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
