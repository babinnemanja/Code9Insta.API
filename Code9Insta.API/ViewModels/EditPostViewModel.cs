using System;


namespace Code9Insta.API.ViewModels
{
    public class EditPostViewModel
    {     
        public Guid UserId { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }
    }
}
