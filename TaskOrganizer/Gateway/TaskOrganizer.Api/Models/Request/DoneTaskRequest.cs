using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class DoneTaskRequest
    {
        public DoneTaskRequest()
        {   
            TaskBase = new TaskBase();            
        }

        public TaskBase TaskBase { get; set; }

        public void IsValid()
        {
            var validate = new DoneTaskRequestValidation();
            validate.ValidateDone(this);
        }
    }
}