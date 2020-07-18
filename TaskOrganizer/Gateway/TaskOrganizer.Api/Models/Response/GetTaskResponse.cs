namespace TaskOrganizer.Api.Models.Response
{
    public class GetTaskResponse
    {
        public GetTaskResponse()
        {
            TaskResponse = new TaskBase();
        }
        
        public TaskBase TaskResponse { get; set; }
    }
}