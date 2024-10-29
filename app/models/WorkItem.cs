using lms.models;
using System.Security.Cryptography;

namespace Lms.Models;

public class WorkItem : Item
{
    // Fields

    // Id and Created At Implemented in Items
    public DateTime? DueAt { get; set; }
    public required string Title { get; set; }

    public List<Block> Blocks { get; set; } = new List<Block>();

    public List<Progress> Progresses { get; set; } = new List<Progress>();
}