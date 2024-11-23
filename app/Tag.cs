using Lms.Models;
using Lms;
using EntityFramework.Exceptions.Common;

class Tag : ICommand
{
    private LmsDbContext db;

    public Tag(LmsDbContext db) {
        this.db = db;
    }

    public string GetHelp()
    {
        return $"""
        Tag 

        Description:
        Tags are things applied to all other entities.
        This command managed the existence of tags and applies
        them to other entities (e.g., WorkItem Tags, etc.)

        Verbs:
        - create: {GetHelp(Verb.Create)}
        - list: {GetHelp(Verb.List)}
        - edit: {GetHelp(Verb.Edit)}
        - delete: {GetHelp(Verb.Delete)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.Create:
                return "Create a tag. Requires 1 additional argument";
            case Verb.List:
                return "List currently created tags";
            case Verb.Edit:
                return "Edit the name of a tag";
            case Verb.Delete:
                return "Delete a tag";
            default:
                throw new ArgumentException("Unsupported Tag");
        }
    }

    public List<Lms.Models.Tag> GetTags() {
        return db.Tags.AsEnumerable().ToList();
    }

    public Lms.Models.Tag CreateTag(string[] command_args) {
        if (command_args.Count() < 1)
        {
            throw new ArgumentException("Requires 1 argument (name of the tag)");
        }
        var name = command_args[0];
        var tag = new Lms.Models.Tag { Name = name };

        try {
            db.Tags.Add(tag);
            db.SaveChanges();
        }
        catch (UniqueConstraintException) {
            throw new ArgumentException($"Tag with name {name} already exists!");
        }

        return tag;
    }

    public Lms.Models.Tag DeleteTag(string[] command_args) {
        // First argument should be
        var string_id = command_args[0];

        int parsed_id;
        if (!int.TryParse(string_id, out parsed_id)) {
            throw new ArgumentException("Invalid Id -- not an integer");
        }

        var result = db.Tags.Find(parsed_id);

        if (result == null) {
            throw new ArgumentException("Invalid Id -- Tag does not exist");
        }

        db.Tags.Remove(result);
        db.SaveChanges();

        return result;
    }    

    public void Execute(Verb verb)
    {
        throw new NotImplementedException("Use Execute(verb, command_args)");
    }


    public void Execute(Verb verb, string[] command_args)
    {
        switch (verb) {
            case Verb.List:
                List<Lms.Models.Tag> tags = GetTags();
                tags.ForEach(
                    (l) => {
                        Console.WriteLine(l.Name);
                    }
                );
                break;
            case Verb.Create:
                var createdTag = CreateTag(command_args);
                Console.WriteLine(createdTag.Name);
                break;
            case Verb.Delete:
                var deletedTag = DeleteTag(command_args);
                Console.WriteLine(deletedTag.Name);
                break;
            default:
                throw new ArgumentException("Invalid Verb");
        }
    }
}