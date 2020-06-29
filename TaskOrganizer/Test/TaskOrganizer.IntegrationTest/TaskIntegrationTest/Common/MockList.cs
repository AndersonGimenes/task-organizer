using System;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class MockDataTask
    {
        public static DomainTask MockDataTest(Progress progress)
        {
            return new DomainTask
            {
                EstimatedDate = new DateTime(2020, 05, 15),
                Title = "Title insert",
                Description = "Description insert",
                Progress = progress
            };
        }
    }
}