using Microsoft.EntityFrameworkCore;

using Lms.Models;

namespace Lms;

public enum DbDriver
{
    Sqlite,
    Postgres,
    Mysql,
    SqlServer,
    Memory
};


public class LmsDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<Models.WorkItem> WorkItems { get; set; }
    public DbSet<Block> Blockers { get; set; }

    public DbDriver Driver { get; private set; } = DbDriver.Sqlite;


    public string DbPath { get; private set; } = "Data Source=lms.db";

    public LmsDbContext()
    {
        
    }

    public LmsDbContext(string dbPath)
    {
        this.DbPath = dbPath;
    }

    public LmsDbContext(DbDriver driver)
    {
        this.Driver = driver;
    }

    public LmsDbContext(DbDriver driver, string dbPath = "lms")
    {
        this.Driver = driver;
        this.DbPath = dbPath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (Driver)
        {
            case DbDriver.Sqlite:
                optionsBuilder.UseSqlite(DbPath);
                break;
            case DbDriver.Postgres:
                throw new NotImplementedException();
            case DbDriver.Mysql:
                throw new NotImplementedException();
            case DbDriver.SqlServer:
                throw new NotImplementedException();
            case DbDriver.Memory:
                optionsBuilder.UseInMemoryDatabase(DbPath);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}