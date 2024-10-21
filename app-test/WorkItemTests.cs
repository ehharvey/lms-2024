using Lms;
using Lms.Models;

namespace db_tests;

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
    public void CreateOneWorkItem()
    {
        // Arrange
        string title = "WorkItemOne";
        string? due_at = null;

        // Act
        var actual = work_item.Create(title, due_at);

        // Assert
        Assert.Equal(title, actual.Title);
    }

    [Fact]
    public void CreateOneWorkItemWithValidDate()
    {
        // Arrange
        string title = "WorkItemTwo";
        string due_at = "2024-10-02";
        DateTime expected_parsed_due_at = DateTime.Parse(due_at);

        // Act
        var actual = work_item.Create(title, due_at);

        // Assert
        Assert.Equal(title, actual.Title);
        Assert.Equal(expected_parsed_due_at, actual.DueAt);
    }

    [Fact]
    public void CreateOneWorkItemWithInvalidDate()
    {
        // Arrange
        string title = "WorkItemThree";
        string due_at = "invaliddate";

        // Act
        Assert.Throws<FormatException>(() => { work_item.Create(title, due_at ); });
    }
}