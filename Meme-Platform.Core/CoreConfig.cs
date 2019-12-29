using Microsoft.Extensions.Configuration;

namespace Meme_Platform.Core
{
    internal class CoreConfig
    {
        public CoreConfig(IConfiguration configuration)
        {
            PageSize = int.Parse(configuration["Core:PageSize"]);
            TopStats = int.Parse(configuration["Core:TopStats"]);
        }

        public int PageSize { get; }
        public int TopStats { get; }
    }
}
