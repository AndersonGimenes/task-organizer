using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class ToDoTaskRequest
    {
        public ToDoTaskRequest()
        {
            TaskRequest = new TaskBase();
        }
        public TaskBase TaskRequest { get; set; } 

        public void IsValid()
        {
            var validate = new ToDoTaskRequestValidation();
            validate.ValidateToDo(this);
        }
    }
}