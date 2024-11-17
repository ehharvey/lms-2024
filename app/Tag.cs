using Lms.Models;
using Lms;

public class Tag : ICommand
{
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

    string ICommand.GetHelp(Verb verb) {
        switch (verb) {
            case Verb.Create:
                return "Create a tag";
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

    void ICommand.Execute(Verb verb)
    {
        throw new NotImplementedException();
    }

    void ICommand.Execute(Verb verb, string[] command_args)
    {
        throw new NotImplementedException();
    }
}