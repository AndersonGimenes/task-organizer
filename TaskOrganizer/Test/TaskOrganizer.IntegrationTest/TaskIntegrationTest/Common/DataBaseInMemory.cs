using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Repository.Context;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest
{
    public static class DataBaseInMemory
    {
        public static TaskOrganizerContext ReturnContext()
        {
            var option = new DbContextOptionsBuilder<TaskOrganizerContext>()
                .UseInMemoryDatabase("DbTaskOrganizer")
                .Options;
            return new TaskOrganizerContext(option); 
        }
    }
}