using System.ComponentModel.DataAnnotations.Schema;

using Lms.Models.Abstract;
namespace Lms.Models;

public class Progress : Taggable
{
    // Fields
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

    public string? Description { get; set; }
    
    public int WorkItemId {get; set; }

    public WorkItem? WorkItem { get; set; }
}