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
				new Quest("Quest 1", "This is the first quest", 1, false, 50, true, IntendedAssignee.Anyone),
				new Quest("Quest 2", "This is the second quest", 1, false, 100, true, IntendedAssignee.Anyone),
				new Quest("Quest 3", "This is the third quest", 1, false, 150, true, IntendedAssignee.Anyone),
				new Quest("Quest 4", "This is the fourth quest", 1, false, 200, true, IntendedAssignee.Anyone),
				new Quest("Quest 5", "This is the fifth quest", 1, false, 250, true, IntendedAssignee.Anyone),
			};

			var assignees = new Assignee[]
			{
				new Assignee("CJ"),
				new Assignee("Kyle"),
			};


			var questLogs = new QuestLog[]
            {
                new QuestLog(),
                new QuestLog(),
            };

			//Don't init quotes in database for now
			//var quotes = new Quote[]{};

			context.Quests.AddRange(quests);
			context.Assignees.AddRange(assignees);
			context.QuestLogs.AddRange(questLogs);
			context.SaveChanges();
		}
	}
}
