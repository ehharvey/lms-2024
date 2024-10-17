using System.Net;
using Lms;
using Microsoft.EntityFrameworkCore;

namespace workitem_tests;

public class WorkItemTests : IDisposable
{
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "WorkItemTests");
    WorkItem work_item = new WorkItem(db);

    public void Dispose()
    {
        db.WorkItems.RemoveRange(db.WorkItems);
        db.SaveChanges();
    }

    [Fact]
    public void TestNoWorkItems()
    {
        // Arrange
        var expected = new List<Lms.Models.WorkItem>();

        // Act
        var actual = work_item.GetWorkItems().ToList();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestOneWorkItem()
    {
        // Arrange
        var expected = new Lms.Models.WorkItem { Title = "TestOneWorkItem", Blocks = [], Progresses = []};

        // Act
        db.WorkItems.Add(expected);
        db.SaveChanges();
        var actual = work_item.GetWorkItems().ToList();

        // Assert
        Assert.Equal(1, actual.Count());
        Assert.Equal(expected, actual.First());        
    }

    [Fact]
    public void TestTwoWorkItems()
    {
        // Arrange
        var expected_one = new Lms.Models.WorkItem { Title = "TestTwoWorkItemOne", Blocks = [], Progresses = []};
        var expected_two = new Lms.Models.WorkItem { Title = "TestTwoWorkItemTwo", Blocks = [], Progresses = []};


        // Act
        db.WorkItems.Add(expected_one);
        db.WorkItems.Add(expected_two);
        db.SaveChanges();
        var actual = work_item.GetWorkItems().ToList();

        // Assert
        Assert.Equal(2, actual.Count());
        Assert.Equal(expected_one, actual.First());
        Assert.Equal(expected_two, actual.Skip(1).First());
    }
}