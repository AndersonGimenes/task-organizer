using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class DoneTaskRequest
    {
        public DoneTaskRequest()
        {   
            TaskRequest = new TaskBase();            
        }

        public TaskBase TaskRequest { get; set; }

        public void IsValid()
        {
            var validate = new DoneTaskRequestValidation();
            validate.ValidateDone(this);
        }
    }
}