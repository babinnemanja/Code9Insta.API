using System;


namespace Code9Insta.API.Infrastructure.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
