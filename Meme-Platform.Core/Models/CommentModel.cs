using System;
using System.Collections.Generic;

namespace Meme_Platform.Core.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTimePosted { get; set; }

        public ProfileModel Owner { get; set; }

        public ICollection<CommentModel> Comments { get; set; }

        public CommentModel Parent { get; set; }
    }
}