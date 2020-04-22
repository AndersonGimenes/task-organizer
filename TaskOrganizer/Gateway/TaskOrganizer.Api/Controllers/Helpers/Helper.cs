using System.Collections.Generic;
using AutoMapper;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Api.Controllers.Commom
{
    public static class Helper
    {
        public static IList<TaskResponse> ReturnApiOutList(IList<DomainTask> list)
        {
            var listReturn = new List<TaskResponse>();
            foreach(var item in list)
            {
                listReturn.Add(MapperDomainTaskToTaskOut(item));
            }
            
            return listReturn;
        }

        public static TaskResponse MapperDomainTaskToTaskOut(DomainTask domainTask)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<DomainTask, TaskResponse>();}
            );  

            return config.CreateMapper().Map<DomainTask, TaskResponse>(domainTask);      
        }

        public static DomainTask MapperTaskInToDomainTask(TaskRequest taskRequest)
        {
            var config = new MapperConfiguration(
                cfg => {cfg.CreateMap<TaskRequest, DomainTask>();}
            );  

            return config.CreateMapper().Map<TaskRequest, DomainTask>(taskRequest);      
        }

    }
}