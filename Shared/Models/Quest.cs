using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuestGiver.Shared.JsonConverters;

namespace QuestGiver.Shared.Models
{
	public enum QuestPriority
    {
        High=1,
        Medium=2,
        Low=3
    }

	public class Quest
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
        [JsonConverter(typeof(TimeSpanConverter))]
        [JsonPropertyName("refreshTime")]
        public TimeSpan RefreshTime { get; set; }
        [JsonPropertyName("completedDate")]
        public DateTime CompletedDate { get; set; }
        [JsonPropertyName("experienceForCompletion")]
        public int ExperienceForCompletion { get; set; }
        [JsonConverter(typeof(QuestPriorityConverter))]
        [JsonPropertyName("priority")]
        public QuestPriority Priority { get; set; }

    public Quest(string name, string description, TimeSpan refreshTime, bool isCompleted)
		{
			Name = name;
			Description = description;
			RefreshTime = refreshTime;
			IsCompleted = isCompleted;
		}
	}
}
