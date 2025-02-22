using Microsoft.EntityFrameworkCore;

using Lms.Models;
using EntityFramework.Exceptions.Sqlite;

namespace Lms;


// DB Context for Handling Database

// DB Drivers
public enum DbDriver
{
    Sqlite,
    Postgres,
    Mysql,
    SqlServer,
    Memory
};

// Custom DBContext that implements base DBContext class
public class LmsDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    // DB Sets (Tables in Database)

    public DbSet<User> Users { get; set; } 
    public DbSet<Models.Progress> Progresses { get; set; }
    public DbSet<Models.WorkItem> WorkItems { get; set; }
    public DbSet<Block> Block { get; set; }

    public DbSet<Models.Tag> Tags { get; set; }

    // set database to SQLite
    public DbDriver Driver { get; private set; } = DbDriver.Sqlite;

    // Connection String for database connection
    public string DbPath { get; private set; } = "Data Source=lms.db";


    // Cunstructors
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

    // Overriding OnConfigring Method to manage Database connections
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (Driver)
        {
            case DbDriver.Sqlite:
                optionsBuilder.UseSqlite(DbPath);       // Connect DBContext to SQLite Database with connection string(DbPath) 
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

        optionsBuilder.UseExceptionProcessor();
    }
}