﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Lms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace lms.Migrations
{
    [DbContext(typeof(LmsDbContext))]
    [ExcludeFromCodeCoverage]
    partial class LmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BlockWorkItem", b =>
                {
                    b.Property<int>("BlocksId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorkItemsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BlocksId", "WorkItemsId");

                    b.HasIndex("WorkItemsId");

                    b.ToTable("BlockWorkItem");
                });

            modelBuilder.Entity("Lms.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Blockers");
                });

            modelBuilder.Entity("Lms.Models.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WorkItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("Lms.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BlockId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProgressId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WorkItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProgressId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Lms.Models.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DueAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WorkItems");
                });

            modelBuilder.Entity("BlockWorkItem", b =>
                {
                    b.HasOne("Lms.Models.Block", null)
                        .WithMany()
                        .HasForeignKey("BlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lms.Models.WorkItem", null)
                        .WithMany()
                        .HasForeignKey("WorkItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Models.Progress", b =>
                {
                    b.HasOne("Lms.Models.WorkItem", "WorkItem")
                        .WithMany("Progresses")
                        .HasForeignKey("WorkItemId");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("Lms.Models.Tag", b =>
                {
                    b.HasOne("Lms.Models.Block", null)
                        .WithMany("Tags")
                        .HasForeignKey("BlockId");

                    b.HasOne("Lms.Models.Progress", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProgressId");

                    b.HasOne("Lms.Models.WorkItem", null)
                        .WithMany("Tags")
                        .HasForeignKey("WorkItemId");
                });

            modelBuilder.Entity("Lms.Models.Block", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Lms.Models.Progress", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Lms.Models.WorkItem", b =>
                {
                    b.Navigation("Progresses");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
