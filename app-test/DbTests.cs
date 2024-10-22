using Lms;
using Lms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace db_tests;

public class DbTests
{
    LmsDbContext db = new LmsDbContext(DbDriver.Memory, "DbTests");

    [Fact]
    public void TestAddBlock()
    {
        Block block = new Block();
        db.Blockers.Add(block);
        db.SaveChanges();
        Assert.NotEqual(0, block.Id);
    }

    [Fact]
    public void TestAddProgress()
    {
        Lms.Models.Progress progress = new Lms.Models.Progress();
        db.Progresses.Add(progress);
        db.SaveChanges();
        Assert.NotEqual(0, progress.Id);
    }

    [Fact]
    public void TestAddWorkItem()
    {
        Lms.Models.WorkItem workItem = new Lms.Models.WorkItem { Title = "Test Work Item" };
        db.WorkItems.Add(workItem);
        db.SaveChanges();
        Assert.NotEqual(0, workItem.Id);
    }

    [Fact]
    public void TestAddWorkItemWithBlock()
    {
        Lms.Models.WorkItem workItem = new Lms.Models.WorkItem { Title = "Test Work Item" };
        Block block = new Block();
        workItem.Blocks.Add(block);
        db.WorkItems.Add(workItem);
        db.SaveChanges();
        Assert.NotEqual(0, workItem.Id);
        Assert.NotEqual(0, block.Id);

        // Ignore the warning about the following line because we
        // immediately check for null in the next line.
        Lms.Models.WorkItem workItem2 = db.WorkItems.Find(workItem.Id);
        Assert.NotNull(workItem2);
        Assert.Single(workItem2.Blocks);
    }
}