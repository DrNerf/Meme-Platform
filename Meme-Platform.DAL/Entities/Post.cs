using System;
using System.Collections.Generic;

namespace Meme_Platform.DAL.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
            Votes = new List<Vote>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Content Content { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Profile Owner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public bool IsNSFW { get; set; }
    }
}