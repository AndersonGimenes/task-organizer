using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TaskOrganizer.Repository.Entities;

namespace TaskOrganizer.Repository.Context
{
    public class TaskOrganizerContext : DbContext
    {
        public TaskOrganizerContext(DbContextOptions option) : base(option)
        {
            
        }
        public TaskOrganizerContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured)
                return;
                
            optionsBuilder.UseNpgsql(ReturnConnectionString());
            
        }
            
        public virtual DbSet<RepositoryTask> RepositoryTasks { get; set; }
        public virtual DbSet<ProgressType> ProgressTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var repositoyTask = modelBuilder.Entity<RepositoryTask>();
            repositoyTask
                .HasKey(x => x.TaskId)
                .HasName("Fk_Task");  
            
            repositoyTask
                .Property(x => x.Title)
                .HasColumnType("varchar(200)")
                .IsRequired();

            repositoyTask
                .Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            repositoyTask
                .Property(x => x.ProgressId)
                .IsRequired();

            repositoyTask
                .Property(x => x.CreateDate)
                .IsRequired();

            repositoyTask
                .Property(x => x.EstimetedDate)
                .IsRequired();

            var progressType = modelBuilder.Entity<ProgressType>();
            progressType
                .HasKey(x => x.ProgressId)
                .HasName("Fk_ProgressType");
            
            progressType
                .Property(x => x.Description)
                .HasColumnType("varchar(20)")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        private string ReturnConnectionString()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");  
            var JSON = System.IO.File.ReadAllText(filePath);
            dynamic returned = Newtonsoft.Json.JsonConvert.DeserializeObject(JSON);

            return (string)returned["connectionString"];
        }
    }
}