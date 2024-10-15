
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Text;

public interface IProgressView
{

    List<Progress> GetProgresses(); // Return a list of Progress
    string GetDisplayProgressSummary(); // Updated to accept multiple progresses

    void DisplayProgressSummary();
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

    private string[] headers = new[] { "Id", "Description", "WorkItem", "CreatedAt" };


    // Method to generate multiple Progress instances in a loop
    public List<Progress> GetProgresses()
    {

        return progresses;
    }
    public void DisplayProgressSummary()
    {
        
        Console.Write(GetDisplayProgressSummary());
    }

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
            row.Description,
            row.WorkItem?.Title ?? "No WorkItem",
            row.CreatedAt.ToString("yyyy-MM-dd")
            };
            sb.Append(PrintRow(rowData, maxWidths) + "\n");
        }

        sb.Append(PrintSeparator(maxWidths) + "\n");

        return sb.ToString();
    }

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

    // Helper method to center text within a given width
    string CenterText(string text, int width)
    {
        int padding = (width - text.Length) / 2;
        return text.PadLeft(text.Length + padding).PadRight(width);
    }

}
