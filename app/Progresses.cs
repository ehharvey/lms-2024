
/*
  Author: Lian
  Date: 10/15/2024
  File: ProgressView.cs
  Description: 
    This file contains the implementation of the Progresses class, 
    which is responsible for displaying, editing, deleting and creating progress information in a formatted manner.

 */
using Lms;
using ConsoleTables;

using Lms.Models;



// This progresses class is responsible for displaying the progress information in a formatted manner.
class Progresses : ICommand {
    private LmsDbContext db;
    private List<Progress> progressList;
  

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


    public void Execute(Verb verb, string[] command_args)
    {
        switch (verb)
        {
            case Verb.List:
                Execute(verb);
                break;
            //case Verb.Delete:
            //    if (command_args.Count() < 1)
            //    {
            //        throw new ArgumentException("Delete requires an ID!");
            //    }

            //    var delete_result = Delete(command_args[0]);

            //    Console.WriteLine("----------------");
            //    Console.WriteLine($"ID: {delete_result.Id}");
            //    Console.WriteLine($"Title: {delete_result.Title}");
            //    Console.WriteLine($"CreatedAt: {delete_result.CreatedAt}");
            //    Console.WriteLine($"DueAt: {delete_result.DueAt}");
            //    Console.WriteLine("----------------");
            //    break;
            //case Verb.Edit:
            //    if (command_args.Count() < 3)
            //    {
            //        throw new ArgumentException("3 arguments: Id, Field, and Value!");
            //    }

            //    var edit_result = Edit(command_args[0], command_args[1], command_args[2]);

            //    Console.WriteLine("------------------------");
            //    Console.WriteLine($"Id: {edit_result.Id}");
            //    Console.WriteLine($"Title: {edit_result.Title}");
            //    Console.WriteLine($"CreatedAt: {edit_result.CreatedAt}");
            //    Console.WriteLine($"DueAt: {edit_result.DueAt}");
            //    Console.WriteLine("------------------------");

            //    break;
            //case Verb.Create:
            //    if (command_args.Count() < 1)
            //    {
            //        throw new ArgumentException("Create requires at least a Title Arg");
            //    }

            //    var title = command_args[0];
            //    string? due_at = command_args.ElementAtOrDefault(1);

            //    var create_result = Create(title, due_at);

            //    Console.WriteLine("----------------------");
            //    Console.WriteLine($"Id: {create_result.Id}");
            //    Console.WriteLine($"Title: {create_result.Title}");
            //    Console.WriteLine($"CreatedAt: {create_result.CreatedAt}");
            //    Console.WriteLine($"DueAt: {create_result.DueAt}");
            //    break;

            default:
                throw new ArgumentException("Invalid Verb");
        }
    }


    /// <summary>
    /// Method to generate multiple Progress instances in a loop
    /// </summary>
    /// <inheritdoc/>
    public List<Lms.Models.Progress> GetProgresses()
    {

        // this is for temporary data
        progressList = new List<Lms.Models.Progress>
        {
            new Lms.Models.Progress
            {
                Id = 0,
                Description = "MyDescription",
                WorkItem = new WorkItem { Id = 0, Title = "Assignment 2" }
            }
        };

        return progressList;
    }


    ///<summary>
    /// Displays the progress summary.
    /// </summary>
    public void DisplayProgressSummary()
    {

        List<Progress> progressList = GetProgresses();


        var table = new ConsoleTable("Id", "Description", "WorkItem", "CreatedAt");



        foreach (var progress in progressList)
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