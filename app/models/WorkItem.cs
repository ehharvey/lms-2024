using System.Security.Cryptography;

using Lms.Models.Abstract;
namespace Lms.Models;

public class WorkItem : Taggable
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    [Cli.Parameter(Order = 0)]
    public required string Title { get; set; }

    [Cli.Parameter(Order = 1)]
    public DateTime? DueAt { get; set; }

    public List<Block> Blocks { get; set; } = new List<Block>();
    public List<Progress> Progresses { get; set; } = new List<Progress>();
}