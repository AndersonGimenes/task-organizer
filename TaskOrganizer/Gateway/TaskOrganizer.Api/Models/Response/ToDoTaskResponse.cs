namespace TaskOrganizer.Api.Models.Response
{
    public class ToDoTaskResponse
    {
        public ToDoTaskResponse()
        {
            TaskResponse = new TaskBase();
        }
        public TaskBase TaskResponse { get; set; }
    }
}