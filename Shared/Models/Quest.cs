using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
        
        [JsonPropertyName("refreshTimeInDays")]
        public int RefreshTimeInDays { get; set; }
        
        [JsonPropertyName("completedDate")]
        public DateTime CompletedDate { get; set; }

        [JsonPropertyName("experienceForCompletion")]
        public int ExperienceForCompletion { get; set; }

        [JsonPropertyName("priority")]
        public QuestPriority Priority { get; set; }

    public Quest(string name, string description, int refreshTimeInDays, bool isCompleted)
		{
			Name = name;
			Description = description;
            RefreshTimeInDays = refreshTimeInDays;
			IsCompleted = isCompleted;
		}
	}
}
