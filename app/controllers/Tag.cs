using Lms.Models;
using Lms;
using EntityFramework.Exceptions.Common;

namespace Lms.Controllers
{
    [Cli.Controller]
    class Tag
    {
        private LmsDbContext db;

        public Tag(LmsDbContext db)
        {
            this.db = db;
        }

        [Cli.Verb]
        public List<Models.Tag> List()
        {
            return db.Tags.AsEnumerable().ToList();
        }

        [Cli.Verb]
        public Models.Tag Create(string[] command_args)
        {
            if (command_args.Count() < 1)
            {
                throw new ArgumentException("Requires 1 argument (name of the tag)");
            }
            var name = command_args[0];
            var tag = new Lms.Models.Tag { Name = name };

            try
            {
                db.Tags.Add(tag);
                db.SaveChanges();
            }
            catch (UniqueConstraintException)
            {
                throw new ArgumentException($"Tag with name {name} already exists!");
            }

            return tag;
        }

        [Cli.Verb]
        public Models.Tag Delete(string[] command_args)
        {
            // First argument should be
            var string_id = command_args[0];

            int parsed_id;
            if (!int.TryParse(string_id, out parsed_id))
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.Tags.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- Tag does not exist");
            }

            db.Tags.Remove(result);
            db.SaveChanges();

            return result;
        }
    }
}