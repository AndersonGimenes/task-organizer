namespace TaskOrganizer.Api.Models.Response
{
    public class TaskResponse
    {
        public TaskResponse()
        {
            TaskBase = new TaskBase();
        }
        
        public TaskBase TaskBase { get; set; }
    }
}