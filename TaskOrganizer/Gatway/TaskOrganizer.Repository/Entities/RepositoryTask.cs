using System;

namespace TaskOrganizer.Repository.Entities
{
    public class RepositoryTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimetedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ProgressType ProgressType { get; set; }
 
    }
}