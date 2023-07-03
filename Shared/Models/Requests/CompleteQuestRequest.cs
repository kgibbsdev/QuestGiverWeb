using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Models.Requests
{
    public class CompleteQuestRequest
    {
        public Quest Quest { get; set; }
        public Assignee Assignee { get; set; }

        public CompleteQuestRequest(Quest quest, Assignee assignee)
        {
            Quest = quest;
            Assignee = assignee;
        }
    }


}
