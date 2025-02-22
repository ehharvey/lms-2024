using System.Diagnostics.CodeAnalysis;
using Lms;
using Lms.Controllers;

[ExcludeFromCodeCoverage]
partial class Program
{
    public static int Main(string[] args)
    {
        var view = new Lms.Views.SimpleStdout();

        var dbContext = new LmsDbContext();
        
        var cli = new Lms.Cli.Router(view, dbContext);

        CommandLineParser parser = new CommandLineParser();

        ((Noun noun, Verb verb), string[] commandArgs) = parser.ParseWithArgs(args);

        if (Global.config.PrintConfiguration)
        {
            Console.WriteLine("################ CONFIGURATION #############");
            Console.WriteLine(view.Stringify(Global.config));
            Console.WriteLine("############################################");
        }

        cli.Execute(noun.ToString(), verb.ToString(), commandArgs);

        return 0;
    }
}
