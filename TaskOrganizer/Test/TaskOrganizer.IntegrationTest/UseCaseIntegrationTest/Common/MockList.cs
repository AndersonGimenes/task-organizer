using System;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class MockDataTask
    {
        public static DomainTask MockDataTest()
        {
            var domainTask = new DomainTask
            {
                EstimetedDate = DateTime.Now.Date.AddDays(25),
                IsNew = true
            };
            domainTask.SetTitle("Title test");
            domainTask.SetDescription("Description test");

            return domainTask;
        }
    }
}