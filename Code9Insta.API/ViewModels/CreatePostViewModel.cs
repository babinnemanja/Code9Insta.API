using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Code9Insta.API.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
        public Guid UserId { get; set; }
    }
}
