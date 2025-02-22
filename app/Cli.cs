using System.ComponentModel;
using System.Reflection;
using Microsoft.VisualBasic;
using SQLitePCL;

namespace Lms.Cli
{
    public class Controller : Attribute
    { }

    public class Parameter : Attribute
    {
        public int Order { get; init; } = 0;
    }

    public class Verb : Attribute
    { }

    struct ControllerInfo
    {
        public required string Name;
        public required object Controller;
        public required Dictionary<string, MethodInfo> Verbs;
    }

    public class Router
    {
        private Dictionary<string, ControllerInfo> Controllers;

        public LmsDbContext DbContext;

        private readonly Views.IView View;

        public Router(Views.IView view, LmsDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.View = view;
            Controllers = Global.config.EnabledControllers.ToDictionary(c => c, c =>
            {
                Type t = Type.GetType($"Lms.Controllers.{c}") ?? throw new ArgumentException($"{c} is not a valid controller. Not found");

                if (t.GetCustomAttributes(typeof(Controller), false).Length <= 0)
                {
                    throw new ArgumentException($"{c} is not a valid controller. Does not have Command as attribute");
                }

                var dbContext = new LmsDbContext();

                var result_name = c;
                var result_command = Activator.CreateInstance(t, dbContext) ?? throw new Exception($"Unable to insantiate {c}");
                var result_verbs = t.GetMethods()
                                    .Where(m => m.GetCustomAttribute(typeof(Verb), false) is not null)
                                    .ToDictionary(t => t.Name, t => t);

                var result = new ControllerInfo
                {
                    Name = result_name,
                    Controller = result_command,
                    Verbs = result_verbs
                };

                return result;
            });
        }

        public void Execute(string command, string verb, string[] args)
        {
            var controllerInfo = Controllers.ContainsKey(command) ? Controllers[command] : throw new ArgumentException($"Controller {command} not found");
            var controller_verb = controllerInfo.Verbs.GetValueOrDefault(verb)?? throw new ArgumentException($"Controller {command} has no verb {verb}");

            var controller_verb_parameters = controller_verb.GetParameters();
            
            object? result;
            switch (controller_verb_parameters.Length)
            {
                case 0:
                result = controller_verb.Invoke(controllerInfo.Controller, []);
                break;
                case 1:
                result = controller_verb.Invoke(controllerInfo.Controller, [args]);
                break;
                default:
                throw new Exception($"{controllerInfo}::{verb} receives more than 1 parameter");
            }

            if (result is not null)
            {
                var result_stdout = View.Stringify(result);
                Console.WriteLine(result_stdout);
            }             
        }
    }
}