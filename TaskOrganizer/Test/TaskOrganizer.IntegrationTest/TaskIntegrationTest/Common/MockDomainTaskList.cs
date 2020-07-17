using System;
using System.Collections.Generic;
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
                    TaskId = 10,
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
                    TaskId = 20,
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
                    TaskId = 30,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title three", 
                    Description = "Test description three", 
                    ProgressId = (int)Progress.Done,
                    EndDate = DateTime.Now.Date.AddDays(30),
                    StartDate = DateTime.Now.Date.AddDays(5)
                },
                new RepositoryTask
                {
                    TaskId = 40,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date,
                    Title = "Test title forty", 
                    Description = "Test description forty", 
                    ProgressId = (int)Progress.ToDo,
                    EndDate = null,
                    StartDate = null
                },
                new RepositoryTask
                {
                    TaskId = 100,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date.AddDays(-8),
                    Title = "Test title one hundred", 
                    Description = "Test description one hundred", 
                    ProgressId = (int)Progress.ToDo,
                    EndDate = null,
                    StartDate = null
                },
                new RepositoryTask
                {
                    TaskId = 200,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date.AddDays(-8),
                    Title = "Test title two hundred", 
                    Description = "Test description two hundred", 
                    ProgressId = (int)Progress.InProgress,
                    EndDate = null,
                    StartDate = DateTime.Now.Date.AddDays(-5)
                },
                new RepositoryTask
                {
                    TaskId = 300,
                    EstimatedDate = DateTime.Now.Date.AddDays(20),
                    CreateDate = DateTime.Now.Date.AddDays(-8),
                    Title = "Test title three hundred", 
                    Description = "Test description three hundred", 
                    ProgressId = (int)Progress.InProgress,
                    EndDate = null,
                    StartDate = DateTime.Now.Date.AddDays(-5)
                }
            };

        }
    }
}