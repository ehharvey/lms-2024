namespace Lms.Models;

public class Block
{
    public int Id { get; set; }
    public required string Title { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? Description { get; set; }

    public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}