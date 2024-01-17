using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models
{
    public class QuestLog
    {
        public int Id { get; set; }
        public List<Quest> Quests { get; set; }
        public QuestLog()
        {
            Quests = new List<Quest>();
        }

    }
}
