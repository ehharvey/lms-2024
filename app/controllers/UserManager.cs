using Lms.Models;
using Lms;

namespace Lms.Controllers
{
    [Cli.Controller]
    class UserManager
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


        // Get All Users
        [Cli.Verb]
        public List<Lms.Models.User> List()
        {
            return db.Users.ToList();
        }

        [Cli.Verb]
        public User Login(string[] args)
        {
            if (args.Count() < 1)
            {
                throw new ArgumentException("Id not passed");
            }
            string id = args[0];
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


        [Cli.Verb]
        public User Create(string[] args)
        {
            if (args.Count() < 1)
            {
                throw new ArgumentException("Username not passed");
            }
            string username = args[0];
            var user = new User { Username = username };

            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        
        [Cli.Verb]
        public User Edit(string[] args)
        {
            if (args.Count() < 3)
            {
                throw new ArgumentException("3 arguments required: id, field to modify, and new value");
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

        
        [Cli.Verb]
        public User DeleteUser(string[] args)
        {
            if (args.Count() < 1)
            {
                throw new ArgumentException("Id not passed");
            }
            string id = args[0];
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