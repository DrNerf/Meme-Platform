using Meme_Platform.IL.Events;
using Meme_Platform.IL.Web;
using System.Collections.Generic;

namespace Meme_Platform.IL
{
    public interface IPlugin
    {
        string GetName();

        string GetDescription();

        string GetVersion();
    }
}
