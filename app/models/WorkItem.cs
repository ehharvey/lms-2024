using System.Security.Cryptography;

namespace Lms.Models;

public class WorkItem
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime? DueAt { get; set; }
    public required string Title { get; set; }
    public List<Block> Blocks { get; set; } = new List<Block>();
    public List<Progress> Progresses { get; set; } = new List<Progress>();
}