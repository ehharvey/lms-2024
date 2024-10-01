using Microsoft.EntityFrameworkCore;

using Lms.Models;

namespace Lms;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<Block> Blockers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=lms.db");
    }
}