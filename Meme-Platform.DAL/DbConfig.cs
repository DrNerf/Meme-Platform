using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.DAL
{
    internal class DbConfig
    {
        public DbConfig(IConfiguration configuration)
        {
            ConnectionString = configuration["Db:ConnectionString"];
        }

        public string ConnectionString { get; }
    }
}
