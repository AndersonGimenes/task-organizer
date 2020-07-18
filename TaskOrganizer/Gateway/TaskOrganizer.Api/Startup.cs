using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase.ContractRepository;
using TaskOrganizer.Api.Mapping;
using TaskOrganizer.Domain.ContractUseCase.Task;
using TaskOrganizer.UseCase.Task;
using TaskOrganizer.Domain.ContractUseCase.Task.ToDo;
using TaskOrganizer.UseCase.Task.ToDo;
using TaskOrganizer.Domain.ContractUseCase.Task.InProgress;
using TaskOrganizer.UseCase.Task.InProgress;
using TaskOrganizer.Domain.ContractUseCase.Task.Done;
using TaskOrganizer.UseCase.Task.Done;
using TaskOrganizer.Repository.Mapping;

namespace TaskOrganizer.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Repository    
            services.AddTransient<ITaskReadOnlyRepository, TaskReadOnlyRepository>();
            services.AddTransient<ITaskWriteDeleteOnlyRepository, TaskWriteDeleteOnlyRepository>();
            
            // UseCase
            services.AddTransient<ITaskUseCase, TaskUseCase>();
            services.AddTransient<IToDoCreateTaskUseCase, ToDoCreateTaskUseCase>();
            services.AddTransient<IToDoUpdateTaskUseCase, ToDoUpdateTaskUseCase>();
            services.AddTransient<IToDoDeleteTaskUseCase, ToDoDeleteTaskUseCase>();
            services.AddTransient<IInProgressUseCase, InProgressUseCase>();
            services.AddTransient<IDoneUseCase, DoneUseCase>();

            // Database configuration
            services.AddDbContext<TaskOrganizerContext>(
                option => option.UseNpgsql(Configuration["connectionString"])
            );

            //Mapper configuration
            var cfg = new MapperConfiguration(x => 
            { 
                x.AddProfile(new MappingProfileApi());
                x.AddProfile(new MappingProfileRepository());
            });
            var mapper = cfg.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
