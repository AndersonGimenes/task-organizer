using System.Collections.Generic;
using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Api.Controllers.Commom
{
    public static class Helper
    {
        public static IList<TaskModel> ReturnApiOutList(IList<DomainTask> list)
        {
            var listReturn = new List<TaskModel>();
            foreach(var item in list)
            {
                listReturn.Add(MapperDomainTaskToTaskOut(item));
            }
            
            return listReturn;
        }

        public static TaskModel MapperDomainTaskToTaskOut(DomainTask domainTask)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<DomainTask, TaskModel>();}
            );  

            return config.CreateMapper().Map<DomainTask, TaskModel>(domainTask);      
        }

        public static DomainTask MapperTaskInToDomainTask(TaskModel taskModel)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<TaskModel, DomainTask>();}
            );  

            return config.CreateMapper().Map<TaskModel, DomainTask>(taskModel);      
        }

    }
}