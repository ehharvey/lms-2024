using Lms;
using Lms.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Controllers
{

    [Cli.Controller]
    class Progress
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

        [Cli.Verb]
        public List<Lms.Models.Progress> List()
        {
            return db.Progresses.ToList();
        }

        [Cli.Verb]
        public Models.Progress Edit(string[] args)
        {
            int parsed_id = -1;
            if (args.Count() < 3)
            {
                throw new ArgumentException("3 arguments: Id, Field, and Value!");
            }
            string id = args[0];
            string field = args[1];
            string value = args[2];

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


        [Cli.Verb]
        public Models.Progress Delete(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Id required");
            }

            string id = args[0];
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