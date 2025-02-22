namespace Lms.Models;

using Lms.Models.Abstract;

public class Block : Taggable
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    [Cli.Parameter]
    public string? Description { get; set; }
    public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();

}