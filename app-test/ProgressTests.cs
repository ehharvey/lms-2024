

namespace progress_tests;

public class ProgressTests
{
    ProgressView progress = new ProgressView { };


    /*
    //Test for Creating Progress Model
    [Fact]
    public void ShouldCreateProgressWithCorrectFields()
    {
       

        // Assert
        Assert.Equal("MyDescription", progress.Description);
        Assert.Equal(1, progress.WorkItem.Id);
        Assert.Equal(DateTime.Now.Date, progress.CreatedAt.Date);  // Compare dates only to avoid time differences
    }

    // Test for CreatedAt Field Default Value
    [Fact]
    public void ShouldSetCreatedAtToCurrentDateWhenProgressIsCreated()
    {
        // Act
        var progress = new Progress();

        // Assert
        Assert.Equal(DateTime.Now.Date, progress.CreatedAt.Date); // Ensure CreatedAt is set to today's date
    }

    // Test for ForeignKey WorkItemId
    [Fact]
    public void ShouldHaveWorkItemForeignKey()
    {
        
        // Act & Assert
        Assert.NotNull(progress.WorkItem);
        Assert.Equal(1, progress.WorkItem.Id);
    }*/

}