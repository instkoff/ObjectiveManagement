﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ObjectiveManagement.DataAccess;

namespace ObjectiveManagement.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200509175748_ObjectiveModified2")]
    partial class ObjectiveModified2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ObjectiveManagement.DataAccess.Entities.ObjectiveEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CompletedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstimateTime")
                        .HasColumnType("int");

                    b.Property<int>("FactTime")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectiveStatusType")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Performers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("ObjectiveManagement.DataAccess.Entities.ObjectiveEntity", b =>
                {
                    b.HasOne("ObjectiveManagement.DataAccess.Entities.ObjectiveEntity", "ParentObjective")
                        .WithMany("SubObjectives")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
