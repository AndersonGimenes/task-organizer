using System;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class MockDataTask
    {
        public static DomainTask MockDataTest(string progress)
        {
            var domainTask = new DomainTask
            {
                EstimatedDate = new DateTime(2020, 05, 15)
            };
            domainTask.SetTitle("Title insert");
            domainTask.SetDescription("Description insert");
            domainTask.SetProgress(progress);

            return domainTask;
            
        }
    }
}