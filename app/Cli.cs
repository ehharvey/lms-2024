using System.Reflection;

namespace Lms.Cli
{
    public class Controller : Attribute
    { }

    public class Command : Attribute
    { }

    struct ControllerInfo
    {
        public string Name;
        public object Command;
        public Dictionary<string, MethodInfo> Verbs;
    }

    public class Router
    {
        private Dictionary<string, ControllerInfo> Controllers;

        private readonly Views.IView View;

        public Router(_Config config, Views.IView view)
        {
            this.View = view;
            Controllers = config.EnabledControllers.ToDictionary(c => c, c => {
                Type t = Type.GetType($"Lms.Controllers.{c}")?? throw new ArgumentException($"{c} is not a valid controller. Not found");

                if (t.GetCustomAttributes(typeof(Command), false).Length <= 0)
                {
                    throw new ArgumentException($"{c} is not a valid controller. Does not have Command as attribute");
                }

                var dbContext = new LmsDbContext();

                var result_name = c;
                var result_command = Activator.CreateInstance(t, dbContext)?? throw new Exception($"Unable to insantiate {c}");
                var result_verbs = t.GetMethods()
                                    .Where(m => m.GetCustomAttributes(typeof(Command), false).Length > 0)
                                    .Where(m => m.ReturnType == typeof(object))
                                    .ToDictionary(m => m.Name, m => m);
                var result = new ControllerInfo {
                    Name = result_name,
                    Command = result_command,
                    Verbs = result_verbs
                };

                return result;
            });

        }

        public void Execute(string command, string verb, string[] args)
        {
            var controller = Controllers.ContainsKey(command) ? Controllers[command] : throw new ArgumentException($"Controller {command} not found");

            var method = controller.Verbs.ContainsKey(command) ? controller.Verbs[verb] : throw new ArgumentException($"Verb {verb} not found");

            var result = method.Invoke(controller, args)?? throw new Exception("Execution result returned null");

            var stdout = View.Stringify(result);

            Console.WriteLine(stdout);
        }
    }
}