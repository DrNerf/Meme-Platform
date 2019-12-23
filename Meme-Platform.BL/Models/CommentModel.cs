using System;
using System.Collections.Generic;

namespace Meme_Platform.BL.Models
{
    public class CommentModel
    {
        public string Text { get; set; }

        public DateTime DateTimePosted { get; set; }

        public ProfileModel Owner { get; set; }

        public PostModel PostOwner { get; set; }

        public ICollection<CommentModel> Comments { get; set; }

        public CommentModel Parent { get; set; }
    }
}