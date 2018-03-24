using System;
using System.Collections.Generic;
using System.Linq;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;

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
          
           
        }

        public IEnumerable<Post> GetPosts()
        {
            return new List<Post>
            {
                new Post
                {
                    Id = Guid.NewGuid(),
                    Likes = 3,
                    UserId = Guid.NewGuid()
                },
                 new Post
                {
                    Id = Guid.NewGuid(),
                    Likes = 3,
                    UserId = Guid.NewGuid()
                }
            };
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
