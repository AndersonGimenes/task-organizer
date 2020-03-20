using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Repository.Entities;

namespace TaskOrganizer.Repository.Context
{
    public class TaskOrganizerContext : DbContext
    {
        public TaskOrganizerContext(DbContextOptions options) : base(options)
        {
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
                .HasColumnType("varchar(40)")
                .IsRequired();

            repositoyTask
                .Property(x => x.Description)
                .HasColumnType("varchar(MAX)")
                .IsRequired();

            repositoyTask
                .Property(x => x.ProgressId).IsRequired();

            var progressType = modelBuilder.Entity<ProgressType>();
            progressType
                .HasKey(x => x.ProgressId)
                .HasName("Fk_ProgressType");
            
            progressType
                .Property(x => x.Description)
                .HasColumnType("varchar(20)")
                .IsRequired();

            progressType
                .HasOne(x => x.RepositoryTask)
                .WithOne(x => x.ProgressType)
                .HasForeignKey<RepositoryTask>(b => b.ProgressId);
         
            base.OnModelCreating(modelBuilder);
        }
    }
}