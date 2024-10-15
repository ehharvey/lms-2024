
/*
  Author: Lian
  Date: 10/15/2024
  File: ProgressView.cs
  Description: 
    This file contains the implementation of the ProgressView class, 
    which is responsible for displaying progress information in a formatted manner.

 */

using Lms.Models;
using System.Text;

public interface IProgressList
{
    /// <summary>
    /// Retrieves a list of progress objects.
    /// </summary>
    /// <returns>A list of Progress objects.</returns>
    List<Progress> GetProgresses();

    /// <summary>
    /// Generates a formatted string representation of the progress summary.
    /// </summary>
    /// <returns>A formatted string representation of the progress summary.</returns>
    string GetDisplayProgressSummary();

    /// <summary>
    /// Displays the progress summary.
    /// </summary>
    void DisplayProgressSummary();
}

/*
    The ProgressView class is an implementation of the IProgressView interface. 
    It represents a view that displays progress information. 
    This class provides methods to retrieve a list of progress objects, display a summary of the progress, and 
    generate a formatted string representation of the progress summary.
    The ProgressView class maintains a list of Progress objects and an array of headers for the progress summary table. It calculates the maximum column widths based on the headers and the data in the progress objects. The class also includes helper methods to format and print rows and separators for the progress summary table.
    Overall, the ProgressView class encapsulates the logic for retrieving progress data, generating a summary, and 
    displaying it in a formatted manner. It provides a convenient way to visualize progress information.
 */
public class ProgressList : IProgressList
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

    private string[] headers = new[] { "Id", "Description", "WorkItem", "CreatedAt" };


    // Method to generate multiple Progress instances in a loop
    /// <inheritdoc/>
    public List<Progress> GetProgresses()
    {

        return progresses;
    }

    /// <inheritdoc/>
    public void DisplayProgressSummary()
    {

        Console.Write(GetDisplayProgressSummary());
    }

    /// <inheritdoc/>
    public string GetDisplayProgressSummary()
    {
        List<Progress> rows = progresses;
        var maxWidths = GetMaxColumnWidths(headers, rows);
        var sb = new StringBuilder();

        sb.Append(PrintSeparator(maxWidths) + "\n");
        sb.Append(PrintRow(headers, maxWidths, true) + "\n"); // Center headers
        sb.Append(PrintSeparator(maxWidths) + "\n");

        foreach (var row in rows)
        {
            var rowData = new string[]
            {
                $"p{row.Id}",  // Prefix 'p' to the Id value
                row.Description ?? string.Empty,
                row.WorkItem?.Title ?? "No WorkItem",
                row.CreatedAt.ToString("yyyy-MM-dd")
            };
            sb.Append(PrintRow(rowData, maxWidths) + "\n");
        }

        sb.Append(PrintSeparator(maxWidths) + "\n");

        return sb.ToString();
    }

    /// <summary>
    /// Calculates the maximum column widths based on the headers and the data in the progress objects.
    /// </summary>
    /// <param name="headers">The array of headers for the progress summary table.</param>
    /// <param name="rows">The list of progress objects.</param>
    /// <returns>An array of integers representing the maximum column widths.</returns>
    int[] GetMaxColumnWidths(string[] headers, IEnumerable<Progress> rows)
    {
        var maxWidths = new int[headers.Length];

        // Determine maximum widths based on headers
        for (int i = 0; i < headers.Length; i++)
        {
            maxWidths[i] = headers[i].Length;
        }

        // Determine maximum widths based on the row contents
        foreach (var row in rows)
        {
            maxWidths[0] = Math.Max(maxWidths[0], $"p{row.Id}".Length);  // Add 'p' to the Id length
            maxWidths[1] = Math.Max(maxWidths[1], row.Description?.Length ?? 0);
            maxWidths[2] = Math.Max(maxWidths[2], row.WorkItem?.Title?.Length ?? 0);
            maxWidths[3] = Math.Max(maxWidths[3], row.CreatedAt.ToString("yyyy-MM-dd").Length);
        }

        return maxWidths;
    }

    /// <summary>
    /// Prints a row of data in the progress summary table.
    /// </summary>
    /// <param name="row">The array of data for the row.</param>
    /// <param name="maxWidths">The array of maximum column widths.</param>
    /// <param name="isHeader">A flag indicating whether the row is a header row.</param>
    /// <returns>A formatted string representing the row.</returns>
    string PrintRow(string[] row, int[] maxWidths, bool isHeader = false)
    {
        var formattedRow = new string[row.Length];
        for (int i = 0; i < row.Length; i++)
        {
            if (isHeader)
            {
                // Center header by padding manually
                formattedRow[i] = CenterText(row[i], maxWidths[i]);
            }
            else
            {
                // Left-align for data rows
                formattedRow[i] = String.Format($"{{0,-{maxWidths[i]}}}", row[i]);
            }
        }
        return "| " + string.Join(" | ", formattedRow) + " |";
    }

    /// <summary>
    /// Prints a separator line in the progress summary table.
    /// </summary>
    /// <param name="maxWidths">The array of maximum column widths.</param>
    /// <returns>A formatted string representing the separator line.</returns>
    string PrintSeparator(int[] maxWidths)
    {
        // Use String.Format to simplify separator generation
        var separatorParts = new string[maxWidths.Length];
        for (int i = 0; i < maxWidths.Length; i++)
        {
            separatorParts[i] = new string('-', maxWidths[i] + 2);
        }
        return "+" + string.Join("+", separatorParts) + "+";
    }

    /// <summary>
    /// Centers the text within a given width.
    /// </summary>
    /// <param name="text">The text to be centered.</param>
    /// <param name="width">The width of the text.</param>
    /// <returns>A string representing the centered text.</returns>
    string CenterText(string text, int width)
    {
        int padding = (width - text.Length) / 2;
        return text.PadLeft(text.Length + padding).PadRight(width);
    }
}
