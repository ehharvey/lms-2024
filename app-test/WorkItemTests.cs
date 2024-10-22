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
    public void TestDeleteItem() {
        // Arrange
        var title = "Delete Me!";
        var item = new Lms.Models.WorkItem { Title = title };
        db.WorkItems.Add(item);
        db.SaveChanges();

        // Act
        var actual = work_item.Delete(item.Id.ToString());

        // Assert
        Assert.Equal(title, actual.Title);
    }

    [Fact]
    public void TestDeleteStringId() {
        // Arrange
        var title = "Delete Me!";
        var item = new Lms.Models.WorkItem { Title = title };
        db.WorkItems.Add(item);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Delete("invalid id"); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestDeleteNegativeId() {
        // Arrange
        var title = "Delete Me!";
        var item = new Lms.Models.WorkItem { Title = title };
        db.WorkItems.Add(item);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- WorkItem does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { work_item.Delete("-1"); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }
}