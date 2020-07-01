using TaskOrganizer.Api.Validation;

namespace TaskOrganizer.Api.Models
{
    public class ToDoModel
    {
        public ToDoModel()
        {
            TaskModel = new TaskModel();
        }
        public TaskModel TaskModel { get; set; } 

        public void IsValid()
        {
            var validate = new ToDoModelValidation();
            validate.ValidateToDo(this);
        }
    }
}