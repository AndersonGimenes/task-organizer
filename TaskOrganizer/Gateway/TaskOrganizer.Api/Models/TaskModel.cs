using System;
using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models
{
    public class TaskModel
    {        
        public int TaskNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }     
        public DateTime CreateDate { get; set;}
        public DateTime EstimatedDate { get; set; }
        public DateTime? StartDate { get; set;}
        public DateTime? EndDate { get; set;}

        public void IsValid()
        {
            var validation = new TaskModelValidator();
            
            if(this.Progress.Equals("ToDo"))
                validation.ValidateToDo(this);
            
            if(this.Progress.Equals("InProgress"))
                validation.ValidateInProgress(this, nameof(EndDate));
        }
    }
}