using System;
using System.Collections.Generic;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using TaskOrganizer.Repository.Entities;

namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class MockRepositoryTask
    {
        public static List<RepositoryTask> MockDataRepositoryTask()
        {
            return new List<RepositoryTask>
            {
                new RepositoryTask
                {
                    TaskId = 1,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title one", 
                    Description = "Test description one", 
                    ProgressId = (int)Progress.ToDo,
                    EndDate = null,
                    StartDate = null,
                },
                new RepositoryTask
                {
                    TaskId = 2,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title two", 
                    Description = "Test description two", 
                    ProgressId = (int)Progress.InProgress,
                    EndDate = null,
                    StartDate = DateTime.Now.Date.AddDays(5)
                },
                new RepositoryTask
                {
                    TaskId = 3,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title three", 
                    Description = "Test description three", 
                    ProgressId = (int)Progress.Done,
                    EndDate = DateTime.Now.Date.AddDays(30),
                    StartDate = DateTime.Now.Date.AddDays(5)
                }
            };
        }
    }
}