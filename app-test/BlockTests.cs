using Lms;
using Lms.Models;
using Lms.Controllers;
using Microsoft.EntityFrameworkCore;

namespace block_test;

public class BlockTests: IDisposable
 {
    static LmsDbContext db = new LmsDbContext(DbDriver.Memory, "BlockTests");
    Blockers block = new Blockers(db);


    public void Dispose()
    {
        db.Blockers.RemoveRange(db.Blockers);
        db.SaveChanges();
    }

    [Fact]
    public void TestDeleteBlock() {
        // Arrange
        var description = "Delete Me!";
        var item = new Block { Description = description };
        db.Blockers.Add(item);
        db.SaveChanges();
        string[] args = {item.Id.ToString()};

        // Act
        var actual = block.Delete(args);

        // Assert
        Assert.Equal(description, actual.Description);
    }

    [Fact]
    public void TestDeleteStringId() {
        // Arrange
        var exception = new ArgumentException("Invalid block id.");
        string[] args = {"id"};

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { block.Delete(args); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestDeleteNegativeId() {
        // Arrange
        var exception = new ArgumentException("Invalid block id.");
        string[] args = {"-1"};

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { block.Delete(args); });

        // Assert
        Assert.Equal(exception.Message, actual.Message);
    }

    [Fact]
    public void TestNoWorkItems()
    {
        // Arrange
        var expected = new List<Block>();

        // Act
        var actual = block.GetBlockers().ToList();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestOneBlock()
    {
        // Arrange
        var expected = new Block { Description = "TestOneBlock", WorkItems = []};

        // Act
        db.Blockers.Add(expected);
        db.SaveChanges();
        var actual = block.GetBlockers().ToList();

        // Assert
        Assert.Equal(1, actual.Count());
        Assert.Equal(expected, actual.First());        
    }

    [Fact]
    public void TestTwoBlockers()
    {
        // Arrange
        var expected_one = new Block { Description = "TestTwoBlockOne", WorkItems = []};
        var expected_two = new Block { Description = "TestTwoBlockTwo", WorkItems = []};


        // Act
        db.Blockers.Add(expected_one);
        db.Blockers.Add(expected_two);
        db.SaveChanges();
        var actual = block.GetBlockers().ToList();

        // Assert
        Assert.Equal(2, actual.Count());
        Assert.Equal(expected_one, actual.First());
        Assert.Equal(expected_two, actual.Skip(1).First());
    }

    [Fact]
    public void CreateOneBlock()
    {
        // Arrange
        string[] args = {"description", "-"};

        // Act
        var actual = block.Create(args);

        // Assert
        Assert.Equal(args[0], actual.Description);
    }

    [Fact]
    public void TestBlockItem() {
        // Arrange
        var edit = new Block { Description = "description" };
        db.Blockers.Add(edit);
        db.SaveChanges();

        string[] args = {edit.Id.ToString(), "edited_description", "-"};

        // Act
        var actual = block.Edit(args);

        // Assert
        Assert.Equal(args[1], actual.Description);
    }

    [Fact]
    public void TestEditBlockStringId() {
        // Arrange
        string[] args = {"id", "edited_description", "-"};
        var expected = new ArgumentException("Invalid block id.");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { block.Edit(args); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditBlockNegatiVeId() {
        // Arrange
        string[] args = {"-1", "edited_description", "-"};
        var expected = new ArgumentException("Invalid block id.");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { block.Edit(args); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }

    [Fact]
    public void TestEditBlockInvalidField() {
        // Arrange
        string[] args = {"1", "-"};
        var expected = new ArgumentException("Invalid options.");

        // Act
        var actual = Assert.Throws<ArgumentException>(() => { block.Edit(args); });

        // Assert
        Assert.Equal(expected.Message, actual.Message);
    }
 }
