using System;

namespace TaskOrganizer.Api.Controllers
{
    public class TaskOut
    {
        public int TaskNumeber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EstimatedDate { get; set; }
    }
}