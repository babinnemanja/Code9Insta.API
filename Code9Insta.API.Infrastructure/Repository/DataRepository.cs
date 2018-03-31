using System;
using System.Collections.Generic;
using System.Linq;
using Code9Insta.API.Core.DTO;
using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                    Text = tag
                };
           
                newPost.PostTags.Add(new PostTag
                {
                    Post = newPost,
                    Tag = newTag
                });
                
            }

            _context.Posts.Add(newPost);

                    
        }

        public void EditPost(Post post, string description, string[] tags)
        {
            post.Description = description;

            //clear removed tags
            var tagsForRemoval = post.PostTags.Where(pt => tags.All(x => x != pt.Tag.Text)).ToList();

            foreach (var item in tagsForRemoval)
            {
                post.PostTags.Remove(item);
            }

            //add new tags
            foreach (var tag in tags)
            {
                if(!post.PostTags.Any(t => t.Tag.Text == tag))
                {
                    var newTag = new Tag
                    {                       
                        Text = tag
                    };

                    post.PostTags.Add(new PostTag
                    {
                        Post = post,
                        Tag = newTag
                    });
                }
            }
        }

        public Post GetPostForUser(Guid userId, Guid id)
        {
            return _context.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId);
        }

        public Post GetPostById(Guid id)
        {
            return _context.Posts
                .Include(p => p.Image)
                .Include(p => p.UserLikes)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(e => e.PostTags)
                    .ThenInclude(e => e.Tag)
                .SingleOrDefault(p => p.Id == id);
        }

        public void DeletePost(Post post)
        {
            _context.Remove(post);
        }

        public void ReactToPost(Post post, Guid userId)
        {
            var like = _context.UserLikes.SingleOrDefault(pl => pl.UserId == userId && pl.PostId == post.Id);
            if (like == null)
            {
                var newLike = new UserLike
                {
                    PostId = post.Id,
                    UserId = userId
                };
               
                post.UserLikes = post.UserLikes ?? new List<UserLike>();

                post.UserLikes.Add(newLike);
                
            }
            else
            {
                _context.UserLikes.Remove(like);
            }
        }

        public IEnumerable<Post> GetPosts()
        {
            var posts = _context.Posts
                .Include(p => p.Image)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(e => e.PostTags)
                    .ThenInclude(e => e.Tag)
                    .ToList();
            return posts;
        }

        public IEnumerable<Post> GetPage(int pageNumber, int pageSize)
        {
            var posts = _context.Posts
               .Include(p => p.Image)
               .Include(p => p.User)
               .Include(p => p.Comments)
               .Include(e => e.PostTags)
                   .ThenInclude(e => e.Tag)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();

            return posts;
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
