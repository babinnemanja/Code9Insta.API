﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Code9Insta.API.Core.DTO
{
    public class CreatePostDto
    {
        [Required]
        public IFormFile Image { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }
    }
}