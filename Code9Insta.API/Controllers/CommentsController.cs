using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Code9Insta.API.Helpers;
using System;

namespace Code9Insta.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Comments")]
    public class CommentsController : Controller
    {
        private IDataRepository _repository;

        public CommentsController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CreateComment(Guid postId, string text)
        {
            if (!_repository.PostExists(postId))
                return BadRequest("Post does not exist");

            var userId = Guid.Parse(HttpContext.User.GetUserId());

            var comment = new Comment
            {
                PostId =  postId,
                UserId = userId, // get from context
                CreatedOn = DateTime.Now,
                Text = text
            };

            _repository.CreateComment(comment);

            if(!_repository.Save())
                return StatusCode(500, "There was a problem while handling your request.");

            return StatusCode(200, "Comment created");

        }

        [HttpDelete]
        public IActionResult DeleteComment(Guid commentId)
        {
            var comment = _repository.GetCommentById(commentId);

            if (comment == null)
                return BadRequest("Comment does not exist");

            _repository.DeleteComment(comment);

            if (!_repository.Save())
                return StatusCode(500, "There was a problem while handling your request.");

            return StatusCode(200, "Comment deleted");
        }
    }
}