using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class InProgressTaskRequest
    {
        public InProgressTaskRequest()
        {
            TaskBase = new TaskBase();
        }

        public TaskBase TaskBase { get; set; }

        public void IsValid()
        {
            var validate = new InProgressTaskRequestValidation();
            validate.ValidateInProgress(this);
        }
    }
}