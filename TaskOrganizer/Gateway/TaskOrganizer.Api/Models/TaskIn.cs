using System;

namespace TaskOrganizer.Api.Models
{
    public class TaskIn
    {
        public int TaskNumeber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EstimatedDate { get; set; }
    }
}