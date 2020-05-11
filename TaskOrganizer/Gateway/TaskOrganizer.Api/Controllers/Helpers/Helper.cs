using System.Collections.Generic;
using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Api.Controllers.Commom
{
    public static class Helper
    {
        public static IList<TaskModel> ReturnTaskModelList(IList<DomainTask> list)
        {
            var listReturn = new List<TaskModel>();
            foreach(var item in list)
            {
                listReturn.Add(MapperDomainTaskToTaskModel(item));
            }
            
            return listReturn;
        }

        public static TaskModel MapperDomainTaskToTaskModel(DomainTask domainTask)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<DomainTask, TaskModel>();}
            );  

            return config.CreateMapper().Map<DomainTask, TaskModel>(domainTask);      
        }

        public static DomainTask MapperTaskModelToDomainTask(TaskModel taskModel)
        {
            var domainTask = new DomainTask
            {
                TaskNumber = taskModel.TaskNumber,
                EstimatedDate = taskModel.EstimatedDate,
                CreateDate = taskModel.CreateDate,
                StartDate = taskModel.StartDate,
                EndDate = taskModel.EndDate
            };
            domainTask.SetTitle(taskModel.Title);
            domainTask.SetDescription(taskModel.Description);
            domainTask.SetProgress(taskModel.Progress);

            return domainTask;

        }
       
    }
}