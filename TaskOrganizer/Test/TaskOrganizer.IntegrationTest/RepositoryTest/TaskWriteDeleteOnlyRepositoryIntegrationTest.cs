using System;
using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using Xunit;

namespace TaskOrganizer.IntegrationTest.RepositoryTest
{
    public class TaskWriteDeleteOnlyRepositoryIntegrationTest
    {
        private IRegisterTaskUseCase _registerTaskUseCase;
        public TaskWriteDeleteOnlyRepositoryIntegrationTest()
        {
            
        }  

        // [Fact(Skip="need implemented")]
        // public void MustInsertTheTask()
        // {
        //     // need to implement 
        //     var option = new DbContextOptionsBuilder<TaskOrganizerContext>()
        //         .UseInMemoryDatabase("DbTaskOrganizer")
        //         .Options;
        //     var context = new TaskOrganizerContext(option); 
        //     var repository = new TaskWriteDeleteOnlyRepository(context);

            
        //     _registerTaskUseCase = new RegisterTaskUseCase(repository);

        //     _registerTaskUseCase.Register(MockDataTest());


        // }

        #region AuxiliaryMethods
        private DomainTask MockDataTest()
        {
            var domainTask = new DomainTask
            {
                EstimetedDate = DateTime.Now.Date.AddDays(25),
                IsNew = true
            };
            domainTask.SetTitle("Tarefa ligar para o medico as 14 horas do dia 17 do mes de março");
            domainTask.SetDescription("Description test");

            return domainTask;
        }
        

        #endregion
    }
}