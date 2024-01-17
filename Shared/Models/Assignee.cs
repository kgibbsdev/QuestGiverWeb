using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
	public class Assignee
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("currentQuestId")]
        public int? CurrentQuestId { get; set; }
        
        [JsonPropertyName("currentQuest")]
        public Quest? CurrentQuest { get; set; }

        [JsonPropertyName("totalExperience")]
        public int TotalExperience { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("questsCompleted")]
        public int QuestsCompleted { get; set; }
        public int? QuestLogId { get; set; }
        public QuestLog? QuestLog { get; set; }

        public Assignee(string name)
		{
			Name = name;
		}
	}
}
