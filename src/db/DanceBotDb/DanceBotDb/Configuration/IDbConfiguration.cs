using System;
namespace DanceBotDb.Configuration
{
	public interface IDbConfiguration
    {
        public string ConnectionString { get; }
        public string DbName { get; }
    }
}

