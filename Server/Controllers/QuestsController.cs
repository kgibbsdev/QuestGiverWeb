using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using QuestGiver.Server.Data;
using QuestGiver.Shared.Models;

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
            var existingQuest = await _context.Quests.FindAsync(request.Quest.Id);
            existingQuest.CompletedDate = request.Quest.CompletedDate;
            existingQuest.IsCompleted = request.Quest.IsCompleted;

            var existingAssignee = await _context.Assignees.FindAsync(request.Assignee.Id);
            existingAssignee.CurrentQuestId = null;
            existingAssignee.TotalExperience += existingQuest.ExperienceForCompletion;

            _context.Quests.Update(existingQuest);
            _context.Assignees.Update(existingAssignee);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
