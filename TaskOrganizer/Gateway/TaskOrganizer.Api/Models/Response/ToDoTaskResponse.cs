namespace TaskOrganizer.Api.Models.Response
{
    public class ToDoTaskResponse
    {
        public ToDoTaskResponse()
        {
            TaskBase = new TaskBase();
        }
        public TaskBase TaskBase { get; set; }
    }
}