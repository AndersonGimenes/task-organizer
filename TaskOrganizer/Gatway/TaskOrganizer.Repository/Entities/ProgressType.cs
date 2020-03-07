namespace TaskOrganizer.Repository.Entities
{
    public class ProgressType
    {
        public int ProgressId { get; set; }
        public string Description { get; set; }
        public virtual RepositoryTask RepositoryTask { get; set; }
    }
}