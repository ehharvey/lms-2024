using Lms;
using Microsoft.EntityFrameworkCore;
using Lms.Controllers;
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