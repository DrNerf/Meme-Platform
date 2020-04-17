using Microsoft.Extensions.Configuration;

namespace Meme_Platform.Core
{
    public class UIConfig
    {
        private readonly IConfiguration configuration;

        public UIConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string SiteName => configuration["UI:Name"];

        public string SiteLogo => configuration["UI:Logo"];

        public string SiteFavicon => configuration["UI:Favicon"];
    }
}
