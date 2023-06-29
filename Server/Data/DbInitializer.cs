using QuestGiver.Shared.Models;
namespace QuestGiver.Server.Data
{
	public class DbInitializer
	{
		public static void Initialize(QuestGiverDbContext context)
		{
			if (context.Quests.Any())
			{
				return;
			}

			//Create an array of quests using the Quest Model to add to the database

			var quests = new Quest[]
			{
				new Quest("Quest 1", "This is the first quest", new TimeSpan(8, 0, 0), false),
				new Quest("Quest 2", "This is the second quest", new TimeSpan(8, 0, 0), false),
				new Quest("Quest 3", "This is the third quest", new TimeSpan(8, 0, 0), false),
				new Quest("Quest 4", "This is the fourth quest", new TimeSpan(8, 0, 0), false),
				new Quest("Quest 5", "This is the fifth quest", new TimeSpan(8, 0, 0), false),
			};

			var assignees = new Assignee[]
			{
				new Assignee("CJ"),
				new Assignee("Kyle"),
			};
			context.Quests.AddRange(quests);
			context.SaveChanges();
		}
	}
}
