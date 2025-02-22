using System.ComponentModel.DataAnnotations.Schema;

using Lms.Models.Abstract;
namespace Lms.Models;

public class Progress : Taggable
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    [Cli.Parameter(Order = 0)]
    public string? Description { get; set; }
    
    [Cli.Parameter(Order = 1)]
    public int WorkItemId {get; set; }

    [ForeignKey("WorkItemId")]
    public WorkItem? WorkItem { get; set; }
}