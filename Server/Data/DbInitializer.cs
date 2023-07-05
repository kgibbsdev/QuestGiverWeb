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
				new Quest("Quest 1", "This is the first quest", 1, false, 50),
				new Quest("Quest 2", "This is the second quest", 1, false, 100),
				new Quest("Quest 3", "This is the third quest", 1, false, 150),
				new Quest("Quest 4", "This is the fourth quest", 1, false, 200),
				new Quest("Quest 5", "This is the fifth quest", 1, false, 250),
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
