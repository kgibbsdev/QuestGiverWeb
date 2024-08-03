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
using QuestGiver.Shared.Classes.Utility;

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

            if(existingQuest != null)
            {
				existingQuest.CompletedDate = quest.CompletedDate;
				existingQuest.IsCompleted = quest.IsCompleted;
				existingQuest.Name = quest.Name;
				existingQuest.Priority = quest.Priority;
				existingQuest.ExperienceForCompletion = quest.ExperienceForCompletion;
				existingQuest.Description = quest.Description;
				existingQuest.RefreshTimeInDays = quest.RefreshTimeInDays;
				existingQuest.IsAssigned = quest.IsAssigned;
				existingQuest.QuestLogId = quest.QuestLogId;
			}

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
            var incomingQuest = request.Quest;
            var existingQuest = await _context.Quests.FindAsync(incomingQuest.Id);
            existingQuest.CompletedDate = incomingQuest.CompletedDate;
            existingQuest.IsCompleted = incomingQuest.IsCompleted;
            existingQuest.TimesCompleted += 1;
            existingQuest.IsAssigned = false;
            existingQuest.QuestLogId = null;

            var existingAssignee = await _context.Assignees.FindAsync(request.Assignee.Id);
            existingAssignee.TotalExperience += existingQuest.ExperienceForCompletion;
            existingAssignee.Level = LevelCalculator.CalculateLevel(existingAssignee.TotalExperience);
            existingAssignee.QuestsCompleted += 1;

            var incomingQuestLog = request.Assignee.QuestLog;
            var existingQuestLog = await _context.QuestLogs.FindAsync(incomingQuestLog.Id);
            existingQuestLog.QuestsCompleted += 1;
            existingQuestLog.Quests.Remove(incomingQuest);

            _context.Quests.Update(existingQuest);
            _context.Assignees.Update(existingAssignee);
            _context.QuestLogs.Update(existingQuestLog);
            

            await _context.SaveChangesAsync();

            return NoContent();
        }

		// POST: api/Quests/assign
		[HttpPost("assign")]
		public async Task<IActionResult> AssignNewQuest([FromBody] Assignee assignee)
		{
			// Fetch quests without tracking to avoid conflicts
			var quests = await _context.Quests.AsNoTracking().Where(q => q.IsCompleted == false).ToListAsync();

			var assignableQuests = new List<Quest>();

			// Filter out assigned quests and prioritize based on your logic
			var unassignedQuests = quests.Where(q => !q.IsAssigned).ToList();

			if (unassignedQuests.Any(q => q.Priority == QuestPriority.High))
			{
				assignableQuests = unassignedQuests.Where(q => q.Priority == QuestPriority.High).ToList();
			}
			else if (unassignedQuests.Any(q => q.Priority == QuestPriority.Medium))
			{
				assignableQuests = unassignedQuests.Where(q => q.Priority == QuestPriority.Medium).ToList();
			}
			else if (unassignedQuests.Any(q => q.Priority == QuestPriority.Low))
			{
				assignableQuests = unassignedQuests.Where(q => q.Priority == QuestPriority.Low).ToList();
			}

			if (!assignableQuests.Any())
			{
				return NotFound("No assignable quests available.");
			}

			// Pick a random quest from the assignable quests
			var random = new Random();
			var randomQuest = assignableQuests[random.Next(assignableQuests.Count)];

			// Ensure the assignee's QuestLog is not null and attach it to the context if necessary
			if (assignee.QuestLog == null)
			{
				assignee.QuestLog = new QuestLog();
				_context.QuestLogs.Add(assignee.QuestLog); // New QuestLog should be added
			}
			else
			{
				_context.QuestLogs.Attach(assignee.QuestLog); // Attach existing QuestLog
			}

			// Update the quest and assignee
			randomQuest.IsAssigned = true;
			randomQuest.QuestLogId = assignee.QuestLog.Id;

			// Attach the quest to the context and mark it as modified
			_context.Quests.Attach(randomQuest);
			_context.Entry(randomQuest).State = EntityState.Modified;

			// Attach the assignee to the context and mark it as modified
			_context.Assignees.Attach(assignee);
			_context.Entry(assignee).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return Ok(assignee);
		}


		// POST: api/AssignThisQuestToMe/kyle
		[Route("AssignThisQuestToMe/{name}")]
        [HttpPost]
        public async Task<IActionResult> AssignThisQuestToMe([FromBody] Quest quest, [FromRoute] string name)
        {
            Assignee assignee = await _context.Assignees.FirstOrDefaultAsync(a => a.Name == name);
            if (assignee != null) {

                if (assignee.QuestLog == null)
                {
                    assignee.QuestLog = new QuestLog();
                }

                assignee.QuestLog.AddQuest(quest);
                quest.IsAssigned = true;

                _context.Quests.Update(quest);
                _context.Assignees.Update(assignee);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Active quest is {assignee.QuestLog.ActiveQuest}");
                return Ok(assignee);
            }
            else
            {
                string message = $"User with the name {name} was not found!";
                Console.WriteLine(message);
                return NotFound(message);
            }
        }

        // POST: api/Quests/Reset
        [HttpPost("reset")]
        public async Task<IActionResult> ResetQuests(bool hardReset)
        {
            //Do not delete
            #if !DEBUG
                return NotFound();
            #endif

            var quests = await _context.Quests.ToListAsync();
            var assignees = await _context.Assignees.ToListAsync();

            foreach (var quest in quests)
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

        // POST: api/Quests/Startup
        [HttpPost("startup")]
        public async Task<IActionResult> Startup()
        {
            StartupJobs();
            return Ok();
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
