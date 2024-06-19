using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
    public class QuestLog
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
		[JsonPropertyName("activeQuest")]
		public Quest? ActiveQuest { get { return (this.IsEmpty() ? null : Quests[0]); } }
        [JsonPropertyName("quests")]
        public List<Quest> Quests { get; set; }
        [JsonPropertyName("questsCompleted")]
        public int QuestsCompleted { get; set; }
        public QuestLog()
        {
            Quests = new List<Quest>();
        }

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
        }

        public bool IsEmpty()
        {
            return Quests.Count == 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Quest Log:\n");
            sb.Append("Quests Completed: " + QuestsCompleted + "\n");
            sb.Append("Active Quest: " + (ActiveQuest == null ? "None" : ActiveQuest.Name) + "\n");
            sb.Append("Quests:\n");
            if(Quests == null)
            {
                sb.Append("Quests property was null\n");
            }
            else
			{
				foreach (Quest quest in Quests)
				{
					sb.Append(quest.Name + "\n");
				}
			}
            return sb.ToString();
        }

    }
}
