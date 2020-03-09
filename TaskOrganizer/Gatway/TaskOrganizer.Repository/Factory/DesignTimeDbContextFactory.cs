using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaskOrganizer.Repository.Context;

namespace TaskOrganizer.Repository.Factory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaskOrganizerContext>
    {
        public TaskOrganizerContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "../../../WebApi/TaskOrganizer.Api/appsettings.json")
                .Build(); 
            var builder = new DbContextOptionsBuilder<TaskOrganizerContext>(); 
            builder.UseMySql(configuration.GetConnectionString("Default")); 
            return new TaskOrganizerContext(builder.Options); 
        } 
    }
}