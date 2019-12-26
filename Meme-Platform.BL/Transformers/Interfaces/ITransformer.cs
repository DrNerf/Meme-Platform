using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.Core.Transformers.Interfaces
{
    public interface ITransformer<TSource, TDestination>
    {
        TDestination Transform(TSource source);
    }
}
