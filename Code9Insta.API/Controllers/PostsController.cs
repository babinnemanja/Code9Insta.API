using System;
using Code9Insta.API.Infrastructure.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Code9Insta.API.Infrastructure.Entities;

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
        [Route("GetAll")]
        public IActionResult GetAllPosts(string searchString)
        {
            var posts = _repository.GetPosts(searchString);


            var result = AutoMapper.Mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(int pageNumber = 1, int pageSize = 10, string searchString = null)
        {
            var posts = _repository.GetPage(pageNumber, pageSize, searchString);

            var result = AutoMapper.Mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(result);
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(Guid id)
        {
            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            
            //ToDo - calculate if post is liked by the current user
            var postDto = AutoMapper.Mapper.Map<PostDto>(post);
            return Ok(postDto);
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
                UserId = model.UserId,
                Tags = model.Tags,
                Description = model.Description                
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.Image.CopyToAsync(memoryStream);
                post.ImageData = memoryStream.ToArray();
            }

            _repository.CreatePost(model.UserId, post);

            if(!_repository.Save())
            {
                return StatusCode(500, "A problem happened with handling your request.");
            }


            return CreatedAtRoute("GetPost",
                new { userId = model.UserId, id = post.Id },
                post);

        }

        [HttpPut("{id}", Name = "EditPost")]
        [Route("{id}")]
        public IActionResult Edit(EditPostViewModel model, Guid id)
        {
            if (model == null)
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

            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {

                _repository.EditPost(post, model.Description, model.Tags);

            }
            if (!_repository.Save())
            {
                throw new Exception($"Updating post likes {id} failed on save.");
            }

            return NoContent();
        }

        // PUT: api/Posts/5
        [HttpPut("{id}/reactToPost", Name ="ReactToPost")]
        [Route("{id}/reactToPost")]
        public IActionResult Put(Guid userId, Guid id)
        {
            var post = _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {

            _repository.ReactToPost(post, userId);

            }
            if (!_repository.Save())
            {
                throw new Exception($"Updating post likes {id} failed on save.");
            }

            return NoContent();
        }

       
        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid userId, Guid id)
        {
            if (!_repository.UserExists(userId))
            {
                return NotFound();
            }

            var post = _repository.GetPostForUser(userId, id);
            if (post == null)
            {
                return NotFound();
            }

            _repository.DeletePost(post);

            if (!_repository.Save())
            {
                throw new Exception($"Deleting post {id} for user {userId} failed on save.");
            }

           
            return NoContent();
        }
    }
}
