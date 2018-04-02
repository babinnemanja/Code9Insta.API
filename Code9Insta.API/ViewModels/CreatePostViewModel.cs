using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Code9Insta.API.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }
    }
}
