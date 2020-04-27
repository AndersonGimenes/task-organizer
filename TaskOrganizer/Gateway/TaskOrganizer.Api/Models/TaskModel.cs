using System;

namespace TaskOrganizer.Api.Models
{
    public class TaskModel
    {        
        public int TaskNumeber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }     
        public DateTime CreateDate { get; set;}
        public DateTime EstimatedDate { get; set; }
        public DateTime? StartDate { get; set;}
        public DateTime? EndDate { get; set;}
    }
}