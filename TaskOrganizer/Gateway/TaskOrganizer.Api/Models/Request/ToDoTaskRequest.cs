using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models.Request
{
    public class ToDoTaskRequest
    {
        public ToDoTaskRequest()
        {
            TaskBase = new TaskBase();
        }
        public TaskBase TaskBase { get; set; } 

        public void IsValid()
        {
            var validate = new ToDoTaskRequestValidation();
            validate.ValidateToDo(this);
        }
    }
}