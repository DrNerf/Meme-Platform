using Meme_Platform.Core.Models;
using System.Collections.Generic;

namespace Meme_Platform.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProfileModel> TopContributors { get; set; }

        public IEnumerable<PostModel> TopPosts { get; set; }

        public PostModel PostOfTheDay { get; set; }
    }
}
