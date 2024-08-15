using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

	public enum IntendedAssignee
	{
		Anyone = 0,
		CJ =1,
		Kyle=2,
	}

	public class Quest
	{
		[Key]
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

		[JsonPropertyName("timesCompleted")]
		public int TimesCompleted { get; set; }

		[ForeignKey("QuestLog")]
		[JsonPropertyName("questLogId")]
		public int? QuestLogId { get; set; }

		[JsonPropertyNameAttribute("isAssigned")]
		public bool IsAssigned { get; set; }
		[JsonPropertyNameAttribute("isActive")]
		public bool IsActive { get; set; }

		[JsonPropertyNameAttribute("intendedAssignee")]
		public IntendedAssignee IntendedAssignee { get; set; }

        [JsonPropertyNameAttribute("oneTime")]
		public bool OneTime { get; set; }

        [JsonConstructorAttribute]
		public Quest(string name, string description, int refreshTimeInDays, bool isCompleted, int experienceForCompletion, bool isActive, IntendedAssignee intendedAssignee, bool oneTime = false)
		{
			Name = name;
			Description = description;
			RefreshTimeInDays = refreshTimeInDays;
			IsCompleted = isCompleted;
			ExperienceForCompletion = experienceForCompletion;
			IsActive = isActive;
			IntendedAssignee = intendedAssignee;

            // If you don't specify OneTime, it should be assume this is a new permanent quest.
            OneTime = oneTime;
		}

		public Quest(string name)
		{
			Name = name;
			Description = "Default description";
			RefreshTimeInDays = 1;
			IsCompleted = false;
			ExperienceForCompletion = 0;
			IsActive = true;
			IntendedAssignee = IntendedAssignee.Anyone;

            // If you don't specify OneTime, it should be assume this is a new permanent quest.
            OneTime = false;
		}

		public bool IsOneTime()
		{
			return OneTime;
		}
	}
}
