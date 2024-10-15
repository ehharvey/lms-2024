
using Lms.Models;

public interface IProgressView
{

    List<Progress> GetProgresses(); // Return a list of Progress
    string DisplayProgressSummary(); // Updated to accept multiple progresses
}

public class ProgressView : IProgressView
{
    private List<Progress> progresses = new List<Progress>
    {
        new Progress
        {
            Id = 0,
            Description = "MyDescription",
            WorkItem = new WorkItem { Id = 0, Title = "Assignment 2" }
        }
    };



    // Method to generate multiple Progress instances in a loop
    public List<Progress> GetProgresses()
    {

        return progresses;
    }

    public string DisplayProgressSummary()
    {
        // Calculate the maximum lengths for each column
        int maxIdLength = "Id".Length; 
        int maxDescriptionLength = "Description".Length;
        int maxWorkItemLength = "WorkItem".Length;
        int maxCreatedAtLength = "CreatedAt".Length;

        foreach (var progress in progresses)
        {
            maxIdLength = Math.Max(maxIdLength, progress.Id.ToString().Length);  
            maxDescriptionLength = Math.Max(maxDescriptionLength, (progress.Description ?? "No description").Length);
            maxWorkItemLength = Math.Max(maxWorkItemLength, (progress.WorkItem?.Title ?? "No work item").Length);
            maxCreatedAtLength = Math.Max(maxCreatedAtLength, progress.CreatedAt.ToString("yyyy-MM-dd").Length);
        }

        // Helper function to center-align text within a given width
        string PadBoth(string text, int width, bool takeExtraPadding = false)
        {
            int padding = width - text.Length;
            int padLeft = padding / 2;
            int padRight = padding - padLeft;

            // If this column should take the extra padding (in the case of uneven spacing)
            if (takeExtraPadding)
            {
                padRight++;
            }

            return text.PadLeft(text.Length + padLeft).PadRight(width);
        }

        // Create the table header with dynamic widths
        string table =
            $"+-{new string('-', maxIdLength)}-+-{new string('-', maxDescriptionLength)}-+-{new string('-', maxWorkItemLength)}-+-{new string('-', maxCreatedAtLength)}-+\n" +
            $"| {PadBoth("Id", maxIdLength)} | {PadBoth("Description", maxDescriptionLength)} | {PadBoth("WorkItem", maxWorkItemLength)} | {PadBoth("CreatedAt", maxCreatedAtLength, true)} |\n" + // Extra padding goes to "CreatedAt"
            $"+-{new string('-', maxIdLength)}-+-{new string('-', maxDescriptionLength)}-+-{new string('-', maxWorkItemLength)}-+-{new string('-', maxCreatedAtLength)}-+\n";

        // Add each progress entry to the table
        foreach (var progress in progresses)
        {
            string description = progress.Description ?? "No description";
            string workItemTitle = progress.WorkItem?.Title ?? "No work item";

            // Include the 'p' character before the Id and adjust padding
            table += $"| p{PadBoth(progress.Id.ToString(), maxIdLength - 1)} | {PadBoth(description, maxDescriptionLength)} | {PadBoth(workItemTitle, maxWorkItemLength)} | {PadBoth(progress.CreatedAt.ToString("yyyy-MM-dd"), maxCreatedAtLength, true)} |\n"; // Extra padding for "CreatedAt"
        }

        // Add the table footer
        table += $"+-{new string('-', maxIdLength)}-+-{new string('-', maxDescriptionLength)}-+-{new string('-', maxWorkItemLength)}-+-{new string('-', maxCreatedAtLength)}-+\n";

        return table;
    }

}
