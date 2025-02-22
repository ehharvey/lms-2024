using Lms.Models;
using Microsoft.EntityFrameworkCore;

namespace Lms.Models;

[PrimaryKey(nameof(Id)), Index(nameof(Name), IsUnique = true)]
public class Tag {
    // TODO: code standards doc
    public int Id;

    [Cli.Parameter]
    public required string Name;

    public List<Lms.Models.Block> Blocks = new List<Lms.Models.Block>();

    public List<Lms.Models.WorkItem> WorkItems = new List<Lms.Models.WorkItem>();

    public List<Lms.Models.Progress> Progresses = new List<Lms.Models.Progress>();
}