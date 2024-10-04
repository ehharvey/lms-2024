using Lms;
using Lms.Models;

namespace db_tests;

public class DbTests
{
    LmsDbContext db = new LmsDbContext(DbDriver.Memory);

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
        Progress progress = new Progress();
        db.Progresses.Add(progress);
        db.SaveChanges();
        Assert.NotEqual(0, progress.Id);
    }

    [Fact]
    public void TestAddWorkItem()
    {
        WorkItem workItem = new WorkItem { Title = "Test Work Item" };
        db.WorkItems.Add(workItem);
        db.SaveChanges();
        Assert.NotEqual(0, workItem.Id);
    }

    [Fact]
    public void TestAddWorkItemWithBlock()
    {
        WorkItem workItem = new WorkItem { Title = "Test Work Item" };
        Block block = new Block();
        workItem.Blocks.Add(block);
        db.WorkItems.Add(workItem);
        db.SaveChanges();
        Assert.NotEqual(0, workItem.Id);
        Assert.NotEqual(0, block.Id);

        // Ignore the warning about the following line because we
        // immediately check for null in the next line.
        WorkItem workItem2 = db.WorkItems.Find(workItem.Id);
        Assert.NotNull(workItem2);
        Assert.Single(workItem2.Blocks);
    }
}