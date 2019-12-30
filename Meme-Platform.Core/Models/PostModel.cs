using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meme_Platform.Core.Models
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

        public int CalculateScore()
        {
            return Votes?.Select(v => v.Type).Cast<int>().Sum() ?? 0;
        }

        public int CountComments()
        {
            var count = Comments.Count();
            count += Comments.Where(c => c.Comments.Any())
                .SelectMany(c => c.Comments).Count();
            return count;
        }

        public bool AnyNewComments()
        {
            var anyNewComments = Comments.Any(c => c.DateTimePosted.Date == DateTime.Now.Date);
            var anyNewRepliesComments = Comments.Where(c => c.Comments.Any())
                .SelectMany(c => c.Comments)
                .Any(c => c.DateTimePosted.Date == DateTime.Now.Date);

            return anyNewComments || anyNewRepliesComments;
        }
    }
}
