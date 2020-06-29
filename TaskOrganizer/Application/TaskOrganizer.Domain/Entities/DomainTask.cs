using System;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.Domain.Validation;

namespace TaskOrganizer.Domain.Entities
{
    public class DomainTask
    {
        public int TaskNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimatedDate { get; set; }
        public Progress Progress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public void IsValid()
        {   
            var validation = new DomainTaskValidator();            
            validation.DomainTaskValidate(this);
        }
    }
}
