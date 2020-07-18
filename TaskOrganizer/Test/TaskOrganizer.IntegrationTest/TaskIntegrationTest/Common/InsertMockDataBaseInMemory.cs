using System;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest;
using TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common;

namespace TaskOrganizer.IntegrationTest.TaskIntegrationTest.Common
{
    public static class InsertMockDataBaseInMemory
    {
        public static void InsertMock()
        {
            using (var context = DataBaseInMemory.ReturnContext())
            {   
                try
                { 
                    context.RepositoryTasks.AddRange(MockRepositoryTask.MockDataRepositoryTask());
                    context.SaveChanges();
                }
                catch(Exception)
                {
                    
                }
            }        
        }
    }
}