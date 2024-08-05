using Microsoft.EntityFrameworkCore;
using QuestGiver.Shared.Models;

namespace QuestGiver.Server.Data
{
	public class QuestGiverDbContext : DbContext
	{
	public QuestGiverDbContext(DbContextOptions<QuestGiverDbContext> options) : base(options)
	{
		
	}

	public DbSet<Quest> Quests { get; set; }
	public DbSet<Assignee> Assignees { get; set; }
	public DbSet<QuestLog> QuestLogs { get; set; }
	public DbSet<Quote> Quotes { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Quest>().ToTable("Quests");
		modelBuilder.Entity<Assignee>().ToTable("Assignees");
		modelBuilder.Entity<QuestLog>().ToTable("QuestLogs");
		modelBuilder.Entity<Quote>().ToTable("Quotes");
	}
}
}
