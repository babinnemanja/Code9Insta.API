using System;
using System.Collections.Generic;
using Code9Insta.API.Infrastructure.Identity;


namespace Code9Insta.API.Infrastructure.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<UserLike> UserLikes { get; set; }

        public ApplicationUser User { get; set; }
        public Image Image { get; set; }
    }

}
