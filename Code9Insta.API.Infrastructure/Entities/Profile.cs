﻿using System;
using System.Collections.Generic;
using Code9Insta.API.Infrastructure.Identity;

namespace Code9Insta.API.Infrastructure.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsPublic { get; set; }
        public string Handle { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ApplicationUser User { get; set; }
    }
}