using Meme_Platform.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meme_Platform.Extensions
{
    public static class ModelExtensions
    {
        private const string YouTubeAddressRegex = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";

        public static string GetImageUrl(this ContentModel model)
        {
            return $"/image/{model.Id}{model.Extension}";
        }

        public static string GetYTVideoId(this ContentModel model)
        {
            var url = Encoding.UTF8.GetString(model.Data);
            return new Regex(YouTubeAddressRegex).Match(url).Groups[1].Value;
        }

        public static string GetYoutubeThumbnail(this ContentModel model)
        {
            var url = Encoding.UTF8.GetString(model.Data);
            var match = Regex.Match(url, YouTubeAddressRegex);
            return $"http://i3.ytimg.com/vi/{match.Groups[1].Value}/hqdefault.jpg";
        }
    }
}
