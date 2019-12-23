using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meme_Platform.BL.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ContentModel Content { get; set; }

        public DateTime DateCreated { get; set; }

        public ProfileModel Owner { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; } = Enumerable.Empty<CommentModel>();

        public virtual IEnumerable<VoteModel> Votes { get; set; } = Enumerable.Empty<VoteModel>();

        public bool IsNSFW { get; set; }
    }
}
