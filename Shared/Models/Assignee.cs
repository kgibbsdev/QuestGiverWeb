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
	public class Assignee
	{
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("totalExperience")]
        public int TotalExperience { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("questsCompleted")]
        public int QuestsCompleted { get; set; }

        [ForeignKey("QuestLogId")]
        [JsonPropertyName("questLogId")]
        public int? QuestLogId { get; set; }

		[JsonPropertyName("questLog")]
		public QuestLog? QuestLog { get; set; }

        public Assignee(string name)
		{
			Name = name;
		}
	}
}
