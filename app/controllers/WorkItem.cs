using Lms.Models;
using Lms;

namespace Lms.Controllers
{

    [Cli.Controller]
    class WorkItem
    {

        public enum Field
        {
            // WorkItem
            Title,
            DueAt
        }

        // DBContext for Database Interactions (Add, Update, Remove, SaveChanges)
        private LmsDbContext db;

        // Constructor - Set DB
        public WorkItem(LmsDbContext db)
        {
            this.db = db;
        }


        [Cli.Verb]
        public List<Models.WorkItem> List()
        {
            return db.WorkItems.ToList();
        }

        // Create new WorkItem
        [Cli.Verb]
        public Models.WorkItem Create(string[] args)
        {
            if (args.Count() < 1)
            {
                throw new ArgumentException("Create requires at least a Title Arg");
            }

            var title = args[0];
            string? due_at = args.ElementAtOrDefault(1);

            DateTime? parsed_due_at;

            if (due_at != null)
            {
                parsed_due_at = DateTime.Parse(due_at);
            }
            else
            {
                parsed_due_at = null;
            }

            Lms.Models.WorkItem result = new Lms.Models.WorkItem { Title = title, DueAt = parsed_due_at };


            db.Add(result);

            db.SaveChanges();

            return result;
        }

        [Cli.Verb]
        public Models.WorkItem Edit(string[] args)
        {
            if (args.Count() < 3)
            {
                throw new ArgumentException("3 arguments: Id, Field, and Value!");
            }

            string id = args[0];
            string field = args[1];
            string value = args[2];
            int parsed_id = -1;

            try
            {
                parsed_id = int.Parse(id);
            }
            catch
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.WorkItems.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- WorkItem does not exist");
            }

            Field f;
            if (!Enum.TryParse<Field>(field, out f))
            {
                throw new ArgumentException("Invalid field");
            }

            switch (f)
            {
                case Field.Title:
                    result.Title = value;
                    break;
                case Field.DueAt:
                    DateTime parsed_due_at;

                    if (DateTime.TryParse(value, out parsed_due_at))
                    {
                        result.DueAt = parsed_due_at;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid DueAt value provided");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid Field");
            }

            db.WorkItems.Update(result);
            db.SaveChanges();

            return result;
        }

        [Cli.Verb]
        public Models.WorkItem Delete(string[] args)
        {
            if (args.Count() < 1)
            {
                throw new ArgumentException("Delete requires an ID!");
            }

            string id = args[0];
            int parsed_id;
            if (!int.TryParse(id, out parsed_id))
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.WorkItems.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- WorkItem does not exist");
            }

            db.WorkItems.Remove(result);
            db.SaveChanges();

            return result;
        }
    }
}