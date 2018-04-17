using System;

namespace Code9Insta.API.Core.DTO
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
