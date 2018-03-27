using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IDataRepository
    {
        IEnumerable<Post> GetPosts();
        bool UserExists(Guid userId);
        bool PostExists(Guid userId);
        void AddPostForUser(Guid userId, PostDto post);
        void DeletePost(Post postId);

        bool Save();
        Post GetPostForUser(Guid userId, Guid id);
    }
}
