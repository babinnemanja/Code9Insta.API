using System;
using System.Collections.Generic;
using System.Linq;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Interfaces;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class DataRepository : IDataRepository
    {
        private CodeNineDbContext _context;

        public DataRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public void AddPostForUser(Guid userId, PostDto post)
        {
            var image = new Image
            {
                Data = post.ImageData
            };

            var newPost = new Post
            {          
                Image = image,
                UserId = post.UserId,
                CreatedOn = DateTime.UtcNow,
                Description = post.Description,
                PostTags = new List<PostTag>(),
                
            };

            foreach (var tag in post.Tags)
            {
                var newTag = new Tag
                {
                    Id = post.Id,
                    Text = tag
                };

                _context.Tags.Add(newTag);

                newPost.PostTags.Add(new PostTag
                {
                    Post = newPost,
                    Tag = newTag
                });

                
            }

            _context.Posts.Add(newPost);

                    
        }

        public Post GetPostForUser(Guid userId, Guid id)
        {
            return _context.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId);
        }

        public void DeletePost(Post post)
        {
            _context.Remove(post);
        }

        public IEnumerable<Post> GetPosts()
        {
            return _context.Posts.AsEnumerable();
        }

        public bool UserExists(Guid userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool PostExists(Guid postId)
        {
            return _context.Posts.Any(u => u.Id == postId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

       
    }
}
