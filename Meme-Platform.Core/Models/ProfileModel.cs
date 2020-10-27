namespace Meme_Platform.Core.Models
{
    public class ProfileModel
    {
        public string ProfilePictureUrl { get; set; }

        public string ADIdentifier { get; set; }

        public string Nickname { get; set; }

        public int PostsCount { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }

        public int MaxScore { get; set; }
    }
}