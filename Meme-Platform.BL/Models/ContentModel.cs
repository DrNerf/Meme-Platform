namespace Meme_Platform.BL.Models
{
    public class ContentModel
    {
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