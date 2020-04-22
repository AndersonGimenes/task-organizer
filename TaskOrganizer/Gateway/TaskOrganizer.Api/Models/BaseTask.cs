using System;

namespace TaskOrganizer.Api.Models
{
    public abstract class BaseTask
    {
        
        public int TaskNumeber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}