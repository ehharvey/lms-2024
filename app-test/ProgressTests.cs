using Lms.Models;

namespace progress_tests;

public class ProgressTests
{
    ProgressView progresses = new ProgressView { };
  

    public Progress GetFirstProgress()
    {
        var progressList = progresses.GetProgresses();

        if (progressList == null || progressList.Count == 0)
        {
            throw new InvalidOperationException("No progresses available.");
        }

        Progress currentProgress = progressList[0];
        return currentProgress;
    }
    [Fact]
    public void TestDisplayProgressSummary()
    {
        var expectedTable =
            "+----+---------------+--------------+------------+\n" +
            "| Id |  Description  |   WorkItem   | CreatedAt  |\n" +
            "+----+---------------+--------------+------------+\n" +
            "| p0 | MyDescription | Assignment 2 | " + DateTime.Now.ToString("yyyy-MM-dd") + " |\n" +
            "+----+---------------+--------------+------------+\n";

        

        // Assert
        Assert.Equal(expectedTable, progresses.GetDisplayProgressSummary());
    }
    [Fact]
    public void TestGetProgressViewType()
    {
        Assert.True(typeof(List<Progress>) == progresses.GetProgresses().GetType());
    }

    
    //Test for Creating Progress Model
    [Fact]
    public void ShouldCreateProgressWithCorrectFields()
    {

        // Assert
        Assert.Equal("MyDescription", GetFirstProgress().Description);
        Assert.Equal(0, GetFirstProgress().WorkItem.Id);
        Assert.Equal(DateTime.Now.Date, GetFirstProgress().CreatedAt.Date);  // Compare dates only to avoid time differences
    }
  
    // Test for CreatedAt Field Default Value
    [Fact]
    public void ShouldSetCreatedAtToCurrentDateWhenProgressIsCreated()
    {
   
        // Assert
        Assert.Equal(DateTime.Now.Date, GetFirstProgress().CreatedAt.Date); // Ensure CreatedAt is set to today's date
    }

    // Test for ForeignKey WorkItemId
    [Fact]
    public void ShouldHaveWorkItemForeignKey()
    {
        
        // Act & Assert
        Assert.NotNull(GetFirstProgress().WorkItem);
        Assert.Equal(0, GetFirstProgress().WorkItem.Id);
    }

}