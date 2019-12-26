using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.Core.Transformers.Interfaces
{
    public interface ICollectionTransformer<TSource, TDestination>
        : ITransformer<IEnumerable<TSource>, IEnumerable<TDestination>>
    {
    }
}
