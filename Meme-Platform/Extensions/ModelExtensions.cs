using Meme_Platform.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Extensions
{
    public static class ModelExtensions
    {
        public static string GetImageUrl(this ContentModel model)
        {
            return $"/image/{model.Id}{model.Extension}";
        }
    }
}
