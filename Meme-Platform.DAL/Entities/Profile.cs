﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.DAL.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string ADIdentifier { get; set; }
    }
}
