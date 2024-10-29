using lms.models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.Models;

public class Progress : Item
{
    
    // Common Fields

    // Id and Created At Implemented in Items
    public string? Description { get; set; }
    
    [ForeignKey("WorkItemId")]
    public WorkItem? WorkItem { get; set; }
}