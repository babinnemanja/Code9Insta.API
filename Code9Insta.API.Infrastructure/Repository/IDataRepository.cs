using Code9Insta.API.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code9Insta.API.Infrastructure.Repository
{
    public interface IDataRepository
    {
        IEnumerable<Post> GetPosts();
    }
}
