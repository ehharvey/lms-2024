
/*
  Author: Lian
  Date: 10/15/2024
  File: ProgressView.cs
  Description: 
    This file contains the implementation of the Progresses class, 
    which is responsible for displaying, editing, deleting and creating progress information in a formatted manner.

 */
using ConsoleTables;
using Lms;
using Lms.Models;



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
        //  List<Progress> progresses = db.Progresses.Include(p => p.WorkItem).ToList();
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
    public void DisplayProgressSummary()
    {

        List<Progress> progresses = GetProgresses();


        var table = new ConsoleTable("Id", "Description", "WorkItem", "CreatedAt");



        foreach (var progress in progresses)
        {
            // string workItems = string.Join(", ", progress.WorkItems.Select(w => w.Title));

            table.AddRow($"p{progress.Id}",  // Prefix 'p' to the Id value
                progress.Description ?? string.Empty,
                progress.WorkItem?.Title ?? "No WorkItem",
                progress.CreatedAt.ToString("yyyy-MM-dd"));


        }


        table.Configure(o =>
        {
            o.NumberAlignment = Alignment.Left;  // Ensure Alignment.Left is recognized

        }).Write(Format.Alternative);

       

       
        
    }


}