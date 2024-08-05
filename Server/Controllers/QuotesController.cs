using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestGiver.Server.Data;
using QuestGiver.Shared.Models;

namespace QuestGiver.Server.Controllers
{
	//This is just for me, and at the moment will be managed completely
	//Manually from the database. This is not good practice...
	//But it's my app ;)
	[Route("api/[controller]")]
	[ApiController]
	public class QuotesController : ControllerBase
	{
		private readonly QuestGiverDbContext _context;

		public QuotesController(QuestGiverDbContext context)
		{
			_context = context;
		}

		// GET: api/Quests
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
		{
			if (_context.Quests == null)
			{
				return NotFound();
			}
			return await _context.Quotes.ToListAsync();
		}
	}
}