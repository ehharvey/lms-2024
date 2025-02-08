using Lms.Models;
using Lms;

namespace Lms.Controllers
{
    class UserManager : IController
    {

        // Fields for fetching parameters from CLI args
        public enum Field
        {
            Username
            // Add more fields here.
        }

        // Properties
        private const string ActiveUserFilePath = "activeUser.txt"; // Path to Active User File

        LmsDbContext db;

        public UserManager(LmsDbContext db)
        {
            this.db = db;
        }


        // ICommand Functionalities


        // Command Documentation for CLI Application

        // Main Command Documentation
        public string GetHelp()
        {
            return $"""
        User

        Description:
        Represents the User. A User is a person who uses the program.

        Verbs:
        - list: {GetHelp(Verb.List)}
        - edit: {GetHelp(Verb.Edit)}
        - create: {GetHelp(Verb.Create)}
        - delete: {GetHelp(Verb.Delete)}
        - login: {GetHelp(Verb.Login)}
        """;
        }

        // Individual Commands with their Description

        public string GetHelp(Verb verb)
        {
            switch (verb)
            {
                case Verb.Create:
                    return "Create a new User";
                case Verb.Edit:
                    return "Edit an existing User";
                case Verb.List:
                    return "List the Users recorded previously.";
                case Verb.Delete:
                    return "Delete an User";
                case Verb.Login:
                    return "Login as an User";
                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }


        // Execute Function without additional Arguments (Ex. List) -> lms User List
        public void Execute(Verb verb)
        {
            switch (verb)
            {
                case Verb.List:
                    var users = GetUsers();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("| id | Username         |");
                    users.ToList().ForEach(
                        (u) =>
                        {
                            Console.WriteLine($"| w{u.Id} | {u.Username} |");
                        }
                    );
                    Console.WriteLine("------------------------------");
                    break;
                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }


        // Overloaded Execute Function with additional Arguments (Ex. Delete, Edit, Create) -> lms User Delete 0, lms User Edit 3
        public void Execute(Verb verb, string[] command_args)
        {
            switch (verb)
            {
                case Verb.List:
                    Execute(verb);
                    break;
                case Verb.Delete:
                    if (command_args.Count() < 1)
                    {
                        throw new ArgumentException("Delete requires an ID!");
                    }

                    var delete_result = DeleteUser(command_args[0]);

                    Console.WriteLine("----------------");
                    Console.WriteLine($"ID: {delete_result.Id}");
                    Console.WriteLine($"Title: {delete_result.Username}");
                    Console.WriteLine("----------------");
                    break;
                case Verb.Edit:
                    if (command_args.Count() < 3)
                    {
                        throw new ArgumentException("3 arguments: Id, Field, and Value!");
                    }

                    var edit_result = EditUser(command_args[0], command_args[1], command_args[2]);

                    Console.WriteLine("------------------------");
                    Console.WriteLine($"Id: {edit_result.Id}");
                    Console.WriteLine($"Title: {edit_result.Username}");
                    Console.WriteLine("------------------------");

                    break;
                case Verb.Create:
                    if (command_args.Count() < 1)
                    {
                        throw new ArgumentException("Create requires at least a Title Arg");
                    }

                    string username = command_args[0];

                    var create_result = CreateUser(username);

                    Console.WriteLine("----------------------");
                    Console.WriteLine($"Id: {create_result.Id}");
                    Console.WriteLine($"Title: {create_result.Username}");
                    Console.WriteLine("------------------------");
                    break;
                case Verb.Login:
                    if (command_args.Count() < 1)
                    {
                        throw new ArgumentException("Login requires an ID!");
                    }
                    var login_result = Login(command_args[0]);
                    Console.WriteLine("----------------");
                    Console.WriteLine($"ID: {login_result.Id}");
                    Console.WriteLine($"Title: {login_result.Username}");
                    Console.WriteLine("----------------");
                    break;
                default:
                    throw new ArgumentException("Invalid Verb");
            }
        }

        // Functionalities (Create, Edit, Delete, Login, List)

        // Get All Users
        public IEnumerable<Lms.Models.User> GetUsers()
        {
            return db.Users.AsEnumerable();
        }

        // Login User (Currently only by ID)
        public User Login(string id)
        {
            int parsed_id;
            if (!int.TryParse(id, out parsed_id))
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }
            var result = db.Users.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- WorkItem does not exist");
            }

            UpdateActiveUser(result);

            return result;
        }

        // Create User
        public User CreateUser(string username)
        {
            var user = new User { Username = username };

            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        // Edit User
        public User EditUser(string id, string field, string value)
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

            var result = db.Users.Find(parsed_id);

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
                case Field.Username:
                    result.Username = value;
                    break;
                // Add more fields here.
                default:
                    throw new ArgumentException("Invalid Field");
            }

            db.Users.Update(result);
            db.SaveChanges();

            return result;
        }

        // Delete User
        public User DeleteUser(string id)
        {
            int parsed_id;
            if (!int.TryParse(id, out parsed_id))
            {
                throw new ArgumentException("Invalid Id -- not an integer");
            }

            var result = db.Users.Find(parsed_id);

            if (result == null)
            {
                throw new ArgumentException("Invalid Id -- WorkItem does not exist");
            }

            db.Users.Remove(result);
            db.SaveChanges();

            return result;
        }


        // Active User Management

        // Get Active User from File 
        public User? FetchActiveUser(StreamReader sr)
        {
            string userString = sr.ReadLine();

            if (!int.TryParse(userString, out int result))
            {
                Console.WriteLine("Invalid data in file");
                return null;
            }

            return ParseUser(userString);
        }

        // Update/Change Active User (Store Active User to the File)
        public void UpdateActiveUser(User user)
        {
            using (StreamWriter sw = new StreamWriter(ActiveUserFilePath, false))   // Overwrites the file
            {
                sw.WriteLine(ComposeUser(user));
            }
        }

        // Compose User
        public string ComposeUser(User user)
        {
            return $"{user.Id}";
        }

        // Parse User
        public User? ParseUser(string userString)
        {
            var user_id = -1;

            if (!int.TryParse(userString, out user_id))
            {
                Console.WriteLine($"The input string {userString} was not in a correct format. Expected input: Integer");
                return null;
            }

            var result = db.Users.Find(user_id);
            return result;
        }
    }
}