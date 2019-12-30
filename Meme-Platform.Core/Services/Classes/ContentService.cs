using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using System.Linq;

namespace Meme_Platform.Core.Services.Classes
{
    internal class ContentService : IContentService
    {
        private readonly IRepository<Content> contentRepository;
        private readonly ITransformer<Content, ContentModel> contentTransformer;

        public ContentService(
            IRepository<Content> contentRepository,
            ITransformer<Content, ContentModel> contentTransformer)
        {
            this.contentRepository = contentRepository;
            this.contentTransformer = contentTransformer;
        }

        public void Dispose()
        {
            contentRepository.Dispose();
        }

        public ContentModel Get(int id)
        {
            return contentTransformer.Transform(
                contentRepository.Get().First(c => c.Id == id));
        }
    }
}
