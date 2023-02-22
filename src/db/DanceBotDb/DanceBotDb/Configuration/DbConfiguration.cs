using System;
using Microsoft.Extensions.Options;

namespace DanceBotDb.Configuration
{
	public class DbConfiguration : IDbConfiguration
    {
        public static readonly string Configuration = "DbConfiguration";

        public DbConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration["DbConfiguration:ConnectionString"];
            DbName = configuration["DbConfiguration:DbName"];
        }

        public string ConnectionString { get; set; }
        public string DbName { get; set; }
    }
}

