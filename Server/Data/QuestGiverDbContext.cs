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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Quest>().ToTable("Quests");
      modelBuilder.Entity<Assignee>().ToTable("Assignees");
      modelBuilder.Entity<QuestLog>().ToTable("QuestLogs");
      }
  }
}
