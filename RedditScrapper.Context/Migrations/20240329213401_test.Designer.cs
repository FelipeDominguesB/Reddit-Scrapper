﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedditScrapper.Context;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    [DbContext(typeof(RedditScrapperContext))]
    [Migration("20240329213401_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RedditScrapper.Domain.Entities.Routine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPostsPerSync")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NextRun")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostSorting")
                        .HasColumnType("int");

                    b.Property<string>("SubredditName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SyncRate")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routines");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.RoutineExecution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPostsPerSync")
                        .HasColumnType("int");

                    b.Property<int>("PostSorting")
                        .HasColumnType("int");

                    b.Property<long>("RoutineId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Succeded")
                        .HasColumnType("bit");

                    b.Property<int>("SyncRate")
                        .HasColumnType("int");

                    b.Property<int>("TotalLinksFound")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutinesExecutions");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.RoutineExecutionFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int?>("Classification")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DownloadDirectory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ExecutionId")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Succeded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionId");

                    b.ToTable("RoutineExecutionsFiles");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.Routine", b =>
                {
                    b.HasOne("RedditScrapper.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.RoutineExecution", b =>
                {
                    b.HasOne("RedditScrapper.Domain.Entities.Routine", "Routine")
                        .WithMany("RoutineExecutions")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.RoutineExecutionFile", b =>
                {
                    b.HasOne("RedditScrapper.Domain.Entities.RoutineExecution", "RoutineExecution")
                        .WithMany("RoutineExecutionFiles")
                        .HasForeignKey("ExecutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoutineExecution");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.Routine", b =>
                {
                    b.Navigation("RoutineExecutions");
                });

            modelBuilder.Entity("RedditScrapper.Domain.Entities.RoutineExecution", b =>
                {
                    b.Navigation("RoutineExecutionFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
