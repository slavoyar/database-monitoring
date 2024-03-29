﻿using System;
using DatabaseMonitoring.Services.Workspace.Infrustructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Workspace.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230610143459_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("WorkspaceEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceEntityId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("WorkspaceEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceEntityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.WorkspaceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.Server", b =>
                {
                    b.HasOne("DatabaseMonitoring.Services.Workspace.Domain.WorkspaceEntity", null)
                        .WithMany("Servers")
                        .HasForeignKey("WorkspaceEntityId");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.User", b =>
                {
                    b.HasOne("DatabaseMonitoring.Services.Workspace.Domain.WorkspaceEntity", null)
                        .WithMany("Users")
                        .HasForeignKey("WorkspaceEntityId");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Domain.WorkspaceEntity", b =>
                {
                    b.Navigation("Servers");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
