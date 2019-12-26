using Meme_Platform.Core.Transformers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class GenericCollectionTransformer<TSource, TDestination>
        : ICollectionTransformer<TSource, TDestination>
    {
        private readonly ITransformer<TSource, TDestination> transformer;

        public GenericCollectionTransformer(ITransformer<TSource, TDestination> transformer)
        {
            this.transformer = transformer;
        }

        public IEnumerable<TDestination> Transform(IEnumerable<TSource> source)
        {
            return source.Select(s => transformer.Transform(s));
        }
    }
}
