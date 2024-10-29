using lms.models;

namespace Lms.Models;

public class Block : Item
{
    // Fields


    // Id and Created At Implemented in Items
    public string? Description { get; set; }

    public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}