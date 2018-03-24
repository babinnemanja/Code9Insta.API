using System;
using System.Collections.Generic;
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
    }
}
