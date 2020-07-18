using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class InProgressTaskRequest
    {
        public InProgressTaskRequest()
        {
            TaskRequest = new TaskBase();
        }

        public TaskBase TaskRequest { get; set; }

        public void IsValid()
        {
            var validate = new InProgressTaskRequestValidation();
            validate.ValidateInProgress(this);
        }
    }
}