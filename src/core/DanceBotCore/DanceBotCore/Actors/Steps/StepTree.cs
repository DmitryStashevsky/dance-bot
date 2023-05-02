using System;
namespace DanceBotCore.Actors.Steps
{
	public static class StepTree
	{
		static StepTree()
		{
		}
    }

	public class StepNode
	{
		public string Step { get; set; }
		public Action[] Actions { get; set; }
    }
}

