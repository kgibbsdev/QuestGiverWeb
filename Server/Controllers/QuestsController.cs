using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestGiver.Server.Data;
using QuestGiver.Shared.Models;
using QuestGiver.Shared.Models.Requests;

namespace QuestGiver.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly QuestGiverDbContext _context;

        public QuestsController(QuestGiverDbContext context)
        {
            _context = context;
        }

        // GET: api/Quests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetQuests()
        {
          if (_context.Quests == null)
          {
              return NotFound();
          }
            return await _context.Quests.ToListAsync();
        }

        // GET: api/Quests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetQuest(int id)
        {
          if (_context.Quests == null)
          {
              return NotFound();
          }
            var quest = await _context.Quests.FindAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            return quest;
        }

        // PUT: api/Quests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuest(int id, Quest quest)
        {
            if (id != quest.Id)
            {
                return BadRequest();
            }

            var existingQuest = await _context.Quests.FindAsync(id);
            existingQuest.CompletedDate = quest.CompletedDate;
            existingQuest.IsCompleted = quest.IsCompleted;
            existingQuest.Name = quest.Name;
            existingQuest.Priority = quest.Priority;    
            existingQuest.ExperienceForCompletion = quest.ExperienceForCompletion;
            existingQuest.Description = quest.Description;
            existingQuest.RefreshTimeInDays = quest.RefreshTimeInDays;

            _context.Quests.Update(existingQuest);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quest>> PostQuest(Quest quest)
        {
          if (_context.Quests == null)
          {
              return Problem("Entity set 'QuestGiverDbContext.Quests'  is null.");
          }
            _context.Quests.Add(quest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuest", new { id = quest.Id }, quest);
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(int id)
        {
            if (_context.Quests == null)
            {
                return NotFound();
            }
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestExists(int id)
        {
            return (_context.Quests?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: api/Quests/Complete
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteQuest([FromBody] CompleteQuestRequest request)
        {
            //Do we need to get the quest and assignee from the database?
            var existingQuest = await _context.Quests.FindAsync(request.Quest.Id);
            existingQuest.CompletedDate = request.Quest.CompletedDate;
            existingQuest.IsCompleted = request.Quest.IsCompleted;
            existingQuest.TimesCompleted += 1;

            var existingAssignee = await _context.Assignees.FindAsync(request.Assignee.Id);
            existingAssignee.CurrentQuestId = null;
            existingAssignee.TotalExperience += existingQuest.ExperienceForCompletion;
            existingAssignee.QuestsCompleted += 1;

            _context.Quests.Update(existingQuest);
            _context.Assignees.Update(existingAssignee);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quests/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignNewQuest([FromBody] Assignee assignee)
        {
            var quests = _context.Quests.Where(q => q.IsCompleted == false).ToList();
            
            //Cannot track more than one instance of the same assignee
            var assignees = _context.Assignees.Where(a => a.Name != assignee.Name).ToList();

            var assignableQuests = new List<Quest>();

            //if a quest is already assigned to someone, don't include it in the assignable quests
            var unassignedQuests = quests.Where(q => assignees.All(a => a.CurrentQuestId != q.Id)).ToList();
            
            if(unassignedQuests.Any(q => q.Priority == QuestPriority.High))
            {
                assignableQuests = quests.Where(q => q.Priority == QuestPriority.High).ToList();
            }

            if(unassignedQuests.Any(q => q.Priority == QuestPriority.Medium) && assignableQuests.IsNullOrEmpty())
            {
                assignableQuests = quests.Where(q => q.Priority == QuestPriority.Medium).ToList();
            }

            if(unassignedQuests.Any(q => q.Priority == QuestPriority.Low) && assignableQuests.IsNullOrEmpty())
            {
                assignableQuests = quests.Where(q => q.Priority == QuestPriority.Low).ToList();
            }

            //pick a random quest from the assignable quests
            var random = new Random();
            var randomQuest = assignableQuests[random.Next(0, assignableQuests.Count)];

            //assign the quest to the assignee
            assignee.CurrentQuestId = randomQuest.Id;
            assignee.CurrentQuest = randomQuest;

            _context.Assignees.Update(assignee);

            await _context.SaveChangesAsync();

            //TODO: Need to handle the case where there are no quests to assign
            return Ok(assignee);
        }

        // POST: api/AssignThisQuestToMe/kyle
        [Route("AssignThisQuestToMe/{name}")]
        [HttpPost]
        public async Task<IActionResult> AssignThisQuestToMe([FromBody] Quest quest, [FromRoute]string name)
        {
            Assignee assignee = await _context.Assignees.FirstOrDefaultAsync(a => a.Name == name);
            if (assignee != null) {
                assignee.CurrentQuestId = quest.Id;
                _context.Assignees.Update(assignee);
                await _context.SaveChangesAsync();
                return Ok(assignee);
            }
            else 
            {
                Console.WriteLine($"User with the name {name} was not found!");
                return NotFound($"User with the name {name} was not found!");
            }
        }

        // POST: api/Quests/Reset
        [HttpPost("reset")]
        public async Task<IActionResult> ResetQuests(bool hardReset)
        {

#if !DEBUG
            return NotFound();
#endif

            var quests = await _context.Quests.ToListAsync();
            var assignees = await _context.Assignees.ToListAsync();

            foreach(var quest in quests)
            {
                quest.IsCompleted = false;
                quest.CompletedDate = default;
                quest.TimesCompleted = 0;
            }

            _context.Quests.UpdateRange(quests);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Quests/New
        [HttpPost("new")]
        public async Task<IActionResult> NewQuest([FromBody] Quest quest)
        {
            _context.Quests.Add(quest);

            await _context.SaveChangesAsync();

            return Ok(quest);
        }

        public void StartupJobs()
        {
            var quests = _context.Quests.ToList();

           //For each completed quest, check if it needs to be reset
           foreach(var quest in quests.Where(q => q.IsCompleted))
            {
                var daysSinceCompletion = DateTime.Now.Subtract(quest.CompletedDate).Days;
                if(daysSinceCompletion >= quest.RefreshTimeInDays)
                {
                    quest.IsCompleted = false;
                    quest.CompletedDate = default;
                }
            }

            _context.Quests.UpdateRange(quests);
            _context.SaveChanges();
        }


    

    }
}
