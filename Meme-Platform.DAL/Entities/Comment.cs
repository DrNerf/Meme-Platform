using System;
using System.Collections.Generic;

namespace Meme_Platform.DAL.Entities
{
    public class Comment
    {
        public Comment()
        {
            Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTimePosted { get; set; }

        public virtual Profile Owner { get; set; }

        public virtual Post PostOwner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Comment Parent { get; set; }
    }
}