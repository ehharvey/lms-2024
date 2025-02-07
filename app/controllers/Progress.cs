using Lms;
using Lms.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Controllers
{

    class Progress : ICommand
    {


        public enum Field
        {
            Description,
            WorkItem // WorkItem(Id)
        }

        // DBContext for Database Interactions (Add, Update, Remove, SaveChanges)
        private LmsDbContext db;

        // Constructor - Set DB
        public Progress(LmsDbContext db)
        {
            this.db = db;
        }


        // Command Documentation for CLI Application

        // Main Command Documentation
        public string GetHelp()
        {
            return $"""
        Progress 

        Description:
        Tracks Progress of Work Items for the program.

        Verbs:
        - edit: {GetHelp(Verb.Edit)}
        - delete: {GetHelp(Verb.Delete)}
        """;
        }

        // Individual Commands with their Description
        public string GetHelp(Verb verb)
        {
            switch (verb)
            {
                case Verb.Edit:
                    return "Edit an existing Progress";
                case Verb.Delete:
                    return "Delete an exising Progress";
                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }

        public IEnumerable<Lms.Models.Progress> GetProgresses()
        {
            return db.Progresses.AsEnumerable();
        }


        // Execute Function without additional Arguments
        public void Execute(Verb verb)
        {
            switch (verb)
            {

                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }



        // Overloaded Execute Function with additional Arguments (Ex. Delete, Edit, Create) -> lms Progress Delete 0, lms Progress Edit 3
        public void Execute(Verb verb, string[] command_args)
        {
            switch (verb)
            {

                case Verb.Delete:
                    if (command_args.Count() < 1)
                    {
                        throw new ArgumentException("Delete requires an ID!");
                    }

                    var delete_result = Delete(command_args[0]);

                    Console.WriteLine("----------------");
                    Console.WriteLine($"ID: {delete_result.Id}");
                    Console.WriteLine($"Description: {delete_result.Description}");
                    Console.WriteLine($"WorkItem: {delete_result.WorkItem.Id}");
                    Console.WriteLine($"CreatedAt: {delete_result.CreatedAt}");
                    Console.WriteLine("----------------");
                    break;
                case Verb.Edit:
                    if (command_args.Count() < 3)
                    {
                        throw new ArgumentException("3 arguments: Id, Field, and Value!");
                    }

                    var edit_result = Edit(command_args[0], command_args[1], command_args[2]);

                    Console.WriteLine("----------------");
                    Console.WriteLine($"ID: {edit_result.Id}");
                    Console.WriteLine($"Description: {edit_result.Description}");
                    Console.WriteLine($"WorkItem: {edit_result.WorkItem.Id}");
                    Console.WriteLine($"CreatedAt: {edit_result.CreatedAt}");
                    Console.WriteLine("----------------");

                    break;

                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }

        // Edit Progress
        public Lms.Models.Progress Edit(string id, string field, string value)
        {
            int parsed_id = -1;

            try
            {
                parsed_id = int.Parse(id);
            }
            catch
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.Progresses.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- Progress does not exist");
            }

            Field f;
            if (!Enum.TryParse<Field>(field, out f))
            {
                throw new ArgumentException("Invalid field");
            }

            switch (f)
            {
                case Field.Description:
                    result.Description = value;
                    break;
                case Field.WorkItem:
                    int parsed_work_item_id;
                    if (int.TryParse(value, out parsed_work_item_id))
                    {
                        Lms.Models.WorkItem? workitem = db.WorkItems.Find(parsed_work_item_id);
                        if (workitem != null)
                        {
                            result.WorkItem = workitem;
                        }
                        else
                        {
                            throw new ArgumentException("WorkItem Not Found");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid WorkItem Id value provided");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid Field");
            }

            db.Progresses.Update(result);
            db.SaveChanges();

            return result;
        }

        // Delete Progress by Id
        // TODO: move the parsing logic outside (maybe in CommandLineParser)
        public Lms.Models.Progress Delete(string id)
        {
            int parsed_id;
            if (!int.TryParse(id, out parsed_id))
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.Progresses.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- Progress does not exist");
            }

            db.Progresses.Remove(result);
            db.SaveChanges();

            return result;
        }
    }
}