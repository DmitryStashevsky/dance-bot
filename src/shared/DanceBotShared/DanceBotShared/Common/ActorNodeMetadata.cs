using System;

namespace DanceBotShared.Common
{
	public static class ActorNodeMetadata
	{
		public static string System = "DanceBot";
		public static string Core = $"akka.tcp://{System}@localhost:8081/user/";
		public static string Bot = $"akka.tcp://{System}@localhost:8082/user/";
		public static string Db = $"akka.tcp://{System}@localhost:8083/user/";
    }
}

