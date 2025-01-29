using System.ComponentModel.DataAnnotations.Schema;

using Lms.Models.Abstract;
namespace Lms.Models;

public class Progress : Taggable
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string? Description { get; set; }
    [ForeignKey("WorkItemId")]
    public WorkItem? WorkItem { get; set; }
}