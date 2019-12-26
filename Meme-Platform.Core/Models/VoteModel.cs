namespace Meme_Platform.Core.Models
{
    public class VoteModel
    {
        public virtual ProfileModel Voter { get; set; }

        public VoteType Type { get; set; }
    }

    public enum VoteType
    {
        Up = 1,
        Down = -1
    }
}