using EntityFramework.Exceptions.Common;
using Lms;
using Lms.Models;

namespace user_manager_tests;

public class TagTests: IDisposable
{
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "Db");

    UserManager manager = new UserManager(db);

    public void Dispose()
    {
        db.Users.RemoveRange(db.Users);
        db.SaveChanges();
    }

    [Fact]
    public void TestBogusUserString() {
        // Arrange
        var user_string = "foobar";

        // Act + Assert
        var user = manager.ParseUser(user_string); // should not crash
    }

    [Fact]
    public void TestUserCompose() {
        // Arrange
        var user = new User {Id = 100, Username = "FooBar"};
        var expected = "100";

        // Act
        var actual = manager.ComposeUser(user);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void TestCreateTwoUsers() {
        // Arrange
        var expected_one = "user_one";
        var expected_two = "user_two";

        // Act
        var user_one = manager.CreateUser(expected_one);
        var user_two = manager.CreateUser(expected_two);

        // Assert
        var actual_one = user_one.Username;
        var actual_two = user_two.Username;

        Assert.Equal(expected_one, actual_one);
        Assert.Equal(expected_two, actual_two);
    }

    [Fact]
    public void TestFetchActiveUserBogusText() {
        // Arrange
        // https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        var sr_stream = new MemoryStream();
        var sr_writer = new StreamWriter(sr_stream);
        sr_writer.Write("foobar");
        sr_writer.Flush();
        sr_stream.Position = 0;
        var sr = new StreamReader(sr_stream);

        // Act
        manager.FetchActiveUser(sr); // Shouldn't crash
    }

    [Fact]
    public void TestFetchActiveUserValid() {
        // Arrange
        var username = "mr. foo";
        var expected_user = manager.CreateUser(username);

        var sr_stream = new MemoryStream();
        var sr_writer = new StreamWriter(sr_stream);
        sr_writer.Write($"{expected_user.Id}");
        sr_writer.Flush();
        sr_stream.Position = 0;
        var sr = new StreamReader(sr_stream);

        // Act
        var actual_user = manager.FetchActiveUser(sr);

        // Assert
        Assert.Equal(expected_user, actual_user);
    }
}

