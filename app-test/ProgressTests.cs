using Lms;
using Microsoft.EntityFrameworkCore;
using Lms.Controllers;
namespace progress_tests;

public class ProgressTests: IDisposable
{
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "ProgressTests");
    Progress progress = new Progress(db);


    public void Dispose()
    {
        db.Progresses.RemoveRange(db.Progresses);
        db.SaveChanges();
    }

    [Fact]
    public void TestDeleteItem() {
        // Arrange
        var description = "Delete Me!";
        var item = new Lms.Models.Progress { Description = description };
        db.Progresses.Add(item);
        db.SaveChanges();

        // Act
        var actual = progress.Delete([item.Id.ToString()]);

        // Assert
        Assert.Equal(description, actual.Description);
    }

    [Fact]
    public void TestDeleteStringId() {
        // Arrange
        var decription = "Delete Me!";
        var item = new Lms.Models.Progress { Description = decription };
        db.Progresses.Add(item);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Delete(["invalid id"]); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestDeleteNegativeId() {
        // Arrange
        var description = "Delete Me!";
        var item = new Lms.Models.Progress { Description = description };
        db.Progresses.Add(item);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- Progress does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Delete(["-1"]); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestNoProgresses()
    {
        // Arrange
        var expected = new List<Lms.Models.Progress>();

        // Act
        var actual = progress.List().ToList();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestOneProgress()
    {
        // Arrange
        var expected = new Lms.Models.Progress { Description = "TestOneProgress", WorkItem = null};

        // Act
        db.Progresses.Add(expected);
        db.SaveChanges();
        var actual = progress.List().ToList();

        // Assert
        Assert.Equal(1, actual.Count());
        Assert.Equal(expected, actual.First());        
    }

    [Fact]
    public void TestTwoProgresses()
    {
        // Arrange
        var expected_one = new Lms.Models.Progress { Description = "TestOneProgress", WorkItem = null };
        var expected_two = new Lms.Models.Progress { Description = "TestTwoProgress", WorkItem = null };


        // Act
        db.Progresses.Add(expected_one);
        db.Progresses.Add(expected_two);
        db.SaveChanges();
        var actual = progress.List().ToList();

        // Assert
        Assert.Equal(2, actual.Count());
        Assert.Equal(expected_one, actual.First());
        Assert.Equal(expected_two, actual.Skip(1).First());
    }

    
    [Fact]
    public void TestEditProgress() {
        // Arrange
        var edit = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(edit);
        db.SaveChanges();

        var expected_description = "edited_Description";

        // Act
        var actual = progress.Edit([edit.Id.ToString(), "Description", expected_description]);

        // Assert
        Assert.Equal(expected_description, actual.Description);
    }

    [Fact]
    public void TestEditProgressStringId() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Edit(["asd", "Description", "new description"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditProgressNegatiVeId() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid Id -- Progress does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Edit(["-1", "Description", "new description"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditProgressInvalidField() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid field");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Edit([item.Id.ToString(), "invalid field", "new description"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditIProgressInvalidWorkItem() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(item);
        db.SaveChanges();
        
        var expected = new ArgumentException("Invalid WorkItem Id value provided");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { progress.Edit([item.Id.ToString(), "WorkItem", "Invalid Id"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditProgressValidWorkItem() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "Progress" };
        db.Progresses.Add(item);
        db.SaveChanges();

        db.WorkItems.Add(new Lms.Models.WorkItem { Id = 1, Title = "WorkItem" });

        var workItem = "1";
        var expected_workItem = int.Parse(workItem);

        // Act
        var actual = progress.Edit([item.Id.ToString(), "WorkItem", workItem]);

        // Assert
        Assert.Equal(expected_workItem, actual.WorkItem.Id);
    }

    [Fact]
    public void TestEditProgressValidChangeWorkItem() {
        // Arrange
        var item = new Lms.Models.Progress { Description = "WorkItem", WorkItem = new Lms.Models.WorkItem { Title = "WorkItem 1" } };
        db.Progresses.Add(item);
        db.SaveChanges();

        var workItem_edited = "1";
        var expected_workItem = workItem_edited;

        // Act
        var actual = progress.Edit([item.Id.ToString(), "WorkItem", workItem_edited]);

        // Assert
        Assert.Equal(expected_workItem, actual.WorkItem.Id.ToString());
    }
}