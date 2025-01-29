using Lms;
using Lms.Models;

public class UserManager : ICommand
{
    // Properties
    private const string ActiveUserFilePath = "activeUser.txt"; // Path to Active User File

    LmsDbContext db;

    public UserManager(LmsDbContext db)
    {
        this.db = db;
    }

    public User CreateUser(string username)
    {
        var user = new User{Username = username};

        db.Users.Add(user);
        db.SaveChanges();

        return user;
    }

    // Get Active User from File 
    public User? FetchActiveUser(StreamReader sr)
    {
        string userString = sr.ReadLine();
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
        var user_id = int.Parse(userString);

        var result = db.Users.Find(user_id);

        return result;
    }

    public string GetHelp()
    {
        throw new NotImplementedException();
    }

    string ICommand.GetHelp(Verb verb)
    {
        throw new NotImplementedException();
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

