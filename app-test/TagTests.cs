using EntityFramework.Exceptions.Common;
using lms;
using Lms;
using Lms.Controllers;
namespace tag_tests;

public class TagTests: IDisposable
{
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "Tag");
    Tag tag = new Tag(db);

    public void Dispose()
    {
        db.WorkItems.RemoveRange(db.WorkItems);
        db.SaveChanges();
    }

    [Fact]
    public void TestDeleteItem() {
        // Arrange
        var name = "Delete Me!";
        var item = new Lms.Models.Tag { Name = name };
        db.Tags.Add(item);
        db.SaveChanges();

        // Act
        var actual = tag.Delete([item.Id.ToString()]);

        // Assert
        Assert.Equal(name, actual.Name);
    }

    [Fact]
    public void CreateTag() {
        // Arrange
        var name = "New Tag";
        
        // Act
        var createdTag = tag.Create([name]);

        // Assert
        Assert.Equal(name, createdTag.Name);
    }


    // This test fails. We think it's because the MS in-memory provider does not 
    // respect constraints.
    // TODO: Switch to sqlite in-memory https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/in-memory-databases
    // [Fact]
    // public void DuplicateTags() {
    //     // Arrange
    //     var name = "New Tag";
    //     var sameName = "New Tag";
    //     var expectedException = $"Tag with name ${name} already exists!";
        
    //     // Act
    //     var createdTag = tag.CreateTag([name]);
        
    //     // Act + Assert
    //     var actual = Assert.Throws<UniqueConstraintException>(
    //         () => {
    //             tag.CreateTag([sameName]);
    //         }
    //     );
    //     Assert.Equal(expectedException, actual.Message);
    // }
}