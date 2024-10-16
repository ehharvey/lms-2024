
/*
  Author: Lian
  Date: 10/15/2024
  File: ProgressView.cs
  Description: 
    This file contains the implementation of the Progresses class, 
    which is responsible for displaying, editing, deleting and creating progress information in a formatted manner.

 */
using lms.Utilities;
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
        // this is for temporary data
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
        Table table = new Table();

        string[] headers = new[] { "Id", "Description", "WorkItem", "CreatedAt" };

        var maxWidths = table.GetMaxColumnWidths(headers, rows);
        var sb = new StringBuilder();

        sb.Append(table.PrintSeparator(maxWidths) + "\n");
        sb.Append(table.PrintRow(headers, maxWidths, true) + "\n"); // Center headers
        sb.Append(table.PrintSeparator(maxWidths) + "\n");

        foreach (var row in rows)
        {
            var rowData = new string[]
            {
                $"p{row.Id}",  // Prefix 'p' to the Id value
                row.Description ?? string.Empty,
                row.WorkItem?.Title ?? "No WorkItem",
                row.CreatedAt.ToString("yyyy-MM-dd")
            };
            sb.Append(table.PrintRow(rowData, maxWidths) + "\n");
        }

        sb.Append(table.PrintSeparator(maxWidths) + "\n");

        return sb.ToString();
    }


}