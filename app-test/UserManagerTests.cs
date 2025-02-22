using EntityFramework.Exceptions.Common;
using Lms;
using Lms.Models;
using Lms.Controllers;

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
        var user_one = manager.Create([expected_one]);
        var user_two = manager.Create([expected_two]);

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
        // var sr = new StreamReader(sr_stream);

        using(StreamReader sr = new StreamReader(sr_stream))
        {
            // Act
            var user = manager.FetchActiveUser(sr); // Shouldn't crash
        }

        
    }

    [Fact]
    public void TestFetchActiveUserValid() {
        // Arrange
        var username = "mr. foo";
        var expected_user = manager.Create([username]);

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

    // Delete Tests
    [Fact]
    public void TestDeleteUser()
    {
        // Arrange
        var username = "Delete Me!";
        var user = new Lms.Models.User { Username = username };
        db.Users.Add(user);
        db.SaveChanges();

        // Act
        var actual = manager.DeleteUser([user.Id.ToString()]);

        // Assert
        Assert.Equal(username, actual.Username);
    }

    [Fact]
    public void TestDeleteStringId()
    {
        // Arrange
        var username = "Delete Me!";
        var user = new Lms.Models.User { Username = username };
        db.Users.Add(user);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { manager.DeleteUser(["invalid id"]); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestDeleteNegativeId()
    {
        // Arrange
        var username = "Delete Me!";
        var user = new Lms.Models.User { Username = username };
        db.Users.Add(user);
        db.SaveChanges();

        var exception = new ArgumentException("Invalid Id -- WorkItem does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { manager.DeleteUser(["-1"]); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestNoUsers()
    {
        // Arrange
        var expected = new List<Lms.Models.User>();

        // Act
        var actual = manager.List().ToList();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestOneUser()
    {
        // Arrange
        var expected = new Lms.Models.User { Username = "TestOneUser" };

        // Act
        db.Users.Add(expected);
        db.SaveChanges();
        var actual = manager.List().ToList();

        // Assert
        Assert.Equal(1, actual.Count());
        Assert.Equal(expected, actual.First());
    }

    [Fact]
    public void TestTwoUsers()
    {
        // Arrange
        var expected_one = new Lms.Models.User { Username = "TestTwoUserOne" };
        var expected_two = new Lms.Models.User { Username = "TestTwoUserTwo" };


        // Act
        db.Users.Add(expected_one);
        db.Users.Add(expected_two);
        db.SaveChanges();
        var actual = manager.List().ToList();

        // Assert
        Assert.Equal(2, actual.Count());
        Assert.Equal(expected_one, actual.First());
        Assert.Equal(expected_two, actual.Skip(1).First());
    }

    [Fact]
    public void TestEditUser()
    {
        // Arrange
        var edit = new Lms.Models.User { Username = "User" };
        db.Users.Add(edit);
        db.SaveChanges();

        var expected_username = "edited_Username";

        // Act
        var actual = manager.Edit([edit.Id.ToString(), "Username", expected_username]);

        // Assert
        Assert.Equal(expected_username, actual.Username);
    }

    [Fact]
    public void TestEditUserStringId()
    {
        // Arrange
        var item = new Lms.Models.User { Username = "User" };
        db.Users.Add(item);
        db.SaveChanges();

        var expected = new ArgumentException("Invalid Id -- not an integer");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { manager.Edit(["asd", "Title", "new title"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditWorkItemNegatiVeId()
    {
        // Arrange
        var item = new Lms.Models.User { Username = "User" };
        db.Users.Add(item);
        db.SaveChanges();

        var expected = new ArgumentException("Invalid Id -- WorkItem does not exist");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { manager.Edit(["-1", "Title", "new title"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditUserInvalidField()
    {
        // Arrange
        var item = new Lms.Models.User { Username = "User" };
        db.Users.Add(item);
        db.SaveChanges();

        var expected = new ArgumentException("Invalid field");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { manager.Edit([item.Id.ToString(), "invalid field", "new username"]); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    
}

