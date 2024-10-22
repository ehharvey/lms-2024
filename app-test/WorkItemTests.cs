using System.Net;
using Lms;
using Microsoft.EntityFrameworkCore;

namespace workitem_tests;

public class WorkItemTests: IDisposable
{
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "WorkItemTests");
    WorkItem work_item = new WorkItem(db);

    public void Dispose()
    {
        db.WorkItems.RemoveRange(db.WorkItems);
        db.SaveChanges();
    }

    [Fact]
    public void TestEditWorkItem() {
        // Arrange
        var edit = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(edit);
        db.SaveChanges();

        var expected_title = "edited_title";

        // Act
        var actual = work_item.Edit(edit.Id.ToString(), "Title", expected_title);

        // Assert
        Assert.Equal(expected_title, actual.Title);
    }

    [Fact]
    public void TestEditWorkItemStringId() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Edit("asd", "Title", "new title"); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditWorkItemNegatiVeId() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid Id -- WorkItem does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Edit("-1", "Title", "new title"); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditWorkItemInvalidField() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid field");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Edit(item.Id.ToString(), "invalid field", "new title"); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditWorkInvalidDate() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid DueAt value provided");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Edit(item.Id.ToString(), "DueAt", "Invalid Date"); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditWorkItemValidDate() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem" };
        db.WorkItems.Add(item);
        db.SaveChanges();

        var date = "2024-12-12";
        var expected_date = DateTime.Parse(date);

        // Act
        var actual = work_item.Edit(item.Id.ToString(), "DueAt", date);

        // Assert
        Assert.Equal(expected_date, actual.DueAt);
    }

    [Fact]
    public void TestEditWorkItemValidChangeDate() {
        // Arrange
        var item = new Lms.Models.WorkItem { Title = "WorkItem", DueAt = DateTime.Parse("3000-1-1") };
        db.WorkItems.Add(item);
        db.SaveChanges();

        var date = "2024-1-12";
        var expected_date = DateTime.Parse(date);

        // Act
        var actual = work_item.Edit(item.Id.ToString(), "DueAt", date);

        // Assert
        Assert.Equal(expected_date, actual.DueAt);
    }
}