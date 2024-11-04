namespace Lms.Models;

public class Block
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string? Description { get; set; }
    public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}