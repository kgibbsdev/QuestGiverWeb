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
using QuestGiver.Client.Pages;

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
                assignee.QuestLog = await _context.QuestLogs.FindAsync(assignee.QuestLogId);
				var assignedQuests = _context.Quests.Where(q => q.QuestLogId == assignee.QuestLogId).ToList();
				assignee.QuestLog.Quests = assignedQuests;
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

            assignee.QuestLog = await _context.QuestLogs.FindAsync(assignee.QuestLogId);

            var assignedQuests = _context.Quests.Where(q => q.IsAssigned == true && q.QuestLogId == assignee.QuestLogId);

            assignee.QuestLog.Quests = assignedQuests.ToList();

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

            _context.Assignees.Update(assignee);

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

        // POST: api/Assignees/New
        [HttpPost("new")]
        public async Task<IActionResult> NewAssignee([FromBody] Assignee assignee)
        {
            _context.Assignees.Add(assignee);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AssigneeExists(int id)
        {
            return (_context.Assignees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
