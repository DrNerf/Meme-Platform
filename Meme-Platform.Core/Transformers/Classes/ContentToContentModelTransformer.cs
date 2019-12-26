using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class ContentToContentModelTransformer : ITransformer<Content, ContentModel>
    {
        public ContentModel Transform(Content source)
        {
            return new ContentModel
            {
                Data = source.Data,
                Extension = source.Extension,
                ContentType = convertContentType(source.ContentType)
            };
        }

        private Models.ContentType convertContentType(DAL.Entities.ContentType contentType)
        {
            switch (contentType)
            {
                case DAL.Entities.ContentType.Image:
                    return Models.ContentType.Image;
                case DAL.Entities.ContentType.Youtube:
                    return Models.ContentType.Youtube;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contentType), "Ey yo WTF!");
            }
        }
    }
}
