using System;

namespace TaskOrganizer.Repository.Entities
{
    public class RepositoryTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProgressId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
         
    }
}