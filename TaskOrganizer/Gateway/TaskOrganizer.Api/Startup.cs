using System;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskOrganizer.Api.Models;
using TaskOrganizer.Api.Validation;
using TaskOrganizer.Domain.ContractUseCase;
using TaskOrganizer.Repository;
using TaskOrganizer.Repository.Context;
using TaskOrganizer.UseCase;
using TaskOrganizer.UseCase.ContractRepository;

namespace TaskOrganizer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Repository    
            services.AddTransient<ITaskReadOnlyRepository, TaskReadOnlyRepository>();
            services.AddTransient<ITaskWriteDeleteOnlyRepository, TaskWriteDeleteOnlyRepository>();
            
            // UseCase
            services.AddTransient<IGetTasksUseCase, GetTasksUseCase>();
            services.AddTransient<IRegisterTaskUseCase, RegisterTaskUseCase>();
            services.AddTransient<IDeleteTaskUseCase, DeleteTaskUseCase>();

            services.AddDbContext<TaskOrganizerContext>(
                option => option.UseNpgsql(Configuration["connectionString"])
            );
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
