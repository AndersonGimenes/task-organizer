using System;

namespace TaskOrganizer.Domain.Entities
{
    public class DomainTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimetedDate { get; set; }
        public Progress Progress { get; set; }
    }
}
