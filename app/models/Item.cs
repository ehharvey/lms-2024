namespace lms.models
{
    public abstract class Item
    {
        // Field List For Commands
        public enum Field
        {
            // WorkItem
            Title,
            DueAt,
            // Progress & Blockers
            Description,
            WorkItem // WorkItem(Id)
        }

        // Fields
        public int Id { get; set; }

        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
