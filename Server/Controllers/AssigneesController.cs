using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestGiver.Server.Data;
using QuestGiver.Shared.Models;
using QuestGiver.Shared.Classes.Utility;

namespace QuestGiver.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneesController : ControllerBase
    {
        private readonly QuestGiverDbContext _context;

        public AssigneesController(QuestGiverDbContext context)
        {
            _context = context;
        }

        // GET: api/Assignees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignee>>> GetAssignees()
        {
          if (_context.Assignees == null)
          {
              return NotFound();
          }
          var assignees = await _context.Assignees.ToListAsync();
            foreach (var assignee in assignees)
            {
                assignee.CurrentQuest = await _context.Quests.FindAsync(assignee.CurrentQuestId);
            }
            return assignees;
        }

        // GET: api/Assignees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignee>> GetAssignee(int id)
        {
          if (_context.Assignees == null)
          {
              return NotFound();
          }
            var assignee = await _context.Assignees.FindAsync(id);

            if (assignee == null)
            {
                return NotFound();
            }

            return assignee;
        }

        // PUT: api/Assignees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignee(int id, Assignee assignee)
        {
            if (id != assignee.Id)
            {
                return BadRequest();
            }

            // Retrieve the existing assignee from the database
            var existingAssignee = await _context.Assignees.FindAsync(id);

            if (existingAssignee == null)
            {
                return NotFound();
            }

            existingAssignee.CurrentQuest = assignee.CurrentQuest;
            
            //If the assignee has no current quest, set the current quest id to null
            if(existingAssignee.CurrentQuest == null)
            {
                existingAssignee.CurrentQuestId = null;
            }

            _context.Assignees.Update(existingAssignee);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssigneeExists(id))
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

        // POST: api/Assignees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assignee>> PostAssignee(Assignee assignee)
        {
          if (_context.Assignees == null)
          {
              return Problem("Entity set 'QuestGiverDbContext.Assignees'  is null.");
          }
            _context.Assignees.Add(assignee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignee", new { id = assignee.Id }, assignee);
        }

        // DELETE: api/Assignees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignee(int id)
        {
            if (_context.Assignees == null)
            {
                return NotFound();
            }
            var assignee = await _context.Assignees.FindAsync(id);
            if (assignee == null)
            {
                return NotFound();
            }

            _context.Assignees.Remove(assignee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssigneeExists(int id)
        {
            return (_context.Assignees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
