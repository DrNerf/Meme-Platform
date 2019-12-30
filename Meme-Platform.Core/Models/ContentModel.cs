namespace Meme_Platform.Core.Models
{
    public class ContentModel
    {
        public int Id { get; set; }

        public ContentType ContentType { get; set; }

        public byte[] Data { get; set; }

        public string Extension { get; set; }
    }

    public enum ContentType
    {
        Image = 0,
        Youtube = 1
    }
}