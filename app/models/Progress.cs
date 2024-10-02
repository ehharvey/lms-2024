using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.Models;

public class Progress
{
    public int Id { get; set; }

    public DateTime CreatedAt { get;  } = DateTime.Now;

    public string? Description { get; set; }
    
    [ForeignKey("WorkItemId")]
    public WorkItem? WorkItem { get; set; }
}