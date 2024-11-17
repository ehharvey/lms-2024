using Lms.Models;

namespace Lms.Models;

public class Tag {
    // TODO: code standards doc
    public int Id;

    public required string Name;

    public List<Lms.Models.Block> Blocks = new List<Lms.Models.Block>();

    public List<Lms.Models.WorkItem> WorkItems = new List<Lms.Models.WorkItem>();

    public List<Lms.Models.Progress> Progresses = new List<Lms.Models.Progress>();
}