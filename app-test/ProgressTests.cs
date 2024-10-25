/*
 Author: Lian
    Date: 10/15/2024
    File: ProgressTests.cs
    Description: 
        This file contains the implementation of the ProgressTests class, 
        which is responsible for testing the ProgressList class.
 */

using Lms.Models;

namespace progress_tests;


 
/// <summary>
/// This class contains unit tests for the ProgressList class.
/// </summary>
public class ProgressTests
{
    Progresses progresses = new Progresses();

    /// <summary>
    /// Method to get the first progress.
    /// </summary>
    /// <returns>The first progress.</returns>
    public Lms.Models.Progress GetFirstProgress()
    {
        var progressList = progresses.GetProgresses();

        if (progressList == null || progressList.Count == 0)
        {
            throw new InvalidOperationException("No progresses available.");
        }

        Lms.Models.Progress currentProgress = progressList[0];
        return currentProgress;
    }

  

    /// <summary>
    /// Test method to get the progress view type.
    /// </summary>
    [Fact]
    public void TestGetProgressViewType()
    {
        Assert.True(typeof(List<Lms.Models.Progress>) == progresses.GetProgresses().GetType());
    }

    /// <summary>
    /// Test method to create a progress with correct fields.
    /// </summary>
    [Fact]
    public void ShouldCreateProgressWithCorrectFields()
    {
        // Assert
        Assert.Equal("MyDescription", GetFirstProgress().Description);
        Assert.Equal(0, GetFirstProgress().WorkItem.Id);
        Assert.Equal(DateTime.Now.Date, GetFirstProgress().CreatedAt.Date);  // Compare dates only to avoid time differences
    }

    /// <summary>
    /// Test method to ensure CreatedAt field has default value of current date.
    /// </summary>
    [Fact]
    public void ShouldSetCreatedAtToCurrentDateWhenProgressIsCreated()
    {
        // Assert
        Assert.Equal(DateTime.Now.Date, GetFirstProgress().CreatedAt.Date); // Ensure CreatedAt is set to today's date
    }

    /// <summary>
    /// Test method to ensure WorkItem has foreign key.
    /// </summary>
    [Fact]
    public void ShouldHaveWorkItemForeignKey()
    {
        // Act & Assert
        Assert.NotNull(GetFirstProgress().WorkItem);
        Assert.Equal(0, GetFirstProgress().WorkItem.Id);
    }
}
