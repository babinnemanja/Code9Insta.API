using System;
using System.Collections.Generic;

namespace Code9Insta.API.Core.DTO
{
    public class PostDto
    {
        public Guid Id { get; set; }      
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Likes { get; set; }
        public byte[] ImageData { get; set; }

        public ICollection<CommentDto> Comments { get; set; }
      
    }
}
