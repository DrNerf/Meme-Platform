﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.DAL.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string ProfilePictureUrl { get; set; }

        public string ADIdentifier { get; set; }

        public string Nickname { get; set; }
    }
}
