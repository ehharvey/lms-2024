using Lms.Models;

public class UserManager
{
    // Properties
    private const string DELIMITER = "|";
    private const string ActiveUserFilePath = "activeUser.txt"; // Path to Active User File
    public User? ActiveUser { get; set; }  // Active User

    public UserManager()
    {
        // Create Active User File if not exists
        if (!File.Exists(ActiveUserFilePath))
        {
            File.Create(ActiveUserFilePath);
        }
        else
        {
            FetchActiveUser();
        }
    }

    // Get Active User from File 
    public void FetchActiveUser()
    {
        using(StreamReader sr = new StreamReader(ActiveUserFilePath))
        {
            string userString = sr.ReadLine();
            ActiveUser = ParseUser(userString);
        }
    }

    // Update/Change Active User (Store Active User to the File)
    public void UpdateActiveUser(User user)
    {
        ActiveUser = user;
        using (StreamWriter sw = new StreamWriter(ActiveUserFilePath, false))   // Overwrites the file
        {
            sw.WriteLine(ComposeUser(user));
        }
    }

    // Compose User
    public string ComposeUser(User user)
    {
        return $"{user.Id}{DELIMITER}{user.Username}";
    }

    // Parse User
    public User ParseUser(string userString)
    {
        string[] userProps = userString.Split(DELIMITER);
        return new User { Id = int.Parse(userProps[0]), Username = userProps[1] };
    }
}

