using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
	public class Assignee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? CurrentQuestId { get; set; }
		public Quest? CurrentQuest { get; set; }

		public Assignee(string name)
		{
			Name = name;
		}
	}
}
