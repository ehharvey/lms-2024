
/*
  Author: Lian
  Date: 10/15/2024
  File: ProgressView.cs
  Description: 
    This file contains the implementation of the Progresses class, 
    which is responsible for displaying, editing, deleting and creating progress information in a formatted manner.

 */
using Lms;
using Lms.Models;
using System.Text;

// This progresses class is responsible for displaying the progress information in a formatted manner.
class Progresses : ICommand
{
    private LmsDbContext? db;
    private List<Lms.Models.Progress> progresses;
    /// <summary>
    /// Initializes a new instance of the Progresses class.
    /// </summary>
    public Progresses()
    {
        this.db = null;
    }

    /// <summary>
    /// Initializes a new instance of the Progresses class with the specified LmsDbContext.
    /// </summary>
    /// <param name="db">The LmsDbContext to use.</param>
    public Progresses(LmsDbContext db)
    {
        this.db = db;
        //progresses = db.Progresses.ToList();
    }

    /// <summary>
    /// Gets the help information for the Progresses command.
    /// </summary>
    /// <returns>A string containing the help information.</returns>
    public string GetHelp()
    {
        return $"""
        Progresses

        Description:
        The progresses for assignments.
        Verbs:
        - list: {GetHelp(Verb.List)}
        """;
        //- edit: {GetHelp(Verb.Edit)}
        //- delete: {GetHelp(Verb.Delete)}

    }

    public string GetHelp(Verb verb)
    {
        switch (verb)
        {
            case Verb.List:
                return "Lists the progresses for a assignments.";
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }


    /// <summary>
    /// Executes the specified verb.
    /// </summary>
    /// <param name="verb">The verb to execute.</param>
    public void Execute(Verb verb)
    {
        switch (verb)
        {
            case Verb.List:
                DisplayProgressSummary();
                break;
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }

    /// <summary>
    /// Displays the progress summary.
    /// </summary>
    private void DisplayProgressSummary()
    {
        Console.Write(GetDisplayProgressSummary());
    }

    /// <summary>
    /// Method to generate multiple Progress instances in a loop
    /// </summary>
    /// <inheritdoc/>
    public List<Lms.Models.Progress> GetProgresses()
    {
        progresses = new List<Lms.Models.Progress>
        {
            new Lms.Models.Progress
            {
                Id = 0,
                Description = "MyDescription",
                WorkItem = new WorkItem { Id = 0, Title = "Assignment 2" }
            }
        };

        return progresses;
    }

    
    ///<summary>
    /// Displays the progress summary.
    /// </summary>
    /// <returns>A formatted string representing the progress summary.</returns>
    public string GetDisplayProgressSummary()
    {
        List<Progress> rows = GetProgresses();

        string[] headers = new[] { "Id", "Description", "WorkItem", "CreatedAt" };

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
    private int[] GetMaxColumnWidths(string[] headers, IEnumerable<Lms.Models.Progress> rows)
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
    private string PrintRow(string[] row, int[] maxWidths, bool isHeader = false)
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
    private string PrintSeparator(int[] maxWidths)
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
    private string CenterText(string text, int width)
    {
        int padding = (width - text.Length) / 2;
        return text.PadLeft(text.Length + padding).PadRight(width);
    }
}