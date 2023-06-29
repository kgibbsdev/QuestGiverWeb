using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
	public class Quest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsCompleted { get; set; }
		public TimeSpan RefreshTime { get; set; }

    public Quest(string name, string description, TimeSpan refreshTime, bool isCompleted)
		{
			Name = name;
			Description = description;
			RefreshTime = refreshTime;
			IsCompleted = isCompleted;
		}
	}
}
