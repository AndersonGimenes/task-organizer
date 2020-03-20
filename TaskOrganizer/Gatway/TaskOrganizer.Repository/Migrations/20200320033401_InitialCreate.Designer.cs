﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskOrganizer.Repository.Context;

namespace TaskOrganizer.Repository.Migrations
{
    [DbContext(typeof(TaskOrganizerContext))]
    [Migration("20200320033401_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TaskOrganizer.Repository.Entities.ProgressType", b =>
                {
                    b.Property<int>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("ProgressId")
                        .HasName("Fk_ProgressType");

                    b.ToTable("ProgressTypes");
                });

            modelBuilder.Entity("TaskOrganizer.Repository.Entities.RepositoryTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EstimetedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProgressId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.HasKey("TaskId")
                        .HasName("Fk_Task");

                    b.HasIndex("ProgressId")
                        .IsUnique();

                    b.ToTable("RepositoryTasks");
                });

            modelBuilder.Entity("TaskOrganizer.Repository.Entities.RepositoryTask", b =>
                {
                    b.HasOne("TaskOrganizer.Repository.Entities.ProgressType", "ProgressType")
                        .WithOne("RepositoryTask")
                        .HasForeignKey("TaskOrganizer.Repository.Entities.RepositoryTask", "ProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
