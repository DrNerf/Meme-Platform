namespace Meme_Platform.DAL.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        public virtual Post Post { get; set; }

        public virtual Profile Voter { get; set; }

        public VoteType Type { get; set; }
    }

    public enum VoteType
    {
        Up = 1,
        Down = -1
    }
}