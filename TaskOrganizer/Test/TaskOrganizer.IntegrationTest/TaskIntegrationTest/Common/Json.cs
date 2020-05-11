using Newtonsoft.Json;
using TaskOrganizer.Api.Models;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common
{
    public class Json
    {
        public static TaskModel JsonDeserialize(string json)
        {
            return JsonConvert.DeserializeObject<TaskModel>(json);
        }
    }
}