
using Lms.Models;

public interface IProgressView
{
    List<Progress> GetProgresses(); // Return a list of Progress
    string DisplayProgressSummary(List<Progress> progresses); // Updated to accept multiple progresses
}

public class ProgressView : IProgressView
{
    // Method to generate multiple Progress instances in a loop
    public List<Progress> GetProgresses()
    {
        List<Progress> progresses = new List<Progress>();

        // Loop to create multiple Progress instances
        for (int i = 1; i <= 1; i++) // Example: generates 5 progresses
        {
            progresses.Add(new Progress
            {
                Id = i,
                Description = $"Task {i} completed",
                WorkItem = new WorkItem { Id = i, Title = $"WorkItem {i}" },
                // CreatedAt will automatically be set to DateTime.Now due to the model
            });
        }

        return progresses;
    }

    // Method to display the progress summary for multiple progresses in a table format
    public string DisplayProgressSummary(List<Progress> progresses)
    {
        string table =
            "+----+---------------+--------------+------------+\n" +
            "| Id |  Description  |   WorkItem   | CreatedAt  |\n" +
            "+----+---------------+--------------+------------+\n";

        foreach (var progress in progresses)
        {
            string description = progress.Description ?? "No description";
            string workItemTitle = progress.WorkItem?.Title ?? "No work item";

            table += $"| {progress.Id,-3} | {description,-13} | {workItemTitle,-12} | {progress.CreatedAt.ToString("yyyy-MM-dd"),-10} |\n";
        }

        table += "+----+---------------+--------------+------------+\n";

        return table;
    }
}
